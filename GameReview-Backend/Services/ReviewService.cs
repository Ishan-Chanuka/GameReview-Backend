using GameReview_Backend.DataAccess;
using GameReview_Backend.DataAccess.Interface;
using GameReview_Backend.Models;
using GameReview_Backend.Models.RequestModels;
using GameReview_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;
using System.Xml.Linq;

namespace GameReview_Backend.Services
{
    public class ReviewService : IReviewsService
    {
        public ReviewService(IDataAccess dataAccess, IOptions<MongoDBSettings> option)
        {
            _dataAccess = dataAccess;
            _settings = option.Value;
            _reviws = _dataAccess.ConnectToMongo<Reviews>(_settings.ReviewCollection);
            _game = _dataAccess.ConnectToMongo<Games>(_settings.GameCollection);
        }

        private readonly IDataAccess _dataAccess;
        private readonly MongoDBSettings _settings;
        private readonly IMongoCollection<Reviews> _reviws;
        private readonly IMongoCollection<Games> _game;

        public async Task CreateReview(ReviewRequestModel entity, string gameID)
        {
            var game =  _game.FindAsync(c => c.GameId == gameID);

            if (game != null)
            {
                var review = new Reviews()
                {
                    UserName = entity.UserName,
                    UserRating = entity.UserRating,
                    Location = entity.Location,
                    Review = entity.Review,
                    DeletedFlag = false
                };
                await _reviws.InsertOneAsync(review);

                var filter = Builders<Games>.Filter.Eq("GameId", gameID);
                var update = Builders<Games>.Update.Push(x => x.Reviews, review);

                await _game.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<Games> 
                { 
                    ReturnDocument = ReturnDocument.After 
                });

            }
            return;
        }

        public async Task DeleteReview(string gameID, string reviewID)
        {
            var game = _game.FindAsync(c => c.GameId == gameID);

            if (game != null)
            {
                var filterReview = Builders<Reviews>.Filter.Eq("ReviewId", reviewID);
                var updateReview = Builders<Reviews>.Update.Set("DeletedFlag", true);
                await _reviws.UpdateOneAsync(filterReview, updateReview);

                var filter = Builders<Games>.Filter.And
                    (
                        Builders<Games>.Filter.Eq(g => g.GameId, gameID),
                        Builders<Games>.Filter.ElemMatch(g => g.Reviews, r => r.ReviewId == reviewID)
                    );
                var update = Builders<Games>.Update.PullFilter(g => g.Reviews, r => r.ReviewId == reviewID);

                var result = await _game.UpdateOneAsync(filter, update);

            }

            return;
        }
    }
}
