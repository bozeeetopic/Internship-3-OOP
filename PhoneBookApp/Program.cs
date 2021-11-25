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
            Mainfunction(contacts);
               
        }
        static void Mainfunction(Dictionary<Contact, List<Call>> contacts)
        {
            while(true)
            {
                HelpingFunctions.PrintMenu();
                var userNumberInput = NumberInput("vaš izbor", 1, 8);
                var menuChoice = (Enums.Enums.MenuChoice)userNumberInput;
                switch (menuChoice)
                {
                    case Enums.Enums.MenuChoice.Print:
                        {
                            PrintAllContacts(contacts);
                            break;
                        }
                    case Enums.Enums.MenuChoice.Add:
                        {
                            AddNewContact(contacts);
                            break;
                        }
                    case Enums.Enums.MenuChoice.Erase:
                        {
                            EraseContacts(contacts);
                            break;
                        }
                    case Enums.Enums.MenuChoice.EditPrefs:
                        {
                            EditContactPreference(contacts);
                            break;
                        }
                    case Enums.Enums.MenuChoice.CallsFunctions:
                        {

                            HelpingFunctions.PrintSubMenu();
                            userNumberInput = NumberInput("vaš izbor", 1, 4);
                            var submenuChoice = (Enums.Enums.SubMenuChoice)userNumberInput;
                            switch (submenuChoice)
                            {
                                case Enums.Enums.SubMenuChoice.PrintSorted:
                                    {
                                        PrintAllCallsFromContactSorted(contacts);
                                        break;
                                    }
                                case Enums.Enums.SubMenuChoice.AddNew:
                                    {
                                        AddNewCall(contacts);
                                        break;
                                    }
                                case Enums.Enums.SubMenuChoice.Exit:
                                    {
                                        Mainfunction(contacts);
                                        return;
                                    }
                                default:
                                    Console.WriteLine("Nepostojeći izbor!");
                                    break;
                            }
                            break;
                        }
                    case Enums.Enums.MenuChoice.PrintCalls:
                        {
                            PrintAllCalls(contacts);
                            break;
                        }
                    case Enums.Enums.MenuChoice.Exit:
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
        static bool StringContainsString(string a, string b)
        {
            foreach (var character in b)
            {
                if (a.Contains(character)) 
                {
                    return true;
                }
            }
            return false;
        }
        static string NameOrSurnameInput(string nameOrSurname, string forbiddenString, int minLength)
        {
            var repeatedInput = false;
            var input = "";
            do
            {
                if (repeatedInput && (input.Length < minLength))
                {
                    Console.WriteLine("Duljina " + nameOrSurname + "na mora biti " + minLength + "!");
                }
                if (repeatedInput && StringContainsString(input.ToLower(), forbiddenString))
                {
                    Console.WriteLine(nameOrSurname + " sadrži znak, moraju biti isključivo brojevi!");
                }
                Console.Write("Stanovnikovo " + nameOrSurname + ": ");
                input = Console.ReadLine();        
                Console.WriteLine();
                repeatedInput = true;
            }
            while ((input.Length < minLength) || StringContainsString(input.ToLower(), forbiddenString));
            return input;
        }
        static int NumberInput(string message, int minValue, int maxValue)
        {
            var repeatedInput = false;
            int number;
            do
            {
                if (repeatedInput)
                {
                    Console.WriteLine("Morate unjeti broj između " + minValue + " i " + (maxValue - 1) + ".");
                }
                Console.Write("Unesite " + message + ": ");
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Pogrešan unos, brojeve samo!");
                }
                repeatedInput = true;
            }
            while (number >= maxValue || number < minValue);
            return number;
        }
        static Dictionary<Contact, List<Call>> PopulateContacts()
        {

            return new Dictionary<Contact, List<Call>>
            {
            {PopulateContact("Ivo Sanader","1",0),new List<Call>(){PopulateCall(DateTime.Now,0,45), PopulateCall(DateTime.Now, 0,20) } }
            };
        }
        static Contact PopulateContact(string name, string phone, Enums.Enums.PreferenceType preference)
        {
            var contact = new Contact();
            contact.AddValue(name, phone, preference);
            return contact;
        }
        static Call PopulateCall(DateTime time, Enums.Enums.Status status, int duration)
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
            var name = NameOrSurnameInput("ime", "",1);
            var surname = NameOrSurnameInput("prezime", "",0);
            var number="";
            do
            {
                if(DuplicatePhoneNumberExists(number, contacts))
                {
                    Console.WriteLine("Već postoji kontakt sa tim brojem telefona, pokušajte ponovno!");
                }
                number = NameOrSurnameInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
            }
            while (DuplicatePhoneNumberExists(number, contacts));
            Console.WriteLine("Unesi koja je ovo vrsta kontakta, za favorit 0, za default 1 te za blokirati broj upišite 2.");
            var preference = NumberInput("vaš odabir",0,3);
            var myPreference = (Enums.Enums.PreferenceType)preference;
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
                var eraseAnotherConfirm = "";
                do
                {
                    var keyFound = false;
                    PrintAllContacts(contacts);
                    Console.Write("Kojeg kontakta želite izbrisati?");
                    var number = NameOrSurnameInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
                    foreach (var contact in contacts)
                    {
                        if (contact.Key.PhoneNumber == number)
                        {
                            Console.WriteLine("Želite li sigurno izbrisati kontakta: " + contact.Key.ToString() + " (da)?");
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
                        eraseAnotherConfirm = Console.ReadLine();
                    }
                }
                while (eraseAnotherConfirm.Equals("da") || (contacts.Count == 0));
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
                var number = NameOrSurnameInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
                foreach (var contact in contacts)
                {
                    if (contact.Key.PhoneNumber == number)
                    {
                        Console.WriteLine("Nova preferenca:, za favorit 0, za default 1 te za blokirati broj upišite 2.");
                        var preference = NumberInput("vaš odabir", 0, 3);
                        var myPreference = (Enums.Enums.PreferenceType)preference;
                        Console.WriteLine("Želite li sigurno mijenjati kontaktu: " + contact.Key.ToString() + " preferencu u: " + myPreference + "(da)?");
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
            if (contacts.Count == 0)
            {
                Console.WriteLine("Lista kontakata prazna, ne postoji kontakt s kojim ste imali pozive!");
                return;
            }
            else
            {
                PrintAllContacts(contacts);
                Console.Write("Kojem kontaktu želite ispisati pozive?");
                var number = NameOrSurnameInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
                foreach (var contact in contacts)
                {
                    if (contact.Key.PhoneNumber == number)
                    {
                        SortedPrint(contact.Value);
                    }
                }
            }
        }
        static void AddNewCall(Dictionary<Contact, List<Call>> contacts)
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("Lista kontakata prazna, ne postoji kontakt kojeg možete nazvati!");
                return;
            }
            else
            {
                PrintAllContacts(contacts);
                Console.WriteLine("Odaberite kontakta kojeg želite nazvati: ");
                var number = NameOrSurnameInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
                foreach (var contact in contacts)
                {
                    if (contact.Key.PhoneNumber == number)
                    {
                        if (contact.Key.Preference == Enums.Enums.PreferenceType.Blocked)
                        {
                            Console.WriteLine("Kontakt blokiran, nemoguće obaviti poziv!");
                            return;
                        }
                        else
                        {
                            if (CheckForOngoingCalls(contacts))
                            {
                                Console.WriteLine("Nemoguće pozvati drugu osobu jer ste trenutno u pozivu!");
                                return;
                            }
                            else
                            {
                                switch ((Enums.Enums.Status)HelpingFunctions.RandomCallReply)
                                {
                                    case Enums.Enums.Status.Missed:
                                        {
                                            contact.Value.Add(PopulateCall(DateTime.Now, Enums.Enums.Status.Missed, 0));
                                            break;
                                        }
                                    case Enums.Enums.Status.Ongoing:
                                        {
                                            contact.Value.Add(PopulateCall(DateTime.Now, Enums.Enums.Status.Ongoing, HelpingFunctions.RandomCallDuration));
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
                foreach (var contact in contacts)
                {
                    if (contact.Value is null)
                    {
                    }
                    else
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
                        foreach (var call in newCallsList)
                        {
                            if (call.TimeOfCall.Equals(date))
                            {
                                Console.WriteLine(call.ToString());
                                newCallsList.Remove(call);
                            }
                        }
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
                        if (call.CallStatus == Enums.Enums.Status.Ongoing)
                        {
                            if (call.TimeOfCall.AddSeconds(call.Duration) < DateTime.Now)
                            {
                                call.CallStatus = Enums.Enums.Status.Finished;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}