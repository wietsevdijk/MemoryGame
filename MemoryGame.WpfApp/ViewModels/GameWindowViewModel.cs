using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MemoryGame.WpfApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemoryGame.WpfApp.ViewModels {
    public partial class GameWindowViewModel : ObservableObject {
        private GameController _controller;
        private readonly GameWindow _window;
        private Game _game;

        [ObservableProperty]
        private List<Card> _cardList;

        private bool _useCustomImages;

        public GameWindowViewModel(GameWindow window, Game game, GameController gameController, bool useCustomImages) {
            _window = window;
            _useCustomImages = useCustomImages;

            _controller = gameController;
            _game = game;
            
            CardList = _game.CardArray.ToList();
        }


        // NIET MEER GEBRUIKT
        [RelayCommand]
        public void FlipCard(int cardPos) {
            bool success = _controller.FlipCard(cardPos);
            
            UpdateCards();
        }

        private int firstCardPosition = 0;
        [RelayCommand]
        public async Task CardClicked(int clickedCardPosition) {

            // Als er 2 keer op dezelfde kaart wordt geklikt
            if (firstCardPosition == clickedCardPosition) {
                firstCardPosition = clickedCardPosition;
                return;
            }
            // Als het om de eerste kaart gaat
            else if (firstCardPosition == 0) {
                firstCardPosition = clickedCardPosition;

                _controller.FlipCard(clickedCardPosition);
            } 
            // Als het om de tweede kaart gaat
            else {
                _controller.FlipCard(clickedCardPosition);
                UpdateCards();

                bool match = _controller.CompareCards(firstCardPosition, clickedCardPosition);
             
                if (!match) {
                    await ResetCardsAfterDelay(firstCardPosition, clickedCardPosition);
                }

                // Check if game is finished
                if (_game.Complete) {
                    bool isHighScore = _controller.SaveGame();

                    string victoryMessage = $"Gewonnen!" +
                        $"\nNaam: {_game.PlayerName}" +
                        $"\nTijd: {_game.TimeElapsed} seconden" +
                        $"\nBeurten: {_game.Tries}" +
                        $"\nScore: {_game.Score}";

                    if (isHighScore) {
                        victoryMessage += "\n\nGefeliciteerd! Je score is in de top 10 gekomen!";
                    }

                    MessageBox.Show(victoryMessage);

                    _window.Close();
                    
                    LeaderboardWindow leaderboardWindow = new LeaderboardWindow();
                    leaderboardWindow.Show();
                }

                // Reset
                firstCardPosition = 0;
            }

            UpdateCards();
        }

        private async Task ResetCardsAfterDelay(int firstCardPosition, int secondCardPosition) { 
            await Task.Delay(1000);

            _controller.FlipDownCard(firstCardPosition);
            _controller.FlipDownCard(secondCardPosition);
        }

        /// <summary>
        /// Update de kaarten in de GUI.
        /// Dit zou wellicht beter kunnen via een event, maar is geen prioriteit nu.
        /// </summary>
        private void UpdateCards() {
            CardList = _game.CardArray.ToList();
        }

        private BitmapImage SetImage(string imagePath) {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(imagePath, UriKind.Relative);
            image.EndInit();
            return image;
        }
    }
}
