﻿using System.Runtime.CompilerServices;

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
                Game.ShowCard(i1);

                Console.WriteLine("Kies 2e kaart:");
                int i2 = int.Parse(Console.ReadLine());
                Game.ShowCard(i2);

                Game.CompareCards(i1, i2);
            }

            Console.WriteLine("\nGewonnen!");


        }
    }
}