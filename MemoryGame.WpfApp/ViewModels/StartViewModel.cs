using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MemoryGame.DataAccess.SqlServer;
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
                GameRepository gamesDataAccess = new GameRepository(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MemoryDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;");
                GameController gc = new GameController();
            
            
            }
        }

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
