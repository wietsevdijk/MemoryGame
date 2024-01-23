using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MemoryGame.DataAccess.SqlServer;
using MemoryGame.WpfApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MemoryGame.WpfApp.ViewModels {
    public partial class StartViewModel : ObservableObject {

        [ObservableProperty]
        private string _playerName = "";

        [ObservableProperty]
        private int _cardPairCount;

        [ObservableProperty]
        private string _errorMessage = "";

        [RelayCommand]
        private void StartGame() {
            ErrorMessage = "";

            if (CanStartGame()) {
                GameRepository gamesDataAccess = new GameRepository(GlobalConfig.ConnectionString);
                GameController gc = new GameController(gamesDataAccess);
                Game game = gc.InitializeGame(CardPairCount, PlayerName);

                GameWindow gameWindow = new GameWindow(game, gc, false);
                gameWindow.Show();
            }
        }

        [RelayCommand]
        public void OpenLeaderboard() {
            LeaderboardWindow leaderboardWindow = new();
            leaderboardWindow.Show();
        }

        /// <summary>
        /// Check if input fields are valid
        /// </summary>
        /// <returns></returns>
        private bool CanStartGame() {
            bool canStart = true;
            if (string.IsNullOrWhiteSpace(PlayerName)) {
                ErrorMessage += "Voer een naam in!\n";
                canStart = false;
            }
            if (CardPairCount < 2) {
                ErrorMessage += "Je moet met minstens 2 paren spelen!\n";
                canStart = false;
            }
            return canStart;
        }
    }
}
