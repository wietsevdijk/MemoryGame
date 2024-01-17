namespace MemoryGame.Tests {
    [TestFixture]
    public class GameControllerTests {

        [TestCase(4, 10, 2, 800)]
        [TestCase(10, 20, 5, 1000)]
        [TestCase(4, 20, 2, 400)]
        [TestCase(4, 10, 3, 533)]
        public void GameController_CalculateScore_OutputsCorrectly(int amountCards, double timeElapsed, int tries, int expectedScore) {
            int score = GameController.CalculateScore(amountCards, timeElapsed, tries);

            Assert.That(score, Is.EqualTo(expectedScore));
        }

        [Test]
        public void GameController_CheckIfGameActive_ThrowsException() {
            GameController gc = new GameController();

            // NOTE: CheckIfGameActive() is a private method, so we need to use a public one that calls it
            Assert.Throws<Exception>(() => gc.PrintCards());
        }

        [Test]
        public void GameController_CompareCards_ReturnsCorrectly() {
            GameController gc = new GameController();
            Game game = gc.InitializeGame(4, "Test");

            game.CardArray = new Card[] {
                new Card(1), //i0 - c1
                new Card(1), //i1 - c2
                new Card(2), //i2 - c3
                new Card(2), //i3 - c4
                new Card(3), //i4 - c5
                new Card(3), //i5 - c6
                new Card(4), //i6 - c7
                new Card(4)  //i7 - c8
            };

            // Test if matching cards returns correctly
            Assert.That(gc.CompareCards(1, 2), Is.True);
            Assert.That(gc.CompareCards(3, 7), Is.False);

            // Test if comparing to itself returns false
            Assert.That(gc.CompareCards(8, 8), Is.False);

            // Test if comparing to discovered card, despite card values being the same, returns false
            game.CardArray[4].Discovered = true; // Set card 5 to discovered
            Assert.That(gc.CompareCards(5, 6), Is.False);
        }

        [Test]
        public void GameController_CheckIfGameFinished_EndsGame() {
            GameController gc = new GameController();
            Game game = gc.InitializeGame(2, "Test");

            game.CardArray = new Card[] {
                new Card(1), //i0 - c1
                new Card(1), //i1 - c2
                new Card(2), //i2 - c3
                new Card(2)  //i3 - c4
            };

            gc.CompareCards(1, 2);

            Assert.That(game.Complete, Is.False);

            gc.CompareCards(3, 4);

            Assert.That(game.Complete, Is.True);
        }
    }
}