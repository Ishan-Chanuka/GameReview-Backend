using GameReview_Backend.DataAccess;
using GameReview_Backend.DataAccess.Interface;
using GameReview_Backend.Models;
using GameReview_Backend.Models.RequestModels;
using GameReview_Backend.Models.ResponseModels;
using GameReview_Backend.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;

namespace GameReview_Backend.Services
{
    public class GamesService : IGamesService
    {
        public GamesService(IDataAccess data, IOptions<MongoDBSettings> options)
        {
            _dataAccess = data;
            _settings = options.Value;
            _game = _dataAccess.ConnectToMongo<Games>(_settings.GameCollection);
        }

        private readonly IDataAccess _dataAccess;
        private readonly MongoDBSettings _settings;
        private readonly IMongoCollection<Games> _game;

        public async Task<List<GamesResponseModel>> GetAllGames()
        {
            try
            {
                var result = await _game.FindAsync(_ => true);

                var response = new List<GamesResponseModel>();

                foreach (var item in result.ToList())
                {
                    response.Add(new GamesResponseModel()
                    {
                        GameId = item.GameId,
                        Name = item.Name,
                        Description = item.Description,
                        ImageUrl = item.ImageUrl,
                        Rating = item.Rating,
                        Reviews = item.Reviews,
                        DeletedFlag = item.DeletedFlag
                    });
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GamesResponseModel>> GetGameById(string Id)
        {
            var result = await _game.FindAsync(c => c.GameId == Id);

            var response = new List<GamesResponseModel>();

            

            foreach (var item in result.ToList())
            {
                response.Add(new GamesResponseModel()
                {
                    GameId = item.GameId,
                    Name = item.Name,
                    Description = item.Description,
                    ImageUrl = item.ImageUrl,
                    Rating = item.Rating,
                    Reviews = item.Reviews,
                    DeletedFlag = item.DeletedFlag
                });
            }

            return response;
        }

        public bool InsertAGame(Games entity)
        {
            bool state = false;


            if (entity != null)
            {
                _game.InsertOne(entity);
            }

            return state = true;
        }

        public async Task<List<Games>> UpdateAGame(Games entity)
        {
            var filter = Builders<Games>.Filter.Eq("GameId", entity.GameId);
            await _game.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = true });
            var result = await _game.FindAsync(c => c.GameId == entity.GameId);

            return result.ToList();
        }

        public async Task<List<Games>> DeleteAGame(string id)
        {
            var filter = Builders<Games>.Filter.Eq("GameId", id);
            var update = Builders<Games>.Update.Set("DeletedFlag", true);
            await _game.UpdateOneAsync(filter, update);

            var result = await _game.FindAsync(c => c.GameId == id && c.DeletedFlag == false);

            return result.ToList();
        }
    }
}
