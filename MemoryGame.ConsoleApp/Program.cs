using MemoryGame;

namespace MemoryGame.ConsoleApp {
    internal class Program {
        static void Main(string[] args) {
            bool debug = true;

            GameController gc = new GameController();

            Console.WriteLine("Wat is je naam?");
            string playerName = Console.ReadLine();

            bool inputValid = false;
            int cardPairs = 0;
            while (!inputValid) {
                Console.WriteLine("Hoeveel paren aan kaarten wil je hebben?");
                inputValid = int.TryParse(Console.ReadLine(), out int input);
                if (inputValid) { 
                    cardPairs = input;
                    continue; 
                }
                Console.WriteLine("Voer een nummer in!");
            }

            Game game = gc.InitializeGame(cardPairs, playerName);

            if (debug) {
                gc.PrintCardsWithValues();
            }

            //gameloop
            while (!game.Complete) { 
                gc.PrintCards();

                Console.WriteLine("Kies 1e kaart:");
                // TO-DO: Handel ongeldige input af
                int i1 = int.Parse(Console.ReadLine());
                if (game.GetDiscovered(i1)) {
                    Console.WriteLine("Kaart 1 is al omgedraaid!");
                    continue;
                }
                Console.WriteLine(game.GetValue(i1));

                Console.WriteLine("Kies 2e kaart:");
                // TO-DO: Handel ongeldige input af
                int i2 = int.Parse(Console.ReadLine());
                if (game.GetDiscovered(i2)) {
                    Console.WriteLine("Kaart 2 is al omgedraaid!");
                    continue;
                }
                Console.WriteLine(game.GetValue(i2));

                //Check of keuzes niet gelijk zijn
                if (i1 == i2) {
                    Console.WriteLine("Je kan niet dezelfde kaart kiezen!");
                    continue;
                }

                bool match = gc.CompareCards(i1, i2);
                // GA HIER VERDER
            }

            public static void CompareCards(int pos1, int pos2) {
                _tries++;

                bool match = cardArray[pos1 - 1].Value == cardArray[pos2 - 1].Value ? true : false;

                if (match) {
                    cardArray[pos1 - 1].Discovered = true;
                    cardArray[pos2 - 1].Discovered = true;
                    _matches++;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Match!");
                    Console.ForegroundColor = ConsoleColor.White;

                    if (_matches == cardArray.Length / 2) { _complete = true; }
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Geen match");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

        }
    }
}
