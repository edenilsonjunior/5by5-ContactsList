namespace ContactsList
{
    internal class Telephone
    {
        public int UserId { get; set; }
        public string Number { get; set; }
        
        public Telephone(int userId, string number)
        {
            UserId = userId;
            Number = number;
        }

        public override string? ToString()
        {
            return $"{UserId};{Number}";
        }
    }
}
