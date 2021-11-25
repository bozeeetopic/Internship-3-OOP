using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookApp.Enums
{
    class Enums
    {
        public enum Status
        {
            Traje,
            Propušten,
            Završen
        }
        public enum PreferenceType
        {
            Favorit,
            Regularni,
            Blokiran
        }
        public enum MenuChoice
        {
            Print,
            Add,
            Erase,
            EditPrefs,
            CallsFunctions,
            PrintCalls,
            Exit
        }
        public enum SubMenuChoice
        {
            PrintSorted,
            AddNew,
            Exit
        }
    }
}
