using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class Card
    {
        private int _value;
        public int Value { get => _value; set => _value = value; }
        // NOTE: This property is only used for the GUI
        private bool _flipped;
        public bool Flipped { get => _flipped; set => _flipped = value; }
        
        private bool _discovered;
        public bool Discovered { get => _discovered; set => _discovered = value; }

        public Card(int inputValue) { 
            _value = inputValue;
            _flipped = false;
            _discovered = false;
        }

        
    }
}
