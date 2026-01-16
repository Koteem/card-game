namespace proj
{
    class Game
    {
        private Player[] players;
        private Deck gameDeck;
        public Game(int playerAmount, Deck d)
        {
            players = new Player[playerAmount];
            gameDeck = d;
        }
        public void start()
        {
            gameDeck.shuffle();
            int i = 0;
            while(gameDeck.checkUpperCard() != "-1")
            { 
                players[i].drawCard(gameDeck.drawCard());
                i++;
                if(i >= players.Length)
                {
                    i = 0;
                }
            }
        }
    }
    class Player
    {
        private string name;
        public Deck playerDeck;
        public Player(string n, Deck d)
        {
            name = n;
            playerDeck = d;
        }
        public void drawCard(string c)
        {
            playerDeck.addCard(c);
        }
    }
    class Deck
    {
        private Card [] card;

        public Deck() // creates a standard deck of 52 playing cards
        {
            card = new Card[52];
            int index = 0;
            char[] suits = { 'H', 'D', 'C', 'S' };
            for (int s = 0; s < suits.Length; s++)
            {
                for (int v = 1; v <= 13; v++)
                {
                    card[index] = new Card(suits[s] + " " + v);
                    index++;
                }
            }
        }
        public Deck(int n) // creates a deck of n random playing cards (with possible duplicates)
        {
            card = new Card[n];
            Random rand = new Random();
            char[] suits = { 'H', 'D', 'C', 'S' };
            for (int i = 0; i < n; i++)
            {
                char suit = suits[rand.Next(0, 4)];
                int value = rand.Next(1, 14);
                card[i] = new Card(suit + " " + value);
            }
        }
        public Deck(string filename) // creates a deck of playing cards read from a text file
        {
            string[] lines = File.ReadAllLines(filename);
            card = new Card[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                card[i] = new Card(lines[i]);
            }
        }
        public void addCard(string c)
        {
            Card newCard = new Card(c);
            Card[] newDeck = new Card[card.Length + 1];
            for (int i = 0; i < card.Length; i++)
            {
                newDeck[i] = card[i];
            }
            newDeck[card.Length] = newCard;
            card = newDeck;
        }
        public string checkUpperCard()
        {
            if(card.Length == 0)
            {
                return "-1";
            }
            return card[0].ToString();
        }
        public string drawCard()
        {
            if (card.Length > 0)
            {
                string topCard = card[0].ToString();
                Card[] newDeck = new Card[card.Length - 1];
                for (int i = 1; i < card.Length; i++)
                {
                    newDeck[i - 1] = card[i];
                }
                card = newDeck;
                return topCard;
            }
            else
            {
                return "-1";
            }
        }
        public void shuffle()
        {
            Random rand = new Random();
            for (int i = 0; i < card.Length; i++)
            {
                int j = rand.Next(0, card.Length);
                Card temp = card[i];
                card[i] = card[j];
                card[j] = temp;
            }
        }
        public override string ToString()
        {
            string deckString = "";
            for (int i = 0; i < card.Length; i++)
            {
                deckString += card[i].ToString() + "\n";
            }
            return deckString;
        }
    }
    class Card
    {
        private char suit; // H - hearts, D - diamonds, C - clubs, S - spades,
        private int value; // A - 1, ..., J - 11, Q - 12, K - 13,

        public Card(string n)//input like: H 10, D 1, S 12
        {
            string[] x = n.Trim().Split(' ');
            suit = x[0][0];
            value = int.Parse(x[1]);
            if (suit != 'H' && suit != 'D' && suit != 'C' && suit != 'S')// if values are incorrect, set to default values (Ace of Spades)
            {
                suit = 'S';
            }
            if (value < 1 || value > 13)
            {
                value = 1;
            }           
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
            Deck deck = new Deck();
            Console.WriteLine(deck);
        }
    }
}
