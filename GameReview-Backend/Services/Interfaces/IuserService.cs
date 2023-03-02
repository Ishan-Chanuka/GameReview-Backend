using GameReview_Backend.Models;

namespace GameReview_Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<Users>> Insert(Users entity);
        Task<List<Users>> GetUsers();
    }
}
