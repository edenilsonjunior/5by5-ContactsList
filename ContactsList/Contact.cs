namespace ContactsList
{
    internal class Contact
    {
        public static int count = 0;
        public int Id { get; set; }
        public string FullName { get; set; }
        public Address Address { get; set; }

        public string Email { get; set; }

        public List<Telephone> TelephoneList { get; set; }

        public Contact(string fullName, Address address, string email)
        {
            this.Id = ++count;
            this.FullName = fullName;
            this.Address = address;
            this.Email = email;
            this.TelephoneList = new();
        }

        public Contact(int id, string fullName, string email)
        {
            this.Id = id;
            this.FullName = fullName;
            this.Email = email;
            this.TelephoneList = new();
        }
        public string GetFullDescription()
        {
            string text;
                   
            text = $"Full Name....: {FullName}\n";
            text += $"Email........: {Email}\n";
            text += $"{Address.GetFullDescription()}";
                     
            if (TelephoneList.Count != 0)
            {
                text += "Telephones:\n";
                foreach (var telephone in TelephoneList)
                {
                    text += $"Telephone....: {telephone.Number}\n";
                }
            }
            return text;
        }

        public override string? ToString()
        {
            return $"{Id};{FullName};{Email}";

        }
    }
}
