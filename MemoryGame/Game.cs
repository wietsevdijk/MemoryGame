﻿using System;
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
        public int Tries { get => _tries; set => _tries = value; }

        // Whether game is finished
        private bool _complete = false;
        public bool Complete { get => _complete; set => _complete = value; }
        
        // Amount of correct matches
        private int _matches = 0;
        public int Matches { get => _matches; set => _matches = value; }

        private double _timeElapsed = 0;
        public double TimeElapsed { get => _timeElapsed; set => _timeElapsed = value; }

        private double _score = 0;
        public double Score { get => _score; set => _score = value; }

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
