using GameReview_Backend.DataAccess;
using GameReview_Backend.DataAccess.Interface;
using GameReview_Backend.Models;
using GameReview_Backend.Models.RequestModels;
using GameReview_Backend.Models.ResponseModels;
using GameReview_Backend.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameReview_Backend.Services
{
    public class LoginService : ILoginService
    {
        public LoginService(IConfiguration configuration, IDataAccess dataAccess, IOptions<MongoDBSettings> options)
        {
            _secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
            _dataAccess = dataAccess;
            _settings = options.Value;
            _login = _dataAccess.ConnectToMongo<Users>(_settings.UserCollection);
        }

        private readonly string _secretKey;
        private readonly IDataAccess _dataAccess;
        private readonly MongoDBSettings _settings;
        private readonly IMongoCollection<Users> _login;

        public LoginResponseModel Login(LoginRequestModel entity)
        {
            var user = _login.Find(c => c.UserName == entity.UserName && c.Password == entity.Password);
            var details = user.ToList();


            if (user == null)
            {
                return new LoginResponseModel()
                {
                    UserRole = "",
                    Token = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var response = new LoginResponseModel();

            foreach (var item in details)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, item.UserRole)
                }),

                    Expires = DateTime.UtcNow.AddDays(7),

                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);


                response = new LoginResponseModel()
                {
                    Token = tokenHandler.WriteToken(token),
                    UserRole = item.UserRole
                };
            }
            return response;
        }
    }
}
