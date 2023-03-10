using GameReview_Backend.Models;
using GameReview_Backend.Models.RequestModels;

namespace GameReview_Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<Users>> SignUp(SignUpRequestModel entity);
        Task<List<Users>> GetUsers();
    }
}
