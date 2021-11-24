using System;
using System.Collections.Generic;
using System.Text;
using PhoneBookApp.Enums;

namespace PhoneBookApp.Entities
{
    class Contact
    {
        public string _NameAndSurname { get; set; }
        public string _PhoneNumber { get; set; }
        public Enums.Enums.PreferenceType _Preference { get; set; }
        public Contact AddValue(string NameAndSurname, string PhoneNumber, Enums.Enums.PreferenceType Preference)
        {
            _NameAndSurname = NameAndSurname;
            _PhoneNumber = PhoneNumber;
            _Preference = Preference;
            return this;
        }

        public static int Counter = 0;

        public override string ToString() => $"Contact: {_NameAndSurname}\tContact  number: {_PhoneNumber}  Type: {_Preference.ToString()}";
    }
}
