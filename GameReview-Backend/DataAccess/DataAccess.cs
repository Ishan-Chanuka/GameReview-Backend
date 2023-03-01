using GameReview_Backend.DataAccess.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GameReview_Backend.DataAccess
{
    public class DataAccess : IDataAccess
    {
        public DataAccess(IOptions<MongoDBSettings> options)
        {
            _settings = options.Value;
        }

        private readonly MongoDBSettings _settings;


        public IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(_settings.ConnectionString);
            var db = client.GetDatabase(_settings.DatabaseName);
            var collectionName = db.GetCollection<T>(collection);
            return collectionName;
        }
    }
}
