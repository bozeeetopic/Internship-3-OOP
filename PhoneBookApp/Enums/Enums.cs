using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookApp.Enums
{
    class Enums
    {
        public enum Status
        {
            Ongoing,
            Missed,
            Finished
        }
        public enum PreferenceType
        {
            Favourite,
            Normal,
            Blocked
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
