using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame {
    public interface IGameRepository {
        ICollection<Game> GetAll();

        int Insert(Game game);

        int DeleteOldScores();
    }
}
