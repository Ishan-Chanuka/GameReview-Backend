using GameReview_Backend.Models;
using GameReview_Backend.Models.RequestModels;
using GameReview_Backend.Models.ResponseModels;

namespace GameReview_Backend.Services.Interfaces
{
    public interface IGamesService
    {
        Task<List<GamesResponseModel>> GetAllGames();
        Task<List<GamesResponseModel>> GetGameById(string Id);
        bool InsertAGame(Games entity);
        Task<List<Games>> UpdateAGame(Games entity);
        Task<List<Games>> DeleteAGame(string id);
    }
}
