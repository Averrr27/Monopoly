using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Property
    {
        private Random Los_random;
        private Random Loteria_random;
        private bool exit;
        public string karta;
        public string Name { get; set; }
        public double Price { get; set; }
        private double basePrice;
        public Player Owner { get; set; }
        public int Position_on_board { get; set; }
        public int Type_of_card { get; set; }
        public int Buildings { get; set; }
        public int Los_roll { get; set; }
        public Property(string name, double price, int position_on_board, int type_of_card, int buildings)
        {
            Name = name;
            Price = price;
            basePrice = price;
            Position_on_board = position_on_board;
            Type_of_card = type_of_card;
            Buildings = buildings;
            Owner = null;

            Los_random = new Random();
            Loteria_random = new Random();
        }
        public void Wygrana(Player player)
        {
            Console.WriteLine($"\n \t GRATULACJE {player} WYGRAŁEŚ GRĘ!!!! \n \t");
            Console.WriteLine("\n-------------------------------------\n");
            Console.ReadKey();
            Environment.Exit(0);
        }
        public void showProperty()
        {
            if (Type_of_card == 1)
                karta = "Mieszkanie";
            else
                karta = "Inne";
            Console.WriteLine($" Pozycja: {Position_on_board}\n Nazwa: {Name}\n Domki: {Buildings}\n Cena: {Price}\n Typ karty: {karta}  \n {Owner}");
        }
        public void Options(Player player, Game game)
        {
            exit = false;
            while (!exit)
            {
                if (player.Cooldown != 0)
                {
                    Console.WriteLine($"Jesteś w więzieniu, twoja tura przepada");
                    player.Cooldown--;
                    break;
                }
                else
                {
                    if (Type_of_card == 1 || Type_of_card == 3)
                    {
                        if (Owner != null && Owner != player)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\nCzyjeś pole! płacisz: {Price * 0.2}");
                            player.Money -= Price * 0.2;
                            Owner.Money += Price * 0.2;
                            Console.WriteLine($"Aktualny stan konta: {player.Money} \n");
                            Console.ForegroundColor = ConsoleColor.White;
                            propertyOptions(player, game);
                        }
                        else
                        {
                            propertyOptions(player, game);
                        }

                    }
                    else if (Type_of_card == 0)
                    {
                        Console.WriteLine($"Wylądowałeś na pustym polu widoków, na tym polu nie możesz nic zrobić \n");
                        break;
                    }
                    else if (Type_of_card == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Wylądowałeś na polu Los! \n" +
                            "Wylosujemy kwote między -200 / 200 i dodamy ją do twojego konta!");
                        int Los_roll = Los_random.Next(-200, 200) + Los_random.Next(-200, 200);
                        player.Money += Los_roll;
                        Console.WriteLine($"Aktualny stan konta: {player.Money}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                    else if (Type_of_card == 3)
                    {
                        Console.WriteLine("Wylądowałeś na malowniczym kurorcie! Zbierz wszystkie 4 aby wygrać grę!");
                        propertyOptions(player, game);
                    }
                    else if (Type_of_card == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        player.Money -= player.Money * 0.2;
                        Console.WriteLine($"Wylądowałeś na polu Tax\n " +
                            $"Teraz odejmiemy 20% z twoich aktualnych pieniędzy \n "
                            + $"Aktualny stan konta: {player.Money}");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                    else if (Type_of_card == 5)
                    {
                        Console.WriteLine($"IDZIESZ DO WIĘZIENIA!, przez kolejne 2 tury nie możesz zrobić ruchu, oraz wracasz na pozycję 11");
                        player.Position = 11;
                        player.Cooldown += 2;
                        break;
                    }
                    else if (Type_of_card == 6)
                    {
                        Console.WriteLine($"Wylądowałeś na polu Loterii! \n" +
                            "Wylosujemy kwote między 0 / 200 i dodamy ją do twojego konta!");
                        int Loteria_roll = Loteria_random.Next(0, 200) + Loteria_random.Next(0, 200);
                        player.Money += Loteria_roll;
                        Console.WriteLine($"Aktualny stan konta: {player.Money}");
                        break;
                    }
                }
            }
        }
        public void propertyOptions(Player player, Game game)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Wylądowałeś na polu {Name}, Właściciel: {Owner} \n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"{player.Name}, wybierz opcję: \n\n" +
                                                      $"1. Zakończ kolejkę\n" +
                                                      $"2. Wyświetl informacje o polu\n" +
                                                      $"3. Wyświetl mapę jako wektor\n" +
                                                      $"4. Zakup pole \n" +
                                                      $"5. Zakup Domek \n" +
                                                      $"6. Sprzedaj pole \n" +
                                                      $"7. Zamknij grę");
            Console.WriteLine("\n-------------------------------------\n");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Kolejka zakończona.");
                        Console.Clear();
                        exit = true;
                        break;
                    case 2:
                        Console.Clear();
                        showProperty();
                        break;
                    case 3:
                        Console.Clear();
                        foreach (var properties in game.Properties)
                        {
                            Console.WriteLine($"Pole {properties.Position_on_board}, Nazwa: {properties.Name}, Właściciel: {properties.Owner}, Ilość domków: {properties.Buildings}");
                        }
                        Console.WriteLine("\n");
                        break;
                    case 4:
                        Console.Clear();
                        if (Owner == null && (player.Money - Price >= 0))
                        {
                            if (Type_of_card == 3)
                            {
                                Console.WriteLine("Zakupiono pole.");
                                Console.WriteLine("\n-------------------------------------\n");
                                player.Kurorty++;
                                Owner = player;
                                player.Money -= Price;
                                if (player.Kurorty == 4)
                                {
                                    Wygrana(player);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Zakupiono pole.");
                                Console.WriteLine("\n-------------------------------------\n");
                                Owner = player;
                                player.Money -= Price;
                            }
                        }
                        else
                        {
                            Console.WriteLine("To pole jest już kupione, lub nie masz wystarczająco pieniędzy");
                            Console.WriteLine("\n-------------------------------------\n");
                        }
                        break;
                    case 5:
                        Console.Clear();
                        if (Owner == player && player.Tura >=2)
                        {
                            Console.WriteLine($"Cena domku: {basePrice * 0.1}, jeden domek zwiększa wartość pola o 20% bazowej wartości (max 4)");
                            for (int i = 1; i <= 4; i++)
                            {
                                Console.WriteLine($"Cena {i} domku/ów: {basePrice * (i * 0.1)}");
                            }
                            Console.WriteLine("\n");
                            Console.WriteLine($"Wybierz opcję: \n" +
                                $"1.Kup 1 domek, \n" +
                                $"2.Kup 2 domki, \n" +
                                $"3.Kup 3 domki, \n" +
                                $"4.Kup 4 domki, \n" +
                                $"5.Wyjście \n ");
                            string buildingsInput = Console.ReadLine();
                            if (int.TryParse(buildingsInput, out int buildingsChoice))
                            {
                                switch (buildingsChoice)
                                {
                                    case 1:
                                        if (Buildings + 1 > 4 && (player.Money>basePrice * 0.2))
                                        {
                                            Console.WriteLine("Za dużo domków, lub za mało pieniędzy. przerywam");
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Zakupiono 1 domek");
                                            Price += basePrice * 0.2;
                                            player.Money -= basePrice * 0.2;
                                            Buildings++;
                                            break;
                                        }
                                    case 2:
                                        if (Buildings + 2 > 4 && (player.Money > basePrice * 0.4))
                                        {
                                            Console.WriteLine("Za dużo domków, lub za mało pieniędzy. przerywam");
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Zakupiono 2 domki");
                                            Price += basePrice * 0.4;
                                            player.Money -= basePrice * 0.4;
                                            Buildings +=2;
                                            break;
                                        }
                                    case 3:
                                        if (Buildings + 3 > 4 && (player.Money > basePrice * 0.6))
                                        {
                                            Console.WriteLine("Za dużo domków, lub za mało pieniędzy. przerywam");
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Zakupiono 3 domki");
                                            Price += basePrice * 0.6;
                                            player.Money -= basePrice * 0.6;
                                            Buildings +=3;
                                            break;
                                        }
                                    case 4:
                                        if (Buildings + 4 != 4 && (player.Money > basePrice * 0.8))
                                        {
                                            Console.WriteLine("Za dużo domków, lub za mało pieniędzy. przerywam");
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Zakupiono 4 domki");
                                            Price += basePrice * 0.8;
                                            player.Money -= basePrice * 0.8;
                                            Buildings +=4;
                                            break;
                                        }
                                    case 5:
                                        {
                                            Console.WriteLine($"Wyjście");
                                            break;
                                        }
                                    default:
                                        Console.WriteLine($"Zła operacja, przerywam");
                                        break;
                                }
                            }
                            else
                                Console.WriteLine($"Zła operacja, przerywam \n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Domek można kupić dopiero od 2 przejścia przez start! \n");
                            break;
                        }
                    case 6:
                        Console.Clear();
                        if (Owner != null && Owner == player)
                        {
                            Console.WriteLine("Pole zostało sprzedane.");
                            Console.WriteLine("\n-------------------------------------\n");
                            Owner = null;
                            player.Money += (Price - 40);
                        }
                        else
                        {
                            Console.WriteLine("Nie masz tego pola.");
                            Console.WriteLine("\n-------------------------------------\n");
                        }
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Gra zamknięta.");
                        Console.WriteLine("\n-------------------------------------\n");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Niepoprawna opcja.");
                        Console.WriteLine("\n-------------------------------------\n");
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Niepoprawna opcja.");
                Console.WriteLine("\n-------------------------------------\n");
            }
        }
    }
}
