using ContactsList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TelephonesList
{
    internal class TelephoneList
    {
        private Telephone? _head;
        private Telephone? _tail;

        public TelephoneList()
        {
            _head = null;
            _tail = null;
        }

        public void Add(Telephone telephone)
        {
            if (IsEmpty())
            {
                _head = telephone;
                _tail = telephone;
            }
            else
            {
                int compare = string.Compare(telephone.Number, _head.Number);

                if (compare <= 0)
                {
                    Telephone aux = _head;
                    _head = telephone;
                    _head.Next = aux;
                }
                else
                {
                    Telephone aux = _head;
                    Telephone prev = _head;

                    do
                    {
                        compare = string.Compare(telephone.Number, aux.Number);
                        if (compare > 0)
                        {
                            prev = aux;
                            aux = aux.Next;
                        }

                    } while (aux != null && compare > 0);

                    prev.Next = telephone;
                    telephone.Next = aux;

                    if (aux == null)
                    {
                        _tail = telephone;
                    }
                }
            }
        }

        public bool RemoveByNumber(string number)
        {
            // check if the list is empty
            if (IsEmpty())
            {
                return false;
            }

            // check if the correspondent number is in _head
            if (_head.Number.Equals(number))
            {
                _head = _head.Next;

                if (_head == null)
                    _tail = null;

                return true;
            }

            Telephone aux = _head;
            Telephone prev = _head;

            bool equals;
            do
            {
                equals = aux.Number.Equals(number);

                if (!equals)
                {
                    prev = aux;
                    aux = aux.Next;
                }

            } while (aux != null && !equals);

            if (equals)
            {
                prev.Next = aux.Next;

                if (prev.Next == null)
                    _tail = prev;

                return true;
            }

            return false;
        }

        public void Print()
        {
            Console.WriteLine("List:");

            if (!IsEmpty())
            {
                Telephone aux = _head;

                do
                {
                    Console.WriteLine($"-->{aux}");
                    aux = aux.Next;
                } while (aux != null);
            }
            else
            {
                Console.WriteLine("List is empty!");
            }
            Console.WriteLine("===================");
        }


        bool IsEmpty()
        {
            return _head == null && _tail == null;
        }

    }
}