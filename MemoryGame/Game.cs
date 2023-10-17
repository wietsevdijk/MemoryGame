using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public static class Game
    {
        private static Random rng = new Random();
        private static Card[] cardArray;

        private static int _tries = 0;

        private static bool _complete = false;
        public static bool Complete { get => _complete; }
        public static int Tries { get => _tries; }

        private static int _matches = 0;

        //Returns array of shuffled cards
        public static void InitializeCards(int amountCardPairs) {
            Card[] cards = new Card[amountCardPairs * 2];

            int cardValue = 0;
            for (int i = 0; i < amountCardPairs * 2; i++)
            {
                cards[i] = new Card(cardValue);
                if (!(i % 2 == 0)) cardValue++;
            }

            rng.Shuffle(cards);

            cardArray = cards;        
        }

        public static void ShowCard(int cardPos) {
            Console.WriteLine($"Kaart {cardPos} is {cardArray[cardPos-1].Value}");
        }

        public static void CompareCards(int pos1, int pos2) { 
            _tries++;

            bool match = cardArray[pos1 - 1].Value == cardArray[pos2 - 1].Value ? true : false;

            if (match)
            {
                cardArray[pos1 - 1].Discovered = true;
                cardArray[pos2 - 1].Discovered = true;
                _matches++;

                Console.WriteLine("Match!");

                if (_matches == cardArray.Length / 2) { _complete = true; }
            }
            else {
                Console.WriteLine("Geen match");
            }
        }

        public static bool GetDiscovered(int cardPos) {
            if (cardArray[cardPos-1].Discovered) return true;

            return false;
        }

        //Print alle kaarten ondersteboven, "O" = normaal, X = "ontdekt"
        public static void PrintCards() { 
            for (int i = 0; i < cardArray.Length; i++) {
                string flipped = cardArray[i].Discovered ? "X" : "O";
                Console.Write($"{flipped} ");
            }
            Console.WriteLine();
        }




        //DEBUG: print all card values
        public static void PrintCardValues()
        {
            for (int i = 0; i < cardArray.Length; i++)
            {
                Console.Write($"{cardArray[i].Value} ");
            }
            Console.WriteLine();
        }

        //Used for shuffling cards in InitializeCards
        private static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

    }
}
