using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MemoryGame.DataAccess.SqlServer;
using MemoryGame.WpfApp.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace MemoryGame.WpfApp.ViewModels {
    public partial class StartViewModel : ObservableObject {

        [ObservableProperty]
        private string _playerName = "";

        [ObservableProperty]
        private int _cardPairCount;

        [ObservableProperty]
        private string _errorMessage = "";

        [ObservableProperty]
        private bool _useCustomImages;

        [RelayCommand]
        private void StartGame() {
            ErrorMessage = "";

            if (!CanStartGame()) { return; }

            GameRepository gamesDataAccess = new GameRepository(GlobalConfig.ConnectionString);
            GameController gc = new GameController(gamesDataAccess);

            Game game;
            if (UseCustomImages) {
                if(Directory.EnumerateFiles(GlobalConfig.ImageFilePath).Count() < CardPairCount) {
                    ErrorMessage = "Er zijn niet genoeg foto's geupload!\n";
                    return;
                }

                game = gc.InitializeGame(CardPairCount, PlayerName, Directory.GetFiles(GlobalConfig.ImageFilePath));
            } else {
                game = gc.InitializeGame(CardPairCount, PlayerName);
            }

            GameWindow gameWindow = new GameWindow(game, gc);
            gameWindow.Show();
        }

        [RelayCommand]
        public void OpenUploadImagesWindow() {
            List<string> customImagePaths = new List<string>();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                customImagePaths = openFileDialog.FileNames.ToList();

                if (!Directory.Exists(GlobalConfig.ImageFilePath)) {
                    Directory.CreateDirectory(GlobalConfig.ImageFilePath);
                }

                foreach (string imagePath in customImagePaths) {
                    string fileName = Path.GetFileName(imagePath);
                    string destinationPath = Path.Combine(GlobalConfig.ImageFilePath, fileName);
                    File.Copy(imagePath, destinationPath, true);
                }

                ErrorMessage = $"Succesvol {customImagePaths.Count} afbeelding{(customImagePaths.Count > 1 ? "en" : string.Empty)} toegevoegd!";
            }
        }

        [RelayCommand]
        public void OpenLeaderboard() {
            LeaderboardWindow leaderboardWindow = new();
            leaderboardWindow.Show();
        }

        public StartViewModel() {
            if (Directory.Exists(GlobalConfig.ImageFilePath)) {
                DirectoryInfo directoryInfo = new DirectoryInfo(GlobalConfig.ImageFilePath);

                foreach (FileInfo file in directoryInfo.GetFiles()) {
                    file.Delete();
                }
            } else {
                Directory.CreateDirectory(GlobalConfig.ImageFilePath);
            }
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
