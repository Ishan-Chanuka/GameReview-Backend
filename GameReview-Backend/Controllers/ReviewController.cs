using GameReview_Backend.Models;
using GameReview_Backend.Models.RequestModels;
using GameReview_Backend.Models.ResponseModels;
using GameReview_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameReview_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public ReviewController(IReviewsService reviews)
        {
            _reviews = reviews;
        }

        private readonly IReviewsService _reviews;


        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview([FromBody] ReviewRequestModel entity, string gameId)
        {
            ResponseMessageModel response = new();

            if (entity == null || string.IsNullOrEmpty(gameId))
            {
                return BadRequest();
            }

            var result = _reviews.CreateReview(entity, gameId);

            if (result == null)
            {
                response.Message = "Review creation faild";
                return BadRequest(response);
            }

            response.Message = "Review creation successfull";
            return Ok(response);
        }

        [HttpDelete("DeleteReview")]
        public async Task<IActionResult> DeleteReview(string gameId, string reviewId)
        {
            ResponseMessageModel response = new();

            if (string.IsNullOrEmpty(gameId) || string.IsNullOrEmpty(reviewId))
            {
                response.Message = "Failed to delete the review";
                return BadRequest(response);
            }

            var result = _reviews.DeleteReview(gameId, reviewId);

            if (result == null)
            {
                response.Message = "Failed to delete the game";
                return BadRequest(response);
            }

            response.Message = "Review deleted successfull";
            return Ok(response);
        }
    }
}
