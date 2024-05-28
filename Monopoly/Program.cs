using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Program
    {
        public static void Main()
        {
            bool win= false;
            Game game = new Game();
            game.AddPlayers();
            do
            {
                game.PlayRound(game);
            } while (win != true);
            Console.ReadKey();
        }
    }
}
