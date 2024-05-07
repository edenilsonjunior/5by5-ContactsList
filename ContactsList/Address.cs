using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsList
{
    internal class Address
    {

        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string StreetType { get; set; }
        public string District { get; set; }
        public int Number { get; set; }
        public string AdditionalAddress { get; set; }

        public Address(string postalCode, string city, string state, string street, string streetType, string district, int number, string additionalAddress)
        {
            PostalCode = postalCode;
            City = city;
            State = state;
            Street = street;
            StreetType = streetType;
            District = district;
            Number = number;
            AdditionalAddress = additionalAddress;
        }

        public override string? ToString()
        {

            string texto = "\n" +
            $"\tPostal Code.......: {PostalCode}\n" +
            $"\tCity..............: {City}\n" +
            $"\tState.............: {State}\n" +
            $"\tStreet............: {StreetType} {Street}\n" +
            $"\tDistrict..........: {District}\n" +
            $"\tNumber............: {Number}\n";
            if (AdditionalAddress != null)
            {
                texto += $"\tAdditional Address: {AdditionalAddress}";
            }
            return texto;
        }
    }
}
