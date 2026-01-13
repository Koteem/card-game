namespace proj
{
    class Card
    {
        public char suit { get; set; } // H - hearts, D - diamonds, C - clubs, S - spades,
        public int value { get; set; }// A - 1, ..., J - 11, Q - 12, K - 13,

        public Card(string n)//input like: H 10, D 1, S 12
        { 
            string[] x = n.Trim().Split(' ');
            suit = x[0][0];
            value = int.Parse(x[1]);
        }
        public override string ToString()
        {
            string name = "";
            switch(value)
            {
                case 1:
                    name = "Ace";
                    break;
                case 11:
                    name = "Jack";
                    break;
                case 12:
                    name = "Queen";
                    break;
                case 13:
                    name = "King";
                    break;
                default:
                    name = value.ToString();
                    break;
            }
            name += " of ";
            switch(suit)
            {
                case 'H':
                    name += "Hearts";
                    break;
                case 'D':
                    name += "Diamonds";
                    break;
                case 'C':
                    name += "Clubs";
                    break;
                case 'S':
                    name += "Spades";
                    break;
            }
            return name;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Card c = new Card("H 11");
            Console.WriteLine(c);
        }
    }
}
