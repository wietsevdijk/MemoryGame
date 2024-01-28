using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame {
    public class GameController {
        private Stopwatch sw = new Stopwatch();
        private Game? currentGame = null;

        private IGameRepository _gameRepository;
        public GameController(IGameRepository gameRepository) {
            _gameRepository = gameRepository;
        }

        public bool SaveGame() {
            CheckIfGameActive();
            bool gameSaved = false;

            // If less than 10 games have been uploaded, simply upload the current game
            if (_gameRepository.GetAll().Count < 10) {
                _gameRepository.Insert(currentGame);
                gameSaved = true;
            } else if (currentGame.Score > _gameRepository.GetAll().Min(g => g.Score)) {
            // If the current game has a higher score than the lowest score in the database, upload the current game and delete the lowest score
                _gameRepository.Insert(currentGame);
                _gameRepository.DeleteOldScores();
                gameSaved = true;
            }

            return gameSaved;
        }

        public ICollection<Game> GetHighScores() {
            return _gameRepository.GetAll().OrderByDescending(g => g.Score).ToList();
        }

        public Game InitializeGame(int amountCardPairs, string playerName) {
            Card[] cards = InitializeCards(amountCardPairs);
            Game game = new Game(cards, playerName);

            currentGame = game;
            sw.Start();

            return game;
        }

        //OVERLOAD: Used for custom images
        public Game InitializeGame(int amountCardPairs, string playerName, string[] imageFilePaths) {
            Card[] cards = InitializeCards(amountCardPairs, imageFilePaths);
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
                cards[i] = new Card(0, cardValue);
                if (!(i % 2 == 0)) cardValue++;
            }

            // Shuffle card array
            Shuffle(cards);

            // Attach position as property for use in GUI
            for (int i = 1; i <= cards.Length; i++) {
                cards[i - 1].Position = i;
            }

            return cards;
        }

        //OVERLOAD: Returns array of shuffled cards with images
        public Card[] InitializeCards(int amountCardPairs, string[] imageFilePaths) {
            if (imageFilePaths.Length < amountCardPairs) {
                throw new Exception("Not enough images provided!");
            }

            Card[] cards = new Card[amountCardPairs * 2];

            // Build array of cards with values 0 to amountCardPairs * 2
            int cardValue = 0;
            for (int i = 0; i < amountCardPairs * 2; i++) {
                cards[i] = new Card(0, cardValue, imageFilePaths[cardValue]);
                if (!(i % 2 == 0)) cardValue++;
            }

            // Shuffle card array
            Shuffle(cards);

            // Attach position as property for use in GUI
            for (int i = 1; i <= cards.Length; i++) {
                cards[i - 1].Position = i;
            }

            return cards;
        }

        public bool FlipCard(int cardPos) {
            CheckIfGameActive();

            // Check if card is already discovered
            if (currentGame.GetDiscovered(cardPos)) {
                return false;
            }

            // Flip the card
            currentGame.CardArray[cardPos - 1].Flipped = !currentGame.CardArray[cardPos - 1].Flipped;

            return true;
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

            // Increment amount of tries
            currentGame.Tries++;

            // Check if card values match
            bool match = currentGame.GetValue(pos1) == currentGame.GetValue(pos2) ? true : false;

            if (match) {
                // Mark cards as discovered
                currentGame.CardArray[pos1 - 1].Discovered = true;
                currentGame.CardArray[pos2 - 1].Discovered = true;

                // Increment amount of matches
                currentGame.Matches++;

                // Check if all cards are discovered, and mark game as complete
                CheckIfGameFinished();
            }

            return match;
        }

        // Print all cards face down, "O" = undiscovered, X = "discovered"
        public void PrintCards() {
            CheckIfGameActive();

            Console.Write("Kaarten:\n");
            for (int i = 0; i < currentGame.CardArray.Length; i++) {
                string flipped = currentGame.CardArray[i].Discovered ? "X" : "O";
                Console.Write($"{flipped} ");
            }
            Console.WriteLine("\n");
        }

        // DEBUG: Print all card values / cards face up
        public void PrintCardsWithValues() {
            CheckIfGameActive();
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < currentGame.CardArray.Length; i++) {
                Console.Write($"{currentGame.CardArray[i].Value} ");
            }
            Console.WriteLine();
            Console.ResetColor();
        }


        private void CheckIfGameFinished() {
            CheckIfGameActive();

            if (!currentGame.Complete && currentGame.Matches == currentGame.CardArray.Length / 2) {
                currentGame.Complete = true;
                sw.Stop();
                currentGame.TimeElapsed = sw.Elapsed.Seconds;

                currentGame.Score = CalculateScore(currentGame.CardArray.Length, currentGame.TimeElapsed, currentGame.Tries);
                // TO-DO: Event (indien nodig)
            }
        }

        private void CheckIfGameActive() {
            if (currentGame == null) {
                // TO-DO: Dit kan beter afgehandeld worden
                throw new Exception("No game active!");
            }
        }

        public static int CalculateScore(int amountCards, double timeElapsed, int tries) {
            // NOTE: Since integers are used, the score will be truncated
            return (int)(Math.Pow(amountCards, 2) / (timeElapsed * tries) * 1000);
        }

        /// <summary>
        /// Used for shuffling cards in InitializeCards
        /// Source: https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        private static void Shuffle<T>(T[] array) {
            Random rng = new Random();
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
