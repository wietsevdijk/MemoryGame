using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MemoryGame.WpfApp.Views {
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window {
        public GameWindow(Game game, GameController gameController, bool useCustomImages) {
            DataContext = new ViewModels.GameWindowViewModel(this, game, gameController, useCustomImages);
            InitializeComponent();
        }

        public BitmapImage GenerateBitmapImage(string filePath) {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(filePath, UriKind.Relative);
            image.EndInit();

            return image;
        }

    }
}
