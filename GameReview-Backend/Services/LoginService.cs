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
        public LoginService(IConfiguration configuration, IDataAccess dataAccess, IOptions<MongoDBSettings> options, IConverter converter)
        {
            _secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
            _dataAccess = dataAccess;
            _settings = options.Value;
            _login = _dataAccess.ConnectToMongo<Users>(_settings.UserCollection);
            _converter = converter;
        }

        private readonly string _secretKey;
        private readonly IDataAccess _dataAccess;
        private readonly MongoDBSettings _settings;
        private readonly IMongoCollection<Users> _login;
        private readonly IConverter _converter;

        public LoginResponseModel Login(LoginRequestModel entity)
        {
            string hashedPassword = _converter.PasswordEncription(entity.Password);

            var user = _login.Find(c => c.UserEmail == entity.Email && c.Password == hashedPassword);
            var details = user.ToList();


            if (user == null)
            {
                return new LoginResponseModel()
                {
                    UserRole = "",
                    Token = null,
                    Email = "",
                    Name = "",
                    Message = "Login Faild"
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
                    UserRole = item.UserRole,
                    Name = item.FullName,
                    Email = item.UserEmail
                };
            }
            return response;
        }
    }
}