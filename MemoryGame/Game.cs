using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class Game
    {
        private Card[] _cardArray;
        public Card[] CardArray { get => _cardArray; set => _cardArray = value; }
        // Amount of attempts
        private int _tries = 0;
        public int Tries { get => _tries; }

        // Whether game is finished
        private bool _complete = false;
        public bool Complete { get => _complete; set => _complete = value; }
        
        // Amount of correct matches
        private int _matches = 0;
        public int Matches { get => _matches; }

        private string _playerName;
        public string PlayerName { get => _playerName; set => _playerName = value; }

        public Game(Card[] cards, string playerName) {
            _cardArray = cards;
            _playerName = playerName;
        }

        public int GetValue(int cardPos) {
            return _cardArray[cardPos - 1].Value;
        }

        public bool GetDiscovered(int cardPos) {
            return _cardArray[cardPos - 1].Discovered;
        }

        public bool GetFlipped(int cardPos) {
            return _cardArray[cardPos - 1].Flipped;
        }

    }
}
