using System.Collections.Generic;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

namespace ContactsList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\DataContact\";
            string fileContact = "contacts.csv";
            string fileAddress = "address.csv";
            string fileTelephone = "telephones.csv";
            CreateDirectoryAndFiles(path, fileContact, fileAddress, fileTelephone);

            List<Contact> list = LoadData(path, fileContact, fileAddress, fileTelephone);

            while (true)
            {
                int option = Menu();
                list.Sort(new CompareName());

                switch (option)
                {
                    case 1:
                        AddContact(list);
                        break;
                    case 2:
                        RemoveContact(list);
                        break;
                    case 3:
                        EditContact(list);
                        break;
                    case 4:
                        ListAllUsers(list);
                        break;
                    case 5:
                        ListContactByName(list);
                        break;
                    case 0:
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
                SaveData(list, path, fileContact, fileAddress, fileTelephone);

                list.Sort(new CompareName());
                Console.WriteLine("========================");
                Console.Write("Press aky key to continue...");
                Console.ReadKey();
            }
        }

        static int Menu()
        {
            Console.Clear();
            Console.WriteLine("======Contact List======");

            Console.WriteLine("Options: ");
            Console.WriteLine("1- Add new contact");
            Console.WriteLine("2- Remove a contact");
            Console.WriteLine("3- Edit a contact");
            Console.WriteLine("4- List all contacts");
            Console.WriteLine("5- List a specific contact");
            Console.WriteLine("0- Exit");
            Console.Write("R: ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("You must enter a number!");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
                return Menu();
            }

            return option;
        }

        static void AddContact(List<Contact> list)
        {
            Console.Clear();
            Console.WriteLine("==Insert a new contact==");

            string name = ReadString("Name: ");
            string email = ReadString("Email: ");
            Address address = CreateAddress();

            Contact aux = new(name, address, email);
            address.UserId = aux.Id;

            int number = ReadInt("Enter how many telephones: ", "You must enter a number!");
            for (int i = 0; i < number; i++)
            {
                string phone = ReadString($"Telephone {i + 1}: ");
                aux.TelephoneList.Add(new Telephone(aux.Id, phone));
            }

            list.Add(aux);
            Console.WriteLine("Contact added!");
        }
        static List<Contact> LoadData(string path, string fileContact, string fileAddress, string fileTelephone)
        {
            List<Contact> list = new();

            foreach (var line in File.ReadAllLines(path + fileContact))
            {
                string[] fields = line.Split(";");

                int id = int.Parse(fields[0]);
                string fullName = fields[1];
                string email = fields[2];

                Contact c = new(id, fullName, email);
                list.Add(c);

                // adding address to each contact
                foreach (var lineAddress in File.ReadAllLines(path + fileAddress))
                {
                    string[] addressFields = lineAddress.Split(";");
                    int userId = int.Parse(addressFields[0]);

                    string postalCode = addressFields[1];
                    string city = addressFields[2];
                    string state = addressFields[3];
                    string street = addressFields[4];
                    string streetType = addressFields[5];
                    string district = addressFields[6];
                    int number = int.Parse(addressFields[7]);
                    string additionalAddress = addressFields[8];

                    if (c.Id == userId)
                    {
                        c.Address = new Address(userId, postalCode, city, state, street, streetType, district, number, additionalAddress);
                        break;
                    }
                }
                foreach (var linePhone in File.ReadAllLines(path + fileTelephone))
                {
                    string[] phoneFields = linePhone.Split(";");
                    // return $"{UserId};{Number}";
                    int userId = int.Parse(phoneFields[0]);
                    string number = phoneFields[1];

                    if (c.Id == userId)
                    {
                        c.TelephoneList.Add(new Telephone(userId, number));
                    }
                }
            }
            Contact.count = list.Count;
            return list;
        }
        static void SaveData(List<Contact> list, string path, string fileContact, string fileAddress, string fileTelephone)
        {
            // Opening stream channels
            var swContact = new StreamWriter(path + fileContact);
            var swAddress = new StreamWriter(path + fileAddress);
            var swTelephone = new StreamWriter(path + fileTelephone);

            foreach (var item in list)
            {
                swContact.WriteLine(item.ToString());

                swAddress.WriteLine(item.Address.ToString());

                foreach (var telephone in item.TelephoneList)
                {
                    swTelephone.WriteLine(telephone.ToString());
                }
            }

            // closing stream channels
            swContact.Close();
            swAddress.Close();
            swTelephone.Close();
        }

        static void PrintNames(List<Contact> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine($"-->{item.FullName}");
            }
        }

        static void RemoveContact(List<Contact> list)
        {
            Console.Clear();
            Console.WriteLine("====Remove a contact====");

            PrintNames(list);
            Console.WriteLine("========================");

            string name = ReadString("Name: ");

            Contact? aux = GetContactByName(list, name);

            if (aux != null)
            {
                list.Remove(aux);
                Console.WriteLine("Contact removed!");
            }
            else
            {
                Console.WriteLine("There is no contact with this name!");
            }
        }

        static void EditContact(List<Contact> list)
        {
            Console.Clear();
            Console.WriteLine("======Edit contact======");

            PrintNames(list);
            Console.WriteLine("========================");

            string name = ReadString("Enter name: ");
            Contact? contact = GetContactByName(list, name);

            if (contact == null)
            {
                Console.WriteLine("There is no contact with this name!");
                return;
            }

            list.Remove(contact);

            bool end = false;
            while (!end)
            {
                int option = MenuEdit();

                switch (option)
                {
                    case 1: // change name
                        contact.FullName = ReadString("New name: ");
                        break;
                    case 2:// change email
                        contact.Email = ReadString("New email: ");
                        break;
                    case 3: // Add new phone number
                        Telephone newNumber = new(contact.Id, ReadString("New phone: "));
                        contact.TelephoneList.Add(newNumber);
                        break;
                    case 4: // Remove phone number
                        string number = ReadString("Phone number you want to remove: ");
                        Telephone? removed = null;
                        foreach (var item in contact.TelephoneList)
                        {
                            if (item.Number.Equals(number))
                                removed = item;
                        }
                        if (removed != null)
                            contact.TelephoneList.Remove(removed);
                        break;
                    case 5: // change address
                        Address aux = CreateAddress();
                        aux.UserId = contact.Id;
                        contact.Address = aux;
                        break;
                    case 0:
                        Console.WriteLine("Your edited contact: ");
                        Console.WriteLine(contact.GetFullDescription());
                        list.Add(contact);
                        end = true;
                        break;
                    default:
                        break;
                }
            }
        }

        static void ListContactByName(List<Contact> list)
        {
            Console.Clear();
            Console.WriteLine("==List Contact by name==");

            PrintNames(list);
            Console.WriteLine("========================");

            string name = ReadString("Contact name you want to find: ");

            Contact? contact = GetContactByName(list, name);

            if (contact != null)
            {
                Console.WriteLine(contact.GetFullDescription());
            }
            else
            {
                Console.WriteLine("There is no contact with this name!");
            }
        }

        static Address CreateAddress()
        {
            Console.WriteLine("Creating Address:");

            string postalCode = ReadString("Postal code: ");
            string city = ReadString("City: ");

            string state = ReadString("State: ");
            string street = ReadString("Street: ");

            string streetType = ReadString("Street Type: ");
            string district = ReadString("District: ");

            int number = ReadInt("Number: ", "You must enter a number!");
            string additionalAddress = ReadString("Additional Address: ");

            return new Address(postalCode, city, state, street, streetType, district, number, additionalAddress);
        }

        static Contact? GetContactByName(List<Contact> list, string name)
        {
            Contact? contact = null;

            foreach (var item in list)
            {
                if (item.FullName.Equals(name))
                {
                    contact = item;
                }
            }

            return contact;
        }

        static int MenuEdit()
        {
            Console.WriteLine("======Edit options======");

            Console.WriteLine("Options: ");
            Console.WriteLine("1- Change name");
            Console.WriteLine("2- Change email");
            Console.WriteLine("3- Add new phone number");
            Console.WriteLine("4- Remove phone");
            Console.WriteLine("5- Change Address");
            Console.WriteLine("0- Exit");
            Console.Write("R: ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("You must enter a number!");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
                return Menu();
            }

            return option;
        }

        static string ReadString(string str)
        {
            Console.Write(str);
            return Console.ReadLine();
        }

        static int ReadInt(string str, string alert)
        {
            Console.Write(str);
            int number;
            while (!int.TryParse(Console.ReadLine(), out number) || number <= 0)
            {
                Console.WriteLine(alert);
                Console.Write(str);
            }

            return number;
        }

        static void CreateDirectoryAndFiles(string path, string fileContact, string fileAddress, string fileTelephone)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(path + fileContact))
            {
                var file = File.Create(path + fileContact);
                file.Close();
            }

            if (!File.Exists(path + fileAddress))
            {
                var file = File.Create(path + fileAddress);
                file.Close();
            }

            if (!File.Exists(path + fileTelephone))
            {
                var file = File.Create(path + fileTelephone);
                file.Close();
            }
        }

        static void ListAllUsers(List<Contact> list)
        {
            Console.Clear();
            Console.WriteLine("List:");
            Console.WriteLine("========================");

            if (list.Count == 0)
            {
                Console.WriteLine("List is empty!");
            }
            else
            {
                int count = 0;
                foreach (var item in list)
                {
                    Console.WriteLine($"Contact {++count}:");
                    Console.WriteLine(item.GetFullDescription());
                }
            }
        }
    }
}
