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
