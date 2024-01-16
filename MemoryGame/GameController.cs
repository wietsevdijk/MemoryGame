using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame {
    public class GameController {
        private Random rng = new Random();
        private Stopwatch sw = new Stopwatch();
        private Game? currentGame = null;

        public Game InitializeGame(int amountCardPairs, string playerName) {
            Card[] cards = InitializeCards(amountCardPairs);
            Game game = new Game(cards, playerName);

            currentGame = game;
            sw.Start();

            return game;
        }

        //Returns array of shuffled cards
        public Card[] InitializeCards(int amountCardPairs) {
            Card[] cards = new Card[amountCardPairs * 2];

            // Build array of cards with values 0 to amountCardPairs * 2
            int cardValue = 0;
            for (int i = 0; i < amountCardPairs * 2; i++) {
                cards[i] = new Card(cardValue);
                if (!(i % 2 == 0)) cardValue++;
            }

            // Shuffle card array
            Shuffle(rng, cards);

            return cards;
        }

        public bool FlipUpCard(int cardPos) {
            CheckIfGameActive();

            // Check if card is already flipped up, or has been discovered
            if (currentGame.GetFlipped(cardPos) || currentGame.GetDiscovered(cardPos)) {
                return false;
            }

            // Flip the card up
            currentGame.CardArray[cardPos - 1].Flipped = true;

            return true;
        }

        public bool FlipDownCard(int cardPos) {
            CheckIfGameActive();

            // Check if card is already flipped down, or has been discovered
            if (!currentGame.GetFlipped(cardPos) || currentGame.GetDiscovered(cardPos)) {
                return false;
            }

            // Flip the card down
            currentGame.CardArray[cardPos - 1].Flipped = false;

            return true;
        }

        public bool CompareCards(int pos1, int pos2) {
            CheckIfGameActive();

            // Check if positions are the same
            if (pos1 == pos2) {
                return false;
            }

            // Check if cards are already discovered
            if (currentGame.GetDiscovered(pos1) || currentGame.GetDiscovered(pos2)) {
                return false;
            }


            // Check if card values match
            bool match = currentGame.GetValue(pos1) == currentGame.GetValue(pos2) ? true : false;

            if (match) {
                currentGame.CardArray[pos1 - 1].Discovered = true;
                currentGame.CardArray[pos2 - 1].Discovered = true;

                // Check if all cards are discovered, and mark game as complete
                CheckIfGameFinished();
            }

            return match;
        }

        // Print all cards face down, "O" = undiscovered, X = "discovered"
        public void PrintCards() {
            CheckIfGameActive();

            for (int i = 0; i < currentGame.CardArray.Length; i++) {
                string flipped = currentGame.CardArray[i].Discovered ? "X" : "O";
                Console.Write($"{flipped} ");
            }
            Console.WriteLine();
        }

        // DEBUG: Print all card values / cards face up
        public void PrintCardsWithValues() {
            CheckIfGameActive();

            Console.WriteLine("VALUES:\n-----");
            for (int i = 0; i < currentGame.CardArray.Length; i++) {
                Console.Write($"{currentGame.CardArray[i].Value} ");
            }
            Console.WriteLine("-----");
        }


        private void CheckIfGameFinished() {
            if (!currentGame.Complete && currentGame.Matches == currentGame.CardArray.Length / 2) {
                currentGame.Complete = true;
                sw.Stop();
                // TO-DO: Event (indien nodig)
            }
        }

        private void CheckIfGameActive() {
            if (currentGame == null) {
                // TO-DO: Dit kan beter afgehandeld worden
                throw new Exception("No game active!");
            }
        }

        // Used for shuffling cards in InitializeCards
        private static void Shuffle<T>(Random rng, T[] array) {
            int n = array.Length;
            while (n > 1) {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}
