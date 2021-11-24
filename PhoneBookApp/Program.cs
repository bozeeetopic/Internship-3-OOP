using System;
using System.Collections.Generic;
using PhoneBookApp.Entities;
using PhoneBookApp.Helpers;

namespace PhoneBookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var contacts = PopulateContacts();
            string inputToContinue;
            do
            {
                Mainfunction(contacts);
                Console.Write("Želite li još nešto napraviti? ");
                inputToContinue = Console.ReadLine();
            }
            while (inputToContinue.Equals("da"));
        }
    
        static void Mainfunction(Dictionary<Contact, List<Call>> contacts)
        {
            HelpingFunctions.PrintMenu();
            var userNumberInput = NumberInput("vaš izbor", 0, 11);
            switch (userNumberInput)
            {
                case 0:
                    PrintAllContacts(contacts);
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    HelpingFunctions.PrintSubMenu();
                    userNumberInput = NumberInput("vaš izbor", 0, 4);
                    switch (userNumberInput)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        default:
                            Console.WriteLine("Nepostojeći izbor!");
                            break;
                    }
                    break;
                case 6:
                    break;
                case 7:
                    break;
                default:
                    break;
            }
        }
        static int NumberInput(string message, int minValue, int maxValue)
        {
            var number = 0;
            var repeatedInput = false;
            do
            {
                if (repeatedInput) Console.WriteLine("Morate unjeti broj između " + minValue + " i " + maxValue + ".");
                Console.Write("Unesite " + message + ": ");
                if (!int.TryParse(Console.ReadLine(), out number)) Console.WriteLine("Pogrešan unos, brojeve samo!");
                repeatedInput = true;
            }
            while (number >= maxValue || number < minValue);
            return number;
        }
        static  Dictionary<Contact, List<Call>> PopulateContacts()
        {

            return new Dictionary<Contact, List<Call>>
            {
            {PopulateContact("Ivo Sanader","1",0),new List<Call>(){PopulateList(DateTime.Now,0,45), PopulateList(DateTime.Now, 0,20) } }
            };
        }
        static Contact PopulateContact(string name, string phone, Enums.Enums.PreferenceType preference)
        {
            var contact = new Contact();
            contact.AddValue(name, phone, preference);
            return contact;
        }
        static Call PopulateList(DateTime time, Enums.Enums.Status status,int duration)
        {
            var call = new Call();
            call.AddValue(time, status,duration);
            return call;
        }
    }
}
