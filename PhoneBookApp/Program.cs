﻿using System;
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
                switch (userNumberInput)
                {
                    case 1:
                        {
                            PrintAllContacts(contacts);
                            break;
                        }
                    case 2:
                        {
                            AddNewContact(contacts);
                            break;
                        }
                    case 3:
                        {
                            EraseContacts(contacts);
                            break;
                        }
                    case 4:
                        {
                            EditContactPreference(contacts);
                            break;
                        }
                    case 5:
                        {
                            HelpingFunctions.PrintSubMenu();
                            userNumberInput = NumberInput("vaš izbor", 0, 4);
                            switch (userNumberInput)
                            {
                                case 0:
                                    {
                                        PrintAllCallsFromContactSorted(contacts);
                                        break;
                                    }
                                case 1:
                                    {
                                        AddNewCall(contacts);
                                        break;
                                    }
                                case 2:
                                    {
                                        Mainfunction(contacts);
                                        break;
                                    }
                                default:
                                    Console.WriteLine("Nepostojeći izbor!");
                                    break;
                            }
                            break;
                        }
                    case 6:
                        {
                            PrintAllCalls(contacts);
                            break;
                        }
                    case 7:
                        {
                            Console.WriteLine("Hvala na korištenju!");
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
        //Mislio zakomplicirat unos al se sitio da ima i 95,911 itd
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
        }*/
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
            foreach(var contact in contacts)
            {
                Console.WriteLine(contact.Key.ToString());
            }
        }
        static void AddNewContact(Dictionary<Contact, List<Call>> contacts)
        {
            Enums.Enums.PreferenceType myStatus = Enums.Enums.PreferenceType.Normal;
            var name = NameOrSurnameInput("ime", "",1);
            var surname = NameOrSurnameInput("prezime", "",0);
            var number = NameOrSurnameInput("broj", HelpingFunctions.allLettersAndCharacters ,1);
            Console.WriteLine("Unesi koja je ovo vrsta kontakta, za favorit 0, za default 1 te za blokirati broj upišite 2.");
            var status = NumberInput("vaš odabir",0,3);
            if (Enum.IsDefined(typeof(Enums.Enums.PreferenceType), status))
            {
                myStatus = (Enums.Enums.PreferenceType)status;
            }
            contacts.Add(PopulateContact(name + " " + surname, number, myStatus), null);
        }
        static void EraseContacts(Dictionary<Contact, List<Call>> contacts)
        {
            var eraseAnotherConfirm = "";
            var keyFound = false;
            do
            {
                PrintAllContacts(contacts);
                Console.Write("Kojeg kontakta želite izbrisati?");
                var number = NameOrSurnameInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
                foreach(var contact in contacts)
                {
                    if (!(contact.Key._PhoneNumber == number))
                    {
                    }
                    else
                    {
                        Console.WriteLine("Želite li sigurno izbrisati kontakta: " + contact.Key.ToString() + " (da)?");
                        var eraseConfirm = Console.ReadLine();
                        if (eraseConfirm is "da")
                        {
                            if(!(contact.Value is null))
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
        static void EditContactPreference(Dictionary<Contact, List<Call>> contacts)
        {
            PrintAllContacts(contacts);
            Console.Write("Kojem kontaktu želite mjenjati preferencu?");
            var number = NameOrSurnameInput("broj", HelpingFunctions.allLettersAndCharacters, 1);
            foreach (var contact in contacts)
            {
                if (!(contact.Key._PhoneNumber == number))
                {
                }
                else
                {
                    var myStatus = contact.Key._Preference;
                    Console.WriteLine("Nova preferenca:, za favorit 0, za default 1 te za blokirati broj upišite 2.");
                    var status = NumberInput("vaš odabir", 0, 3);
                    if (Enum.IsDefined(typeof(Enums.Enums.PreferenceType), status))
                    {
                        myStatus = (Enums.Enums.PreferenceType)status;
                    }
                    Console.WriteLine("Želite li sigurno mijenjati kontaktu: " + contact.Key.ToString() + " preferencu u: "+ myStatus +"?");
                    string eraseConfirm = Console.ReadLine();
                    if (eraseConfirm == "da")
                    {
                        contacts.Add(PopulateContact(contact.Key._NameAndSurname, contact.Key._PhoneNumber, myStatus),contact.Value);
                        contacts.Remove(contact.Key);
                        return;
                    }
                }
            }
        }
        static void PrintAllCallsFromContactSorted(Dictionary<Contact, List<Call>> contacts)
        {
            PrintAllContacts(contacts);
            Console.Write("Kojem kontaktu želite ispisati pozive?");
            var number = NameOrSurnameInput("broj", HelpingFunctions.allLettersAndCharacters , 1);
            foreach (var contact in contacts)
            {
                if (contact.Key._PhoneNumber == number)
                {
                    SortedPrint(contact.Value);
                }
            }
        }
        static void AddNewCall(Dictionary<Contact, List<Call>> contacts) { }
        static void PrintAllCalls(Dictionary<Contact, List<Call>> contacts)
        {
            foreach (var contact in contacts)
            {
                foreach(var call in contact.Value)
                    Console.WriteLine(call.ToString());
            }
        }




        static void SortedPrint(List<Call> calls)
        {
            var newCallsList = new List<Call>();
        }
    }
}
