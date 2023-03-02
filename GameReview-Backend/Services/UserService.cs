using GameReview_Backend.DataAccess.Interface;
using GameReview_Backend.DataAccess;
using GameReview_Backend.Services.Interfaces;
using Microsoft.Extensions.Options;
using GameReview_Backend.Models;
using MongoDB.Driver;

namespace GameReview_Backend.Services
{
    public class UserService : IUserService
    {
        public UserService(IDataAccess dataAccess, IOptions<MongoDBSettings> options)
        {
            _dataAccess = dataAccess;
            _settings = options.Value;
            _user = _dataAccess.ConnectToMongo<Users>(_settings.UserCollection);
        }

        private readonly IDataAccess _dataAccess;
        private readonly MongoDBSettings _settings;
        private readonly IMongoCollection<Users> _user;

        public async Task<List<Users>> GetUsers()
        {
            var result = await _user.FindAsync(_ => true);

            return result.ToList();
        }

        public async Task<List<Users>> Insert(Users entity)
        {
            _user.InsertOne(entity);
            var result = await _user.FindAsync(proj => proj.UserId == entity.UserId);

            return result.ToList();
        }
    }
}
