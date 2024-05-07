using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsList
{
    // This class implements interface IComparer, that requires to implement the method "Compare"
    // The purpose of this class is to sort the Contact List
    internal class CompareName : IComparer<Contact>
    {
        public int Compare(Contact c1, Contact c2)
        {
            return string.Compare(c1.FullName, c2.FullName);
        }
    }
}
