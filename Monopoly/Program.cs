using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Player
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public int Money { get; set; }

        public Player(string name)
        {
            Name = name;
            Position = 0;
            Money = 1500;
        }

        public void Move(int spaces)
        {
            Position += spaces;
            if (Position > 39)
            {
                Position -= 40;
                Money += 200;
                Console.WriteLine($"{Name} przeszedł przez start i otrzymał 200$.");
            }
        }
    }

    public class Property
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Player Owner { get; set; }

        public Property(string name, int price)
        {
            Name = name;
            Price = price;
            Owner = null;
        }
    }

    public class Game
    {
        public List<Player> Players { get; set; }
        public List<Property> Properties { get; set; }
        private Random random;

        public Game()
        {
            Players = new List<Player>
        {
            new Player("Gracz 1"),
            new Player("Gracz 2"),
            new Player("Gracz 3")
        };

            Properties = new List<Property>
        {
            new Property("Nieruchomość 1", 100),
            new Property("Nieruchomość 2", 200),
            new Property("Nieruchomość 3", 300)
        };

            random = new Random();
        }
        public void addPlayers()
        {
            int n;
            Console.WriteLine("Podaj ilość graczy: ");
            Console.ReadKey();
        }
        public void PlayRound()
        {
            foreach (var player in Players)
            {
                int roll = random.Next(1, 7) + random.Next(1, 7);
                player.Move(roll);
                Console.WriteLine($"{player.Name} rzucił {roll} i jest na pozycji {player.Position}.");
                // Sprawdź, czy gracz jest na nieruchomości i czy może ją kupić
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            Game game = new Game();
            game.PlayRound();
            Console.ReadKey();
        }
    }
}
