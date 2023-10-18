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
            var contacts = Actions.Actions.PopulateContacts();
            MainFunction(contacts);
               
        }
        static void MainFunction(Dictionary<Contact, List<Call>> contacts)
        {
            while(true)
            {
                Console.Clear();
                HelpingFunctions.PrintMenu(contacts.Count);
                var userNumberInput = HelpingFunctions.UserNumberInput("vaš izbor", 1, 7);
                var menuChoice = (Enums.MenuChoice)userNumberInput;
                switch (menuChoice)
                {
                    case Enums.MenuChoice.Print:
                        {
                            Actions.Actions.PrintAllContacts(contacts);
                            break;
                        }
                    case Enums.MenuChoice.Add:
                        {
                            Actions.Actions.AddNewContact(contacts);
                            break;
                        }
                    case Enums.MenuChoice.Erase:
                        {
                            Actions.Actions.EraseContacts(contacts);
                            break;
                        }
                    case Enums.MenuChoice.EditPrefs:
                        {
                            Actions.Actions.EditContactPreference(contacts);
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
                                userNumberInput = HelpingFunctions.UserNumberInput("vaš izbor", 1, 3);
                                var submenuChoice = (Enums.SubMenuChoice)userNumberInput;
                                switch (submenuChoice)
                                {
                                    case Enums.SubMenuChoice.PrintSorted:
                                        {
                                            Actions.Actions.PrintAllCallsFromContactSorted(contacts);
                                            break;
                                        }
                                    case Enums.SubMenuChoice.AddNew:
                                        {
                                            Actions.Actions.AddNewCall(contacts);
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
                            Actions.Actions.PrintAllCalls(contacts);
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

       
    }
}