using System.Runtime.CompilerServices;

namespace MemoryGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool debug = true;

            Console.WriteLine("Hoeveel paren aan kaarten wil je hebben?");
            int cardPairs = int.Parse(Console.ReadLine());

            Game.InitializeCards(cardPairs);
            if (debug) {
                Game.PrintCardValues();
            }

            //gameloop
            while (!Game.Complete)
            {
                Game.PrintCards();

                Console.WriteLine("Kies 1e kaart:");
                int i1 = int.Parse(Console.ReadLine());
                if (Game.GetDiscovered(i1)) {  
                    Console.WriteLine("Kaart 1 is al omgedraaid!");
                    continue;
                }
                Game.ShowCard(i1);

                Console.WriteLine("Kies 2e kaart:");
                int i2 = int.Parse(Console.ReadLine());
                if (Game.GetDiscovered(i2))
                {
                    Console.WriteLine("Kaart 2 is al omgedraaid!");
                    continue;
                }

                //Check of keuzes niet gelijk zijn
                if (i1 == i2)
                {
                    Console.WriteLine("Je kan niet dezelfde kaart kiezen!");
                    continue;
                }

                Game.ShowCard(i2);


                Game.CompareCards(i1, i2);
            }

            Console.WriteLine("\nGewonnen!");


        }
    }
}