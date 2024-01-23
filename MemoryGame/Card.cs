using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class Card
    {
        private int _position;
        public int Position { get => _position; set => _position = value; }
        private int _value;
        public int Value { get => _value; set => _value = value; }
        private bool _discovered;
        public bool Discovered { get => _discovered; set => _discovered = value; }

        // NOTE: All properties below are only used for the GUI
        private bool _flipped;
        public bool Flipped { get => _flipped; set => _flipped = value; }
        public bool CardAvailable => !_discovered;
        public string VisibleValue => _flipped ? _value.ToString() : string.Empty;

        public Card(int id, int inputValue) { 
            _position = id;
            _value = inputValue;
            _flipped = false;
            _discovered = false;
        }

        
    }
}