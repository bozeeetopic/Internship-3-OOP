using System;
using System.Collections.Generic;
using PhoneBookApp.Entities;
using PhoneBookApp.Helpers;

namespace PhoneBookApp.Actions
{
    static class Actions
    {
        public static Dictionary<Contact, List<Call>> PopulateContacts()
        {

            return new Dictionary<Contact, List<Call>>
            {
            {PopulateContact("Ivo Sanader","1",Enums.PreferenceType.Favorit),new List<Call>()
                {
                PopulateCall(new DateTime(1900,1,1,0,0,0),Enums.Status.Završen,999),
                PopulateCall(DateTime.Now, Enums.Status.Traje,20)
                }
            },
            {PopulateContact("Stipe Mesić","2",Enums.PreferenceType.Blokiran),new List<Call>()
                {
                PopulateCall(new DateTime(1920,1,1,0,0,0),Enums.Status.Završen,99)
                }
            }
            };
        }
        public static Contact PopulateContact(string name, string phone, Enums.PreferenceType preference)
        {
            var contact = new Contact();
            contact.AddValue(name, phone, preference);
            return contact;
        }
        public static Call PopulateCall(DateTime time, Enums.Status status, int duration)
        {
            var call = new Call();
            call.AddValue(time, status, duration);
            return call;
        }


        public static void PrintAllContacts(Dictionary<Contact, List<Call>> contacts)
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("Nemate unesenih kontakata!");
            }
            else
            {
                foreach (var contact in contacts)
                {
                    Console.WriteLine(contact.Key.ToString());
                }
            }
        }
        public static void AddNewContact(Dictionary<Contact, List<Call>> contacts)
        {
            var name = HelpingFunctions.UserStringInput("ime", "", 1);
            var surname = HelpingFunctions.UserStringInput("prezime", "", 0);
            var number = "";
            do
            {
                if (DuplicatePhoneNumberExists(number, contacts))
                {
                    Console.WriteLine("Već postoji kontakt sa tim brojem telefona, pokušajte ponovno!");
                }
                number = HelpingFunctions.UserStringInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
            }
            while (DuplicatePhoneNumberExists(number, contacts));
            Console.WriteLine("Unesi koja je ovo vrsta kontakta, za favorit 0, za default 1 te za blokirati broj upišite 2.");
            var myPreference = (Enums.PreferenceType)HelpingFunctions.UserNumberInput("vaš odabir", 0, 2);
            contacts.Add(PopulateContact(name + " " + surname, number, myPreference), null);
        }
        public static void EraseContacts(Dictionary<Contact, List<Call>> contacts)
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("Lista kontakata prazna, ne postoji kontakt koji možete obrisati!");
                return;
            }
            else
            {
                var keyFound = false;
                PrintAllContacts(contacts);
                Console.Write("Kojeg kontakta želite izbrisati?");
                var number = HelpingFunctions.UserStringInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
                foreach (var contact in contacts)
                {
                    if (contact.Key.PhoneNumber == number)
                    {
                        Console.WriteLine("Želite li sigurno izbrisati kontakta: \n" + contact.Key.ToString() + " (da)?");
                        var eraseConfirm = Console.ReadLine();
                        if (eraseConfirm is "da")
                        {
                            if (!(contact.Value is null))
                            {
                                contact.Value.Clear();
                            }
                            contacts.Remove(contact.Key);
                        }
                        keyFound = true;
                    }
                }
                if (!keyFound)
                {
                    Console.WriteLine("Nije pronađen kontakt sa navedenim brojem!");
                }
                if (contacts.Count > 0)
                {
                    Console.Write("Želite li još kontakata brisati (da)? ");
                    var eraseAnotherConfirm = Console.ReadLine();
                    if (eraseAnotherConfirm == "da")
                    {
                        EraseContacts(contacts);
                    }
                }
            }
        }
        public static void EditContactPreference(Dictionary<Contact, List<Call>> contacts)
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("Lista kontakata prazna, ne postoji kontakt kojem možete mjenjati preferencu!");
                return;
            }
            else
            {
                PrintAllContacts(contacts);
                Console.Write("Kojem kontaktu želite mjenjati preferencu?");
                var number = HelpingFunctions.UserStringInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
                foreach (var contact in contacts)
                {
                    if (contact.Key.PhoneNumber == number)
                    {
                        Console.WriteLine("Nova preferenca:, za favorit 0, za default 1 te za blokirati broj upišite 2.");
                        var preference = HelpingFunctions.UserNumberInput("vaš odabir", 0, 2);
                        var myPreference = (Enums.PreferenceType)preference;
                        Console.WriteLine("Želite li sigurno mijenjati:\nKontaktu " + contact.Key.ToString() + " preferencu u: " + myPreference + "(da)?");
                        string eraseConfirm = Console.ReadLine();
                        if (eraseConfirm == "da")
                        {
                            contacts.Add(PopulateContact(contact.Key.NameAndSurname, contact.Key.PhoneNumber, myPreference), contact.Value);
                            contacts.Remove(contact.Key);
                            return;
                        }
                        return;
                    }
                }
                Console.WriteLine("Kontakt sa traženim brojem ne postoji!");
            }
        }
        public static void PrintAllCallsFromContactSorted(Dictionary<Contact, List<Call>> contacts)
        {
            PrintAllContacts(contacts);
            Console.Write("Kojem kontaktu želite ispisati pozive?");
            var number = HelpingFunctions.UserStringInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
            foreach (var contact in contacts)
            {
                if (contact.Key.PhoneNumber == number)
                {
                    SetCallsThatEndedIntoAppropriateState(contacts);
                    SortedPrint(contact.Value);
                }
            }
        }
        public static void AddNewCall(Dictionary<Contact, List<Call>> contacts)
        {
            PrintAllContacts(contacts);
            Console.WriteLine("Odaberite kontakta kojeg želite nazvati: ");
            var number = HelpingFunctions.UserStringInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
            foreach (var contact in contacts)
            {
                if (contact.Key.PhoneNumber == number)
                {
                    if (contact.Key.Preference == Enums.PreferenceType.Blokiran)
                    {
                        Console.WriteLine("Kontakt blokiran, nemoguće obaviti poziv!");
                        return;
                    }
                    else
                    {
                        SetCallsThatEndedIntoAppropriateState(contacts);
                        if (CheckForOngoingCalls(contacts))
                        {
                            Console.WriteLine("Nemoguće pozvati drugu osobu jer ste trenutno u pozivu!");
                            return;
                        }
                        else
                        {
                            switch ((Enums.Status)HelpingFunctions.RandomCallReply)
                            {
                                case Enums.Status.Propušten:
                                    {
                                        contact.Value.Add(PopulateCall(DateTime.Now, Enums.Status.Propušten, 0));
                                        break;
                                    }
                                case Enums.Status.Traje:
                                    {
                                        contact.Value.Add(PopulateCall(DateTime.Now, Enums.Status.Traje, HelpingFunctions.RandomCallDuration));
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                            return;
                        }
                    }
                }
            }
            Console.WriteLine("Kontakt sa traženim brojem ne postoji!");
        }
        public static void PrintAllCalls(Dictionary<Contact, List<Call>> contacts)
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("Lista kontakata prazna, ne postoji kontakt koji možete obrisati!");
                return;
            }
            else
            {
                var noCalls = true;
                SetCallsThatEndedIntoAppropriateState(contacts);
                foreach (var contact in contacts)
                {
                    if (contact.Value != null)
                    {
                        foreach (var call in contact.Value)
                        {
                            Console.WriteLine(call.ToString());
                        }
                        noCalls = false;
                    }
                }
                if (noCalls)
                {
                    Console.WriteLine("Niste dosad uputili nijedan poziv!");
                }
            }
        }


        public static void SortedPrint(List<Call> calls)
        {
            if (calls.Count == 0)
            {
                Console.WriteLine("Niste imali nijedan poziv sa odabranim kontaktom!");
            }
            else
            {
                var newCallsList = new List<Call>();
                foreach (var call in calls)
                {
                    newCallsList.Add(PopulateCall(call.TimeOfCall, call.CallStatus, call.Duration));
                }
                var listOfDates = new List<DateTime>();
                foreach (var call in newCallsList)
                {
                    if (!listOfDates.Contains(call.TimeOfCall))
                    {
                        listOfDates.Add(call.TimeOfCall);
                    }
                }
                SortDatesUp(listOfDates);
                while (newCallsList.Count > 0)
                {
                    foreach (var date in listOfDates)
                    {
                        var callToErase = new Call();
                        foreach (var call in newCallsList)
                        {
                            if (call.TimeOfCall.Equals(date))
                            {
                                Console.WriteLine(call.ToString());
                                callToErase = call;
                            }
                        }
                        newCallsList.Remove(callToErase);
                    }
                }
                newCallsList.Clear();
                listOfDates.Clear();
            }
        }
        public static bool DuplicatePhoneNumberExists(string number, Dictionary<Contact, List<Call>> contacts)
        {
            if (contacts.Count == 0)
            {
                return true;
            }
            else
            {
                foreach (var contact in contacts)
                {
                    if (contact.Key.PhoneNumber == number)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public static void SortDatesUp(List<DateTime> list)
        {
            var newList = new List<DateTime>();
            while (list.Count > 0)
            {
                var minDate = new DateTime(1, 1, 1, 0, 0, 0);
                foreach (var date in list)
                {
                    if (date.CompareTo(minDate) >= 1)
                        minDate = date;
                }
                newList.Add(minDate);
                list.Remove(minDate);
            }
            foreach (var i in newList)
                list.Add(i);
        }
        public static bool CheckForOngoingCalls(Dictionary<Contact, List<Call>> contacts)
        {
            foreach (var contact in contacts)
            {
                if (contact.Value.Count > 0)
                {
                    foreach (var call in contact.Value)
                    {
                        if (call.CallStatus == Enums.Status.Traje)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static void SetCallsThatEndedIntoAppropriateState(Dictionary<Contact, List<Call>> contacts)
        {
            foreach (var contact in contacts)
            {
                if (contact.Value.Count > 0)
                {
                    foreach (var call in contact.Value)
                    {
                        if (call.CallStatus == Enums.Status.Traje)
                        {
                            if (call.TimeOfCall.AddSeconds(call.Duration) < DateTime.Now)
                            {
                                call.CallStatus = Enums.Status.Završen;
                            }
                        }
                    }
                }
            }
        }
    }
}
