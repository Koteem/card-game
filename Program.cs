namespace proj
{
    class Game
    {
        private Player[] players;
        private Deck gameDeck;
        public Game(int playerAmount, Deck d)
        {
            players = new Player[playerAmount];
            for(int i = 0; i < playerAmount; i++)
            {
                players[i] = new Player("Player " + (i + 1), new Deck(0));
            }
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
        public void playRound()
        {
            foreach (Player p in players)
            {
                Console.WriteLine(p.ToString());
            }
            int round = 1;
            while (true)
            {
                Deck deck = new Deck(0);
                Console.WriteLine("\n Round " + round + "\n");
                round++;
                int[] values = new int[players.Length];
                for (int i = 0; i < players.Length; i++)
                {
                    string card = players[i].playerDeck.drawCard();
                    if (card == "-1")
                    {
                        values[i] = -1;
                    }
                    else
                    {
                        deck.addCard(card);
                        string[] parts = card.Split(' ');
                        values[i] = int.Parse(parts[1]);
                        Console.WriteLine(players[i].name + " draws " + card);
                    }
                }
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].playerDeck.checkUpperCard() == "-1")
                    {
                        Console.WriteLine("\n" + players[i].name + " losses!");
                        return;
                    }
                }
                int maxValue = -1;
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] > maxValue)
                    {
                        maxValue = values[i];
                    }
                }
                int howManyWinners = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] == maxValue)
                    {
                        howManyWinners++;
                    }
                }
                while (howManyWinners > 1)
                {
                    int[] tab = values;
                    Console.WriteLine("\nIt's a tie between:");
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (values[i] == maxValue)
                        {
                            Console.WriteLine(players[i].name);
                        }
                        else
                        {
                            values[i] = 0;
                        }
                    }
                    Console.WriteLine("Drawing again...\n");
                    howManyWinners = 0;
                    for (int i = 0; i < players.Length; i++)
                    {
                        if (values[i] == maxValue)
                        {
                            if (players[i].playerDeck.checkUpperCard() != "-1")
                            {
                                string t = players[i].playerDeck.drawCard();
                                deck.addCard(t);
                                string[] parts = t.Split(' ');
                                tab[i] = int.Parse(parts[1]);
                                Console.WriteLine(players[i].name + " draws " + parts[0] + " " + parts[1]);
                            }
                            if (players[i].playerDeck.checkUpperCard() != "-1") 
                            {
                                string t = players[i].playerDeck.drawCard();
                                deck.addCard(t);
                                string[] parts = t.Split(' ');
                                tab[i] = int.Parse(parts[1]);
                                Console.WriteLine(players[i].name + " draws " + parts[0] + " " + parts[1]);
                            }
                        }
                    }
                    for (int i = 0; i < tab.Length; i++)
                    {
                        if (tab[i] > maxValue)
                        {
                            maxValue = tab[i];
                        }
                    }
                    howManyWinners = 0;
                    for (int i = 0; i < tab.Length; i++)
                    {
                        if (tab[i] == maxValue)
                        {
                            howManyWinners++;
                        }
                    }
                    Console.WriteLine();
                    values = tab;
                }
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] == maxValue)
                    {
                        Console.WriteLine(players[i].name + " wins the round!\n");
                        while(deck.checkUpperCard() != "-1")
                        {
                            players[i].drawCard(deck.drawCard());
                        }
                    }
                }
            }
        }
    }
    class Player
    {
        public string name;
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
        public override string ToString()
        {
            return name + "'s deck:\n" + playerDeck.ToString();
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
                for (int v = 2; v <= 14; v++)
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
                int value = rand.Next(2, 15);
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
                string topCard = card[0].getCard();
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
        private int value; // 2 - 2, ..., J - 11, Q - 12, K - 13, A - 14

        public Card(string n)//input like: H 10, D 2, S 12
        {
            string[] x = n.Trim().Split(' ');
            suit = x[0][0];
            value = int.Parse(x[1]);
            if (suit != 'H' && suit != 'D' && suit != 'C' && suit != 'S')// if values are incorrect, set to default values (Ace of Spades)
            {
                suit = 'S';
            }
            if (value < 2 || value > 14)
            {
                value = 2;
            }           
        }
        public string getCard()
        {
            return suit + " " + value;
        }
        public override string ToString()
        {
            string name = "";
            switch(value)
            {
                case 14:
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
            Game g = new Game(4, new Deck());
            g.start();
            g.playRound();
        }
    }
}
