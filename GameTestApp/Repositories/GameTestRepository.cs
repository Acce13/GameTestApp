using GameTestApp.Helpers;
using GameTestApp.Models;
using Newtonsoft.Json;
using SQLite;

namespace GameTestApp.Repositories
{
    public class GameTestRepository
    {
        SQLiteAsyncConnection Database;

        public GameTestRepository() { }

        async Task Init()
        {
            if (Database is not null)
                return;
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await Database.CreateTableAsync<GameTest>();
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

        public async Task<List<GameTest>> GetGameTestsAsync()
        {
            await Init();
            return await Database.QueryAsync<GameTest>("SELECT * FROM GameTests ORDER BY random() LIMIT 10");
        }
    }
}
