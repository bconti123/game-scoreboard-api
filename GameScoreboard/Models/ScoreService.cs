using GameScoreboard.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameScoreboard.Services
{
    public class ScoreService
    {
        private readonly IMongoCollection<Score> _scores;

        public ScoreService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var database = client.GetDatabase(config.GetValue<string>("MongoDB:DatabaseName"));
            _scores = database.GetCollection<Score>(config.GetValue<string>("MongoDB:ScoreCollectionName"));
        }

        public async Task<List<Score>> GetAsync() =>
            await _scores.Find(score => true).ToListAsync();

        public async Task<Score?> GetAsync(string id) =>
            await _scores.Find(score => score.Id == id).FirstOrDefaultAsync();

        public async Task<Score> CreateAsync(Score score)
        {
            await _scores.InsertOneAsync(score);
            return score;
        }

        public async Task UpdateAsync(string id, Score scoreIn) =>
            await _scores.ReplaceOneAsync(score => score.Id == id, scoreIn);

        public async Task DeleteAsync(string id) =>
            await _scores.DeleteOneAsync(score => score.Id == id);
    }
}