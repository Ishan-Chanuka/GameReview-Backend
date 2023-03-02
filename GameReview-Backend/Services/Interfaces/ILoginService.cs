using GameReview_Backend.Models.RequestModels;
using GameReview_Backend.Models.ResponseModels;

namespace GameReview_Backend.Services.Interfaces
{
    public interface ILoginService
    {
        LoginResponseModel Login(LoginRequestModel entity);
    }
}
