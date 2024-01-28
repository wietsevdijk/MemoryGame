using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MemoryGame {
    public class Card {
        private int _position;
        public int Position { get => _position; set => _position = value; }
        private int _value;
        public int Value { get => _value; set => _value = value; }
        private bool _discovered;
        public bool Discovered { get => _discovered; set => _discovered = value; }
        private string _backsideImage = GlobalConfig.ImageFilePath + "achterkant.jpg";
        public string BacksideImage { get => _backsideImage; set => _backsideImage = value; }
        private string _frontsideImage = GlobalConfig.ImageFilePath + "dasvoorkant.jpg";
        public string FrontsideImage { get => _frontsideImage; set => _frontsideImage = value; }

        // NOTE: All properties below are only used for the GUI
        private bool _flipped;
        public bool Flipped { get => _flipped; set => _flipped = value; }
        public bool CardAvailable => CheckCardAvailable();

        public Card(int id, int inputValue) { 
            _position = id;
            _value = inputValue;
            _flipped = false;
            _discovered = false;
        }

        public Card(int id, int inputValue, string imagePath) {
            _position = id;
            _value = inputValue;
            _flipped = false;
            _discovered = false;

            _frontsideImage = imagePath;
        }

        private bool CheckCardAvailable() {
            if (_discovered) {
                return false;
            }
            return !_flipped;
        }

    }
}