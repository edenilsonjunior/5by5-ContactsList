namespace ContactsList
{
    internal class Telephone
    {
        public string Number { get; set; }
        public Telephone? Next { get; set; }

        public Telephone(string number)
        {
            Number = number;
            Next = null;
        }

        public override string? ToString()
        {
            return $"Telephone.........: {Number}";
        }
    }
}
