using System;
using System.Collections.Generic;
using System.Text;
using PhoneBookApp.Entities;
using PhoneBookApp.Enums;

namespace PhoneBookApp.Helpers
{
    static class HelpingFunctions
    {
        public static Random NumberGenerator = new Random();
        public static int RandomCallReply => NumberGenerator.Next(2);
        public static int RandomCallDuration => NumberGenerator.Next(1,20);

        public const string allLettersAndCharacters = "qwertzuiopšđžćčlkjhgfds ayxcvbnm,.-:;<>!#$%&/()=?*¸¨'";
        public static void PrintMenu()
        {
            Console.WriteLine("Odaberite akciju:");
            Console.WriteLine("1 - Ispis svih kontakata");
            Console.WriteLine("2 - Dodavanje novih kontakata u imenik");
            Console.WriteLine("3 - Brisanje kontakata iz imenika");
            Console.WriteLine("4 - Editiranje preference kontakta");
            Console.WriteLine("5 - Upravljanje kontaktom koje otvara podmenu");
            Console.WriteLine("6 - Ispis svih poziva");
            Console.WriteLine("7 - Izlaz iz aplikacije");
            Console.WriteLine();
        }
        public static void PrintSubMenu()
        {
            Console.WriteLine("Odaberite akciju:");
            Console.WriteLine("1 - Ispis svih poziva sa tim kontaktom poredan od vremenski najnovijeg prema najstarijem");
            Console.WriteLine("2 - Kreiranje novog poziva");
            Console.WriteLine("3 - Izlaz iz podmenua");
            Console.WriteLine();
        }
    }
}
