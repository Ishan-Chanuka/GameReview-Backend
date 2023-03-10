using GameReview_Backend.DataAccess.Interface;
using GameReview_Backend.DataAccess;
using GameReview_Backend.Services.Interfaces;
using Microsoft.Extensions.Options;
using GameReview_Backend.Models;
using MongoDB.Driver;
using GameReview_Backend.Models.RequestModels;
using System;

namespace GameReview_Backend.Services
{
    public class UserService : IUserService
    {
        public UserService(IDataAccess dataAccess, IOptions<MongoDBSettings> options, IConverter converter)
        {
            _dataAccess = dataAccess;
            _converter = converter;
            _settings = options.Value;
            _user = _dataAccess.ConnectToMongo<Users>(_settings.UserCollection);
        }

        private readonly IDataAccess _dataAccess;
        private readonly IConverter _converter;
        private readonly MongoDBSettings _settings;
        private readonly IMongoCollection<Users> _user;

        public async Task<List<Users>> GetUsers()
        {
            var result = await _user.FindAsync(_ => true);

            return result.ToList();
        }

        public async Task<List<Users>> SignUp(SignUpRequestModel entity)
        {
            string hashedPassword = _converter.PasswordEncription(entity.Password);

            var userdto = new Users()
            {
                FullName = entity.FullName,
                UserEmail = entity.Email,
                UserRole = "User",
                Password = hashedPassword,
            };

            _user.InsertOne(userdto);
            var result = await _user.FindAsync(proj => proj.UserId == userdto.UserId);

            return result.ToList();
        }
    }
}