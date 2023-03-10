using GameReview_Backend.Models;
using GameReview_Backend.Models.RequestModels;
using GameReview_Backend.Models.ResponseModels;
using GameReview_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharpCompress.Common;
using System.Threading.Tasks;

namespace GameReview_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        public GameController(IGamesService games)
        {
            _games = games;
        }

        private readonly IGamesService _games;

        [HttpGet]
        [Route("GetAllGames")]
        public async Task<ActionResult<IEnumerable<Games>>> GetAllGames()
        {
            var result = await _games.GetAllGames();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateAGame")]
        public async Task<ActionResult<IEnumerable<GameRequestModel>>> CreateAGame([FromBody] GameRequestModel entity)
        {
            ResponseMessageModel response = new();

            if (entity == null)
                return BadRequest();

            var result = _games.InsertAGame(entity);

            if (result == false)
            {
                response.Message = "Game Creation Faild";
                return BadRequest(response);
            }

            response.Message = "Game Creation Successfull";
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateAGame")]
        public async Task<ActionResult> UpdateAGame([FromBody] Games entity)
        {
            ResponseMessageModel response = new();

            if (entity == null)
            {
                response.Message = "Inputs cannot be empty";
                return BadRequest(response);
            }

            var result = _games.UpdateAGame(entity);

            if (result == null)
            {
                response.Message = "Game Update Faild";
                return BadRequest(response);
            }

            response.Message = "Game Update Successfull";
            return Ok(response);
        }

        [HttpDelete]
        [Route("DeleteAGame")]
        public async Task<ActionResult> DeleteAGame(string id)
        {
            ResponseMessageModel response = new();

            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var result =  _games.DeleteAGame(id);

            if (result == null)
            {
                response.Message = "Game Delete Faild";
                return BadRequest(response);
            }

            response.Message = "Game Deleted Successfull";
            return Ok(response);
        }
    }
}
