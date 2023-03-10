using GameReview_Backend.Models;
using GameReview_Backend.Models.RequestModels;
using GameReview_Backend.Models.ResponseModels;
using GameReview_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameReview_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IUserService user, ILoginService login)
        {
            _user = user;
            _login = login;
        }

        private readonly IUserService _user;
        private readonly ILoginService _login;

        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult<IEnumerable<Users>>> SignUp(SignUpRequestModel entity)
        {
            ResponseMessageModel response = new();

            if (entity == null)
            {
                response.Message = "Input Fields Cannot Be Empty";
                return BadRequest(response);
            }
            else
            {
                var result = await _user.SignUp(entity);

                if (result == null)
                {
                    response.Message = "SignUp Faild";
                    return BadRequest(response);
                }
                response.Message = "SignUp Success";
                return Ok(response);
            }
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<IEnumerable<LoginResponseModel>> Login(LoginRequestModel entity)
        {
            var response = _login.Login(entity);

            if (response.Email == null || string.IsNullOrEmpty(response.Token))
            {
                return Unauthorized(new { message = "Email or Password is incorrect" });
            }

            return Ok(response);
        }
    }
}
