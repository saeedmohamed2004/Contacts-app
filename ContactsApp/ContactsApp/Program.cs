using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp
{
    //saeed mohamed
    internal class Program
    {
        static string[][] contacts = new string[100][];
        static string[][] favorites = new string[100][];
        static string[][] emergencyContacts = new string[100][];
        static int contactCount = 0;
        static int favoriteCount = 0;
        static int emergencyCount = 0;

        static void Main(string[] args)
        {
            int choice;

            do
            {
                Console.WriteLine("1- Add a contact");
                Console.WriteLine("2- View all contacts");
                Console.WriteLine("3- Search for a contact");
                Console.WriteLine("4- Edit a contact");
                Console.WriteLine("5- Delete a contact");
                Console.WriteLine("6- Add to favorites");
                Console.WriteLine("7- Remove from favorites");
                Console.WriteLine("8- View favorites");
                Console.WriteLine("9- Add to emergency list");
                Console.WriteLine("10- View emergency list");
                Console.WriteLine("11- Exit");
                Console.Write("Enter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddContact();
                        break;
                    case 2:
                        ShowContacts();
                        break;
                    case 3:
                        SearchContact();
                        break;
                    case 4:
                        EditContact();
                        break;
                    case 5:
                        DeleteContact();
                        break;
                    case 6:
                        AddToFavorites();
                        break;
                    case 7:
                        RemoveFromFavorites();
                        break;
                    case 8:
                        ShowFavorites();
                        break;
                    case 9:
                        AddToEmergency();
                        break;
                    case 10:
                        ShowEmergencyContacts();
                        break;
                    case 11:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            } while (choice != 11);
        }

        // Method to add a contact
        static void AddContact()
        {
            if (contactCount >= 100)
            {
                Console.WriteLine("Contact list is full.\n");
                return;
            }

            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            List<string> phoneNumbers = new List<string>();

            while (true)
            {
                Console.Write("Enter phone number (or leave empty to stop): ");
                string phoneNumber = Console.ReadLine();
                if (string.IsNullOrEmpty(phoneNumber))
                    break;

                phoneNumbers.Add(phoneNumber);
            }

            contacts[contactCount] = new string[] { name, string.Join(";", phoneNumbers) };
            contactCount++;

            Console.WriteLine("Contact added successfully.\n");
        }

        // Method to show all contacts
        static void ShowContacts()
        {
            Console.WriteLine("\nContacts:");
            for (int i = 0; i < contactCount; i++)
            {
                string name = contacts[i][0];
                string[] phoneNumbers = contacts[i][1].Split(';');
                Console.WriteLine($"Name: {name}, Phone numbers: {string.Join(", ", phoneNumbers)}");
            }
            Console.WriteLine();
        }

        // Method to search for a contact
        static void SearchContact()
        {
            Console.Write("Enter name or phone number to search: ");
            string searchTerm = Console.ReadLine();

            for (int i = 0; i < contactCount; i++)
            {
                string name = contacts[i][0];
                string[] phoneNumbers = contacts[i][1].Split(';');

                bool found = false;
                if (name.ToLower().Contains(searchTerm.ToLower()))
                {
                    found = true;
                }
                else
                {
                    for (int j = 0; j < phoneNumbers.Length; j++)
                    {
                        if (phoneNumbers[j].Contains(searchTerm))
                        {
                            found = true;
                            break;
                        }
                    }
                }

                if (found)
                {
                    Console.WriteLine($"Name: {name}, Phone numbers: {string.Join(", ", phoneNumbers)}");
                }
            }
        }

        // Method to edit a contact
        static void EditContact()
        {
            Console.Write("Enter name to edit contact: ");
            string searchTerm = Console.ReadLine();

            for (int i = 0; i < contactCount; i++)
            {
                if (contacts[i][0].ToLower().Contains(searchTerm.ToLower()))
                {
                    Console.Write("Enter new name (or leave empty to keep current name): ");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        contacts[i][0] = newName;
                    }

                    Console.WriteLine("Enter new phone numbers:");
                    List<string> newPhoneNumbers = new List<string>();

                    while (true)
                    {
                        Console.Write("Enter phone number (or leave empty to stop): ");
                        string newPhone = Console.ReadLine();
                        if (string.IsNullOrEmpty(newPhone))
                            break;

                        newPhoneNumbers.Add(newPhone);
                    }

                    if (newPhoneNumbers.Count > 0)
                    {
                        contacts[i][1] = string.Join(";", newPhoneNumbers);
                    }

                    Console.WriteLine("Contact edited successfully.\n");
                    return;
                }
            }
        }

        // Method to delete a contact
        static void DeleteContact()
        {
            Console.Write("Enter name to delete contact: ");
            string searchTerm = Console.ReadLine();

            for (int i = 0; i < contactCount; i++)
            {
                if (contacts[i][0].ToLower().Contains(searchTerm.ToLower()))
                {
                    for (int j = i; j < contactCount - 1; j++)
                    {
                        contacts[j] = contacts[j + 1];
                    }
                    contacts[--contactCount] = null; 
                    Console.WriteLine("Contact deleted successfully.\n");
                    return;
                }
            }
        }

        // Method to add a contact to favorites
        static void AddToFavorites()
        {
            Console.Write("Enter name to add to favorites: ");
            string searchTerm = Console.ReadLine();

            for (int i = 0; i < contactCount; i++)
            {
                if (contacts[i][0].ToLower().Contains(searchTerm.ToLower()) && favoriteCount < 100)
                {
                    favorites[favoriteCount++] = contacts[i];
                    Console.WriteLine("Contact added to favorites.\n");
                    return;
                }
            }
        }

        // Method to remove a contact from favorites
        static void RemoveFromFavorites()
        {
            Console.Write("Enter name to remove from favorites: ");
            string searchTerm = Console.ReadLine();

            for (int i = 0; i < favoriteCount; i++)
            {
                if (favorites[i][0].ToLower().Contains(searchTerm.ToLower()))
                {
                    for (int j = i; j < favoriteCount - 1; j++)
                    {
                        favorites[j] = favorites[j + 1];
                    }
                    favorites[--favoriteCount] = null; 
                    Console.WriteLine("Contact removed from favorites.\n");
                    return;
                }
            }
        }

        // Method to show favorites
        static void ShowFavorites()
        {
            Console.WriteLine("\nFavorite Contacts:");
            for (int i = 0; i < favoriteCount; i++)
            {
                string name = favorites[i][0];
                string[] phoneNumbers = favorites[i][1].Split(';');
                Console.WriteLine($"Name: {name}, Phone numbers: {string.Join(", ", phoneNumbers)}");
            }
            Console.WriteLine();
        }

        // Method to add a contact to the emergency list
        static void AddToEmergency()
        {
            Console.Write("Enter name to add to emergency contacts: ");
            string searchTerm = Console.ReadLine();

            for (int i = 0; i < contactCount; i++)
            {
                if (contacts[i][0].ToLower().Contains(searchTerm.ToLower()) && emergencyCount < 100)
                {
                    emergencyContacts[emergencyCount++] = contacts[i];
                    Console.WriteLine("Contact added to emergency contacts.\n");
                    return;
                }
            }
        }

        // Method to show emergency contacts
        static void ShowEmergencyContacts()
        {
            Console.WriteLine("\nEmergency Contacts:");
            for (int i = 0; i < emergencyCount; i++)
            {
                string name = emergencyContacts[i][0];
                string[] phoneNumbers = emergencyContacts[i][1].Split(';');
                Console.WriteLine($"Name: {name}, Phone numbers: {string.Join(", ", phoneNumbers)}");
            }
            Console.WriteLine();
        }
    }



}
