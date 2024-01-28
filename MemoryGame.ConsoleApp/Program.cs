using MemoryGame.DataAccess.SqlServer;

namespace MemoryGame.ConsoleApp {
    internal class Program {
        static void Main(string[] args) {
            bool debug = true;

            GameRepository gamesDataAccess = new GameRepository(GlobalConfig.ConnectionString);
            GameController gc = new GameController(gamesDataAccess);

            Console.ResetColor();

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
                Console.Clear();
                if (debug) {
                    gc.PrintCardsWithValues();
                }

                gc.PrintCards();

                Console.Write("Kies 1e kaart: ");
                // TO-DO: Handel ongeldige input af
                int i1 = int.Parse(Console.ReadLine());
                if (game.GetDiscovered(i1)) {
                    Console.WriteLine("Kaart 1 is al omgedraaid!");
                    Console.WriteLine("\nDruk op een toets om verder te spelen...");
                    Console.ReadKey();
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Kaart 1 is: {game.GetValue(i1)}\n");
                Console.ResetColor();

                Console.Write("Kies 2e kaart: ");
                // TO-DO: Handel ongeldige input af
                int i2 = int.Parse(Console.ReadLine());
                if (game.GetDiscovered(i2)) {
                    Console.WriteLine("Kaart 2 is al omgedraaid!");
                    Console.WriteLine("\nDruk op een toets om verder te spelen...");
                    Console.ReadKey();
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Kaart 2 is: {game.GetValue(i2)}\n");
                Console.ResetColor();

                //Check of keuzes niet gelijk zijn
                if (i1 == i2) {
                    Console.WriteLine("Je kan niet dezelfde kaart kiezen!");
                    Console.WriteLine("\nDruk op een toets om verder te spelen...");
                    Console.ReadKey();
                    continue;
                }

                bool match = gc.CompareCards(i1, i2);
                
                if (match) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Match!");
                    Console.ResetColor();

                    if(game.Complete) { break;}

                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Geen match");
                    Console.ResetColor();
                }

                //TODO: Elegantere manier
                Console.WriteLine("\nDruk op een toets om verder te spelen...");
                Console.ReadKey();
            }
            

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n\nGewonnen!" +
            $"\nNaam: {game.PlayerName}" +
            $"\nTijd: {game.TimeElapsed} seconden" +
            $"\nBeurten: {game.Tries}" +
            $"\nScore: {game.Score}");

            

            bool isHighScore = gc.SaveGame();

            if(isHighScore) {
                Console.WriteLine("\nGefeliciteerd! Je score is in de top 10 gekomen!");
            }


            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nTop 10 scores:");
            foreach (Game g in gc.GetHighScores()) {
                Console.WriteLine($"\t{g.PlayerName} - {g.Score}");
            }
            Console.ResetColor();

            Console.WriteLine("\nDruk op een toets om af te sluiten...");
        }
    }
}
