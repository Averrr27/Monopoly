using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public List<Property> Properties { get; set; }
        private Random random;

        public Game()
        {
            Players = new List<Player>
            {

            };

            Properties = new List<Property>
        {
                // 0-Puste
                // 1-zwykła własność
                // 2-Los
                // 3-kurort
                // 4-tax
                // 5-Jail
                // 6-Darmowa kaska
            new Property("Start", 0, 1, 0, 0),
            new Property("Białystok", 60, 2, 1, 0),
            new Property("Darmowe pieniądze", 0, 3, 6, 0),
            new Property("Suwałki", 60, 4, 1, 0),
            new Property("Tax", 0, 5, 4, 0),
            new Property("Kurort Świnoujście", 200, 6, 3, 0),
            new Property("Opole", 100, 7, 1, 0),
            new Property("Los", 0, 8, 2, 0),
            new Property("Kędzierzyn-Koźle", 100, 9, 1, 0),
            new Property("Nysa", 120, 10, 1, 0),
            new Property("Puste", 0, 11, 0, 0),
            new Property("Zielona Góra", 140, 12, 1, 0),
            new Property("Los", 0, 13, 2, 0),
            new Property("Gorzów Wielkopolski", 140, 14, 1, 0),
            new Property("Nowa Sól", 160, 15, 1, 0),
            new Property("Kurort Kołobrzeg", 200, 16, 3, 0),
            new Property("Kielce", 180, 17, 1, 0),
            new Property("Los", 0, 18, 2, 0),
            new Property("Ostrowiec Świętokrzyski", 180, 19, 1, 0),
            new Property("Starachowice", 200, 20, 1, 0),
            new Property("Puste", 0, 21, 0, 0),
            new Property("Olsztyn", 220, 22, 1, 0),
            new Property("Los", 0, 23, 2, 0),
            new Property("Elbląg", 220, 24, 1, 0),
            new Property("Ełk", 240, 25, 1, 0),
            new Property("Kurort Hel", 200, 26, 3, 0),
            new Property("Tarnów", 260, 27, 1, 0),
            new Property("Nowy Sącz", 260, 28, 1, 0),
            new Property("Los", 0, 29, 2, 0),
            new Property("Kraków", 280, 30, 1, 0),
            new Property("JAIL", 0, 31, 5, 0),
            new Property("Częstochowa", 300, 32, 1, 0),
            new Property("Rybnik", 300, 33, 1, 0),
            new Property("Darmowe pieniądze", 0, 34, 6, 0),
            new Property("Katowice", 320, 35, 1, 0),
            new Property("Kurort Krynica Morska", 200, 36, 3, 0),
            new Property("Los", 0, 37, 2, 0),
            new Property("Radom", 350, 38, 1, 0),
            new Property("Tax", 0, 39, 4, 0),
            new Property("Warszawa", 400, 40, 1, 0),
        };

            random = new Random();
        }

        public void AddPlayers()
        {
            int n;
            string temp;
            Console.WriteLine("Podaj ilość graczy: ");
            n = int.Parse(Console.ReadLine());
            if (n < 2)
            {
                while (n < 2)
                {
                    Console.WriteLine("Za mała ilość graczy, wybierz przynajmniej dwóch");
                    n = int.Parse(Console.ReadLine());
                }
            }
            else
            {
                for (int i = 1; i <= n; i++)
                {
                    Console.Write($"Podaj nazwę {i} gracza: ");
                    temp = Console.ReadLine();
                    Players.Add(new Player(temp));
                    Console.WriteLine($"Gracz {i} został pomyślnie dodany! ");
                }
            }
        }

        public void PlayRound(Game game)
        {
            if (Players.Count() != 1)
            {
                foreach (var player in Players)
                {
                    if (player.Money > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        int roll = random.Next(1, 7) + random.Next(1, 7);
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine($"{player.Name} Nacisnij dowolny przycisk aby rzucic kostka");
                        Console.ReadKey(true);
                        Console.WriteLine("\n\t");
                        player.Move(roll);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        Console.WriteLine($"{player.Name} rzucił {roll} i jest na pozycji {player.Position}. \n" +
                            $"Pieniądze: {player.Money}");
                        Console.WriteLine("-------------------------------------");
                        Properties[player.Position].Options(player, game);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"\n \n \tNiestety {player.Name}, Przegrałeś! \n \n \t");
                        Players.Remove(player);
                        break;
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"GRATULACJE {Players[0]} WYGRAŁEŚ!");
                Console.WriteLine($"Kończe program!");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
