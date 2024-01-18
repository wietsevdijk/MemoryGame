using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.DataAccess.SqlServer {
    public class GameRepository : IGameRepository {
        public string ConnectionString;

        public GameRepository(string connectionString) {
            ConnectionString = connectionString;
        }

        public ICollection<Game> GetAll() {
            var playedGames = new List<Game>();
            string query = "SELECT PlayerName, CardCount, Score, TimeElapsed, AttemptsTaken FROM Games";

            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                using (SqlCommand command = new SqlCommand(query, connection)) {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read()) {
                        string playerName = reader.GetString(0);
                        int cardCount = reader.GetInt32(1);
                        double score = reader.GetDouble(2);
                        double timeElapsed = reader.GetDouble(3);
                        int attemptsTaken = reader.GetInt32(4);

                        playedGames.Add(new Game(playerName, cardCount, score, timeElapsed, attemptsTaken));
                    }
                }
                connection.Close();
            }

            return playedGames;
        }

        public int Insert(Game game) {
            string query = "INSERT INTO Games (PlayerName, CardCount, Score, TimeElapsed, AttemptsTaken) VALUES (@PlayerName, @CardCount, @Score, @TimeElapsed, @AttemptsTaken)";
            int recordsAffected = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                using (SqlCommand command = new SqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@PlayerName", game.PlayerName);
                    command.Parameters.AddWithValue("@CardCount", game.CardArray.Length);
                    command.Parameters.AddWithValue("@Score", game.Score);
                    command.Parameters.AddWithValue("@TimeElapsed", game.TimeElapsed);
                    command.Parameters.AddWithValue("@AttemptsTaken", game.Tries);

                    connection.Open();
                    recordsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return recordsAffected;
        }
        public int DeleteOldScores() {
            string query = "DELETE FROM Games WHERE Id NOT IN (SELECT TOP 10 Id FROM Games ORDER BY Score DESC)";
            int recordsAffected = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                using (SqlCommand command = new SqlCommand(query, connection)) {
                    connection.Open();
                    recordsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return recordsAffected;
        }
    }
}
