namespace MemoryGame.ConsoleApp {
    internal class Program {
        static void Main(string[] args) {
            bool debug = true;

            GameController gc = new GameController();

            Console.WriteLine("Wat is je naam?");
            string playerName = Console.ReadLine();
            Console.Clear();


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
            Console.Clear();

            Game game = gc.InitializeGame(cardPairs, playerName);

            //gameloop
            while (!game.Complete) {
                if (debug) {
                    gc.PrintCardsWithValues();
                }

                gc.PrintCards();

                Console.Write("Kies 1e kaart: ");
                // TO-DO: Handel ongeldige input af
                int i1 = int.Parse(Console.ReadLine());
                if (game.GetDiscovered(i1)) {
                    Console.WriteLine("Kaart 1 is al omgedraaid!");
                    Thread.Sleep(1500);
                    Console.Clear();
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Kaart 1 is: {game.GetValue(i1)}\n");
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.Write("Kies 2e kaart: ");
                // TO-DO: Handel ongeldige input af
                int i2 = int.Parse(Console.ReadLine());
                if (game.GetDiscovered(i2)) {
                    Console.WriteLine("Kaart 2 is al omgedraaid!");
                    Thread.Sleep(1500);
                    Console.Clear();
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Kaart 2 is: {game.GetValue(i2)}\n");
                Console.ForegroundColor = ConsoleColor.Gray;

                //Check of keuzes niet gelijk zijn
                if (i1 == i2) {
                    Console.WriteLine("Je kan niet dezelfde kaart kiezen!");
                    Thread.Sleep(1500);
                    Console.Clear();
                    continue;
                }

                bool match = gc.CompareCards(i1, i2);
                
                if (match) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Match!");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    if(game.Complete) { break;}

                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Geen match");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                //TODO: Elegantere manier
                Thread.Sleep(1500);
                Console.Clear();
            }
            

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nGewonnen!" +
            $"\nNaam: {game.PlayerName}" +
            $"\nTijd: {game.TimeElapsed} seconden" +
            $"\nBeurten: {game.Tries}" +
            $"\nScore: {game.Score}");

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
