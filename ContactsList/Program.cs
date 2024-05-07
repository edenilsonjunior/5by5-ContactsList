using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

namespace ContactsList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ContactList list = new ContactList();

            while (true)
            {
                int option = Menu();

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
                        list.Print();
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

        static void AddContact(ContactList list)
        {
            Console.Clear();
            Console.WriteLine("==Insert a new contact==");

            string name = ReadString("Name: ");
            string email = ReadString("Email: ");
            Address address = CreateAddress();

            Contact aux = new(name, address, email);

            int number = ReadInt("Enter how many telephones: ", "You must enter a number!");
            for (int i = 0; i < number; i++)
            {
                string phone = ReadString($"Telephone {i + 1}: ");
                aux.TelephoneList.Add(new Telephone(phone));
            }

            list.Add(aux);
            Console.WriteLine("Contact added!");
        }

        static void RemoveContact(ContactList list)
        {
            Console.Clear();
            Console.WriteLine("====Remove a contact====");

            list.PrintNames();
            Console.WriteLine("========================");

            string name = ReadString("Name: ");

            bool result = list.RemoveByName(name);

            if (!result)
                Console.WriteLine("There is no contact with this name!");
            else
                Console.WriteLine("Contact removed!");
        }

        static void EditContact(ContactList list)
        {
            Console.Clear();
            Console.WriteLine("======Edit contact======");

            list.PrintNames();
            Console.WriteLine("========================");

            string name = ReadString("Enter name: ");
            Contact? contact = list.GetUserByName(name);

            if (contact == null)
            {
                Console.WriteLine("There is no contact with this name!");
                return;
            }

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
                        Telephone newNumber = new(ReadString("New phone: "));
                        contact.TelephoneList.Add(newNumber);
                        break;
                    case 4: // Remove phone number
                        string number = ReadString("Phone number you want to remove: ");
                        contact.TelephoneList.RemoveByNumber(number);
                        break;
                    case 5: // change address
                        Address aux = CreateAddress();
                        contact.Address = aux;
                        break;
                    case 0:
                        Console.WriteLine("Your edited contact: ");
                        Console.WriteLine(contact);
                        end = true;
                        break;
                    default:
                        break;
                }
            }

        }

        static void ListContactByName(ContactList list)
        {
            Console.Clear();
            Console.WriteLine("==List Contact by name==");

            list.PrintNames();
            Console.WriteLine("========================");

            string name = ReadString("Contact name you want to find: ");

            Contact? contact = list.GetUserByName(name);

            if (contact != null)
            {
                Console.WriteLine(contact);
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
    }
}
