using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookApp.Enums
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
        Print = 1,
        Add = 2,
        Erase = 3,
        EditPrefs = 4,
        CallsFunctions = 5,
        PrintCalls = 6,
        Exit = 7
    }
    public enum SubMenuChoice
    {
        PrintSorted = 1,
        AddNew = 2,
        Exit = 3
    }
}
