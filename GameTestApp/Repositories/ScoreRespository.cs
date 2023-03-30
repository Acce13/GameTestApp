using GameTestApp.Helpers;
using GameTestApp.Models;
using Newtonsoft.Json;
using SQLite;
using System.Diagnostics;

namespace GameTestApp.Repositories
{
    public class ScoreRespository
    {
        SQLiteAsyncConnection Database;

        public ScoreRespository() { }

        async Task Init()
        {
            if (Database is not null)
                return;
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await Database.CreateTableAsync<GameTest>();
            await Database.CreateTableAsync<Score>();
            //Read File Json
            using var stream = await FileSystem.OpenAppPackageFileAsync("GameTest.json");
            using var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();
            //Convert Data to List
            List<GameTest> gameTests = JsonConvert.DeserializeObject<List<GameTest>>(contents);
            foreach (GameTest gameTest in gameTests)
            {
                //Insert Data
                await Database.InsertAsync(gameTest);
            }
        }
        public async Task<List<Score>> GetScoreAsync()
        {
            await Init();
            return await Database.QueryAsync<Score>("SELECT * FROM Scores ORDER BY id DESC");
        }

        public void StoreScore(int score)
        {
            var newScore = new Score()
            {
                Player = "Player One",
                FScore = score
            };
            var conn = new SQLiteConnection(Constants.DatabasePath);
            conn.Insert(newScore);
        }
    }
}
