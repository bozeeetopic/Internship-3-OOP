using System;
using System.Collections.Generic;
using PhoneBookApp.Entities;
using PhoneBookApp.Helpers;

namespace PhoneBookApp
{
    class Program
    {
        static void Main()
        {
            var contacts = PopulateContacts();
            MainFunction(contacts);
               
        }
        static void MainFunction(Dictionary<Contact, List<Call>> contacts)
        {
            while(true)
            {
                Console.Clear();
                HelpingFunctions.PrintMenu(contacts.Count);
                var userNumberInput = UserNumberInput("vaš izbor", 1, 7);
                var menuChoice = (Enums.MenuChoice)userNumberInput;
                switch (menuChoice)
                {
                    case Enums.MenuChoice.Print:
                        {
                            PrintAllContacts(contacts);
                            break;
                        }
                    case Enums.MenuChoice.Add:
                        {
                            AddNewContact(contacts);
                            break;
                        }
                    case Enums.MenuChoice.Erase:
                        {
                            EraseContacts(contacts);
                            break;
                        }
                    case Enums.MenuChoice.EditPrefs:
                        {
                            EditContactPreference(contacts);
                            break;
                        }
                    case Enums.MenuChoice.CallsFunctions:
                        {
                            if (contacts.Count == 0)
                            {
                                Console.WriteLine("Nemoguće raditi operacije nad kontaktima kad oni ne postoje!");
                            }
                            else
                            {
                                Console.Clear();
                                HelpingFunctions.PrintSubMenu();
                                userNumberInput = UserNumberInput("vaš izbor", 1, 3);
                                var submenuChoice = (Enums.SubMenuChoice)userNumberInput;
                                switch (submenuChoice)
                                {
                                    case Enums.SubMenuChoice.PrintSorted:
                                        {
                                            PrintAllCallsFromContactSorted(contacts);
                                            break;
                                        }
                                    case Enums.SubMenuChoice.AddNew:
                                        {
                                            AddNewCall(contacts);
                                            break;
                                        }
                                    case Enums.SubMenuChoice.Exit:
                                        {
                                            break;
                                        }
                                    default:
                                        Console.WriteLine("Nepostojeći izbor!");
                                        break;
                                }
                            }
                            break;
                        }
                    case Enums.MenuChoice.PrintCalls:
                        {
                            PrintAllCalls(contacts);
                            break;
                        }
                    case Enums.MenuChoice.Exit:
                        {
                            Console.WriteLine("Hvala na korištenju!");
                            Console.ReadLine();
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Nepostojeći izbor!");
                            break;
                        }
                }
                Console.WriteLine("\n\nEnter za nastavak..");
                Console.ReadLine();
            }
         }


        /*  static string GetPhoneNumber()
          {
              var unos = "";
              Console.WriteLine("Telefonski broj se odnosi na fiksni ili mobitel?");
              do
              {
                  Console.Write("Vaš odabir: ");
                  unos = Console.ReadLine();
              } while ((unos != "fiksni") || (unos != "mobitel"));
              if (unos == "fiksni") return GetTelephoneNumber();
              else return GetMobilePhoneNumber();
          }*/ //Mislio zakomplicirat unos al se sitio da ima i emergency brojeva 95,911 itd
        static bool ForbiddenStringChecker(string stringBeingChecked, string forbiddenString)
        {
            foreach (var character in forbiddenString)
            {
                if (stringBeingChecked.Contains(character)) 
                {
                    return true;
                }
            }
            return false;
        }
        static string UserStringInput(string nameOrSurname, string forbiddenString, int minLength)
        {
            var repeatedInput = false;
            var input = "";
            do
            {
                if (repeatedInput && (input.Length < minLength))
                {
                    Console.WriteLine("Duljina " + nameOrSurname + "na mora biti " + minLength + "!");
                }
                if (repeatedInput && ForbiddenStringChecker(input.ToLower(), forbiddenString))
                {
                    Console.WriteLine(nameOrSurname + " sadrži znak, moraju biti isključivo brojevi!");
                }
                Console.Write("Stanovnikovo " + nameOrSurname + ": ");
                input = Console.ReadLine();        
                Console.WriteLine();
                repeatedInput = true;
            }
            while ((input.Length < minLength) || ForbiddenStringChecker(input.ToLower(), forbiddenString));
            return input;
        }
        static int UserNumberInput(string message, int minValue, int maxValue)
        {
            var repeatedInput = false;
            int? number;
            do
            {
                if (repeatedInput)
                {
                    Console.WriteLine("Morate unjeti broj između " + minValue + " i " + (maxValue) + ".");
                }
                Console.Write("Unesite " + message + ": ");
                try
                {
                    number = int.Parse(Console.ReadLine());
                }
                catch
                {
                    number = -1;
                    Console.WriteLine("Pogrešan unos!!");
                }
                repeatedInput = true;
            }
            while (number > maxValue || number < minValue);
            return (int)number;
        }
        static Dictionary<Contact, List<Call>> PopulateContacts()
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
        static Contact PopulateContact(string name, string phone, Enums.PreferenceType preference)
        {
            var contact = new Contact();
            contact.AddValue(name, phone, preference);
            return contact;
        }
        static Call PopulateCall(DateTime time, Enums.Status status, int duration)
        {
            var call = new Call();
            call.AddValue(time, status, duration);
            return call;
        }


        static void PrintAllContacts(Dictionary<Contact, List<Call>> contacts)
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
        static void AddNewContact(Dictionary<Contact, List<Call>> contacts)
        {
            var name = UserStringInput("ime", "",1);
            var surname = UserStringInput("prezime", "",0);
            var number="";
            do
            {
                if(DuplicatePhoneNumberExists(number, contacts))
                {
                    Console.WriteLine("Već postoji kontakt sa tim brojem telefona, pokušajte ponovno!");
                }
                number = UserStringInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
            }
            while (DuplicatePhoneNumberExists(number, contacts));
            Console.WriteLine("Unesi koja je ovo vrsta kontakta, za favorit 0, za default 1 te za blokirati broj upišite 2.");
            var myPreference = (Enums.PreferenceType) UserNumberInput("vaš odabir", 0, 2);
            contacts.Add(PopulateContact(name + " " + surname, number, myPreference), null);
        }
        static void EraseContacts(Dictionary<Contact, List<Call>> contacts)
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
                var number = UserStringInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
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
                    if(eraseAnotherConfirm == "da")
                    {
                        EraseContacts(contacts);
                    }
                }
            }
        }
        static void EditContactPreference(Dictionary<Contact, List<Call>> contacts)
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
                var number = UserStringInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
                foreach (var contact in contacts)
                {
                    if (contact.Key.PhoneNumber == number)
                    {
                        Console.WriteLine("Nova preferenca:, za favorit 0, za default 1 te za blokirati broj upišite 2.");
                        var preference = UserNumberInput("vaš odabir", 0, 2);
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
        static void PrintAllCallsFromContactSorted(Dictionary<Contact, List<Call>> contacts)
        {
            PrintAllContacts(contacts);
            Console.Write("Kojem kontaktu želite ispisati pozive?");
            var number = UserStringInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
            foreach (var contact in contacts)
            {
                if (contact.Key.PhoneNumber == number)
                {
                    SetCallsThatEndedIntoAppropriateState(contacts);
                    SortedPrint(contact.Value);
                }
            }
        }
        static void AddNewCall(Dictionary<Contact, List<Call>> contacts)
        {
            PrintAllContacts(contacts);
            Console.WriteLine("Odaberite kontakta kojeg želite nazvati: ");
            var number = UserStringInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
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
        static void PrintAllCalls(Dictionary<Contact, List<Call>> contacts)
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


        static void SortedPrint(List<Call> calls)
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
        static bool DuplicatePhoneNumberExists(string number, Dictionary<Contact, List<Call>> contacts)
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
        static void SortDatesUp(List<DateTime> list)
        {
            var newList = new List<DateTime>();
            while(list.Count > 0)
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
        static bool CheckForOngoingCalls(Dictionary<Contact, List<Call>> contacts)
        {
            foreach(var contact in contacts)
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
        static void SetCallsThatEndedIntoAppropriateState(Dictionary<Contact, List<Call>> contacts)
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