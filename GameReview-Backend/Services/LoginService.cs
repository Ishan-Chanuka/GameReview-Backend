using GameReview_Backend.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GameReview_Backend.Services
{
    public class LoginService : ILoginService
    {
        public LoginService(IConfiguration configuration)
        {
            _secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
        }

        private readonly string _secretKey;
    }
}
