using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame.WpfApp.ViewModels {
    public partial class GameWindowViewModel : ObservableObject {
        private GameController _controller;
        [ObservableProperty]
        private Game _game;

        [ObservableProperty]
        private ObservableCollection<Card> _cardList;

        private bool _useCustomImages;

        public GameWindowViewModel(Game game, GameController gameController, bool useCustomImages) {
            _useCustomImages = useCustomImages;

            _controller = gameController;

            _game = game;
            
            UpdateCards();
        }

        [RelayCommand]
        public void FlipCard(int cardPos) {
            bool success = _controller.FlipCard(cardPos);
            UpdateCards();

        }

        private void UpdateCards() {
            List<Card> cards = Game.CardArray.ToList();

            CardList = new ObservableCollection<Card>();
            foreach (Card card in cards) {
                CardList.Add(card);
            }
        }
    }
}
