namespace ContactsList
{
    internal class Contact
    {
        public string FullName { get; set; }
        public Address Address { get; set; }

        public string Email { get; set; }

        public TelephoneList TelephoneList { get; set; }

        public Contact? Next { get; set; }

        public Contact(string fullName, Address address, string email)
        {
            this.FullName = fullName;
            this.Address = address;
            this.Email = email;
            this.TelephoneList = new TelephoneList();
            this.Next = null;
        }

        public override string? ToString()
        {
            string text;

            text =  $"Full Name.........: {FullName}\n";
            text += $"Email.............: {Email}\n";
            text += $"Address: {Address}\n";
            text += TelephoneList.ToString();
            return text;
        }
    }
}
