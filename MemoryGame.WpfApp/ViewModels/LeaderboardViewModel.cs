using CommunityToolkit.Mvvm.ComponentModel;
using MemoryGame.DataAccess.SqlServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.WpfApp.ViewModels {
    public partial class LeaderboardViewModel : ObservableObject {
        [ObservableProperty]
        private IOrderedEnumerable<Game>? _highScoreList;

        public LeaderboardViewModel() {
            GameRepository gamesDataAccess = new GameRepository(GlobalConfig.ConnectionString);

            HighScoreList = gamesDataAccess.GetAll().OrderByDescending(g => g.Score);
        }


    }
}
