using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        public StartViewModel() {
            StartGameCommand = new RelayCommand(StartGame);
        }

        public ICommand StartGameCommand { get; }

        private void StartGame() {
            MessageBox.Show("Kaas");
        }

    }
}
