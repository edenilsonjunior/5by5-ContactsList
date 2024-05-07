using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContactsList
{
    internal class ContactList
    {
        private Contact? _head;
        private Contact? _tail;

        public ContactList()
        {
            this._head = null;
            this._tail = null;
        }

        public void Add(Contact contact)
        {
            if (IsEmpty())
            {
                this._head = contact;
                this._tail = contact;
            }
            else
            {
                int compare = string.Compare(contact.FullName, _head.FullName, comparisonType: StringComparison.OrdinalIgnoreCase);

                if (compare <= 0)
                {
                    Contact aux = _head;
                    this._head = contact;
                    _head.Next = aux;
                }
                else
                {
                    Contact aux = _head;
                    Contact prev = _head;

                    do
                    {
                        compare = string.Compare(contact.FullName, aux.FullName);
                        if (compare > 0)
                        {
                            prev = aux;
                            aux = aux.Next;
                        }

                    } while (aux != null && compare > 0);

                    prev.Next = contact;
                    contact.Next = aux;

                    if (aux == null)
                    {
                        this._tail = contact;
                    }
                }
            }
        }

        public bool RemoveByName(string name)
        {
            // check if the list is empty
            if (IsEmpty())
            {
                return false;
            }

            // check if the correspondent name is in head
            if (_head.FullName.Equals(name))
            {
                _head = _head.Next;

                if (_head == null)
                    _tail = null;

                return true;
            }

            Contact aux = _head;
            Contact prev = _head;

            bool equals;
            do
            {
                equals = aux.FullName.Equals(name);

                if (!equals)
                {
                    prev = aux;
                    aux = aux.Next;
                }

            } while (aux != null && !equals);

            if (equals)
            {
                prev.Next = (aux.Next);

                if (prev.Next == null)
                    _tail = prev;

                return true;
            }

            return false;
        }

        public void Print()
        {
            Console.WriteLine("List:");
            Console.WriteLine("========================");

            if (!IsEmpty())
            {
                Contact aux = _head;

                do
                {
                    Console.WriteLine(aux);
                    aux = aux.Next;
                } while (aux != null);
            }
            else
            {
                Console.WriteLine("List is empty!");
            }
        }

        public bool Contains(string name)
        {
            if (IsEmpty())
            {
                return false;
            }

            Contact aux = _head;
            Contact prev = _head;

            bool equals;
            do
            {
                equals = aux.FullName.Equals(name);

                if (!equals)
                {
                    prev = aux;
                    aux = aux.Next;
                }
                else
                {
                    return true;
                }

            } while (aux != null && !equals);


            return false;
        }

        public void PrintNames()
        {
            Console.WriteLine("List:");

            if (IsEmpty())
            {
                Console.WriteLine("List is empty!");
                return;
            }

            Contact aux = _head;
            do
            {
                Console.WriteLine("========================");
                Console.WriteLine($"--> {aux.FullName}");
                aux = aux.Next;
            } while (aux != null);
        }

        public Contact? GetUserByName(string name)
        {
            if (!IsEmpty())
            {
                Contact aux = _head;
                do
                {

                    if (aux.FullName.Equals(name))
                    {
                        return aux;
                    }
                    aux = aux.Next;
                } while (aux != null);
            }
            return null;
        }

        bool IsEmpty()
        {
            return _head == null && _tail == null;
        }

    }
}