using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Player
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public double Money { get; set; }
        public int Kurorty { get; set; }
        public int Cooldown { get; set; }
        public int Tura { get; set; }

        public Player(string name)
        {
            Name = name;
            Position = 0;
            Money = 1000;
            Kurorty = 0;
            Cooldown = 0;
            Tura = 0;
        }

        public void Move(int spaces)
        {
            Position += spaces;
            if (Position > 39)
            {
                Position -= 40;
                Money += 100;
                Tura++;
                Console.WriteLine($"{Name} przeszedł przez start i otrzymał 200$.");
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
