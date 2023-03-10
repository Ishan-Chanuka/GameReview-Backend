using GameReview_Backend.Models;
using GameReview_Backend.Models.RequestModels;

namespace GameReview_Backend.Services.Interfaces
{
    public interface IGamesService
    {
        Task<List<Games>> GetAllGames();
        bool InsertAGame(GameRequestModel entity);
        Task<List<Games>> UpdateAGame(Games entity);
        Task<List<Games>> DeleteAGame(string id);
    }
}
