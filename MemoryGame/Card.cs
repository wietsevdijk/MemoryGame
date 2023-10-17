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
        
        private bool _discovered;
        public bool Discovered { get => _discovered; set => _discovered = value; }

        public Card(int inputValue) { 
            Value = inputValue;
            Discovered = false;
        }

        
    }
}
