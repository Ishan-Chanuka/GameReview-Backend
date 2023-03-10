using GameReview_Backend.Models;
using GameReview_Backend.Models.RequestModels;

namespace GameReview_Backend.Services.Interfaces
{
    public interface IReviewsService
    {
        Task CreateReview(ReviewRequestModel entity, string gameID);
        Task DeleteReview(string gameID, string reviewID);
    }
}
