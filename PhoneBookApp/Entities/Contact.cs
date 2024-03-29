﻿namespace PhoneBookApp.Entities
{
    class Contact
    {
        public string NameAndSurname { get; set; }
        public string PhoneNumber { get; set; }
        public Enums.PreferenceType Preference { get; set; }
        public Contact AddValue(string nameAndSurname, string phoneNumber, Enums.PreferenceType preference)
        {
            NameAndSurname = nameAndSurname;
            PhoneNumber = phoneNumber;
            Preference = preference;
            return this;
        }


        public override string ToString() => $"Kontakt: {NameAndSurname}\t Broj: {PhoneNumber}  Tip: {Preference}";
    }
}
