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
        public async Task<ActionResult<IEnumerable<Users>>> SignUp(Users entity)
        {
            await _user.Insert(entity);

            var user = new List<Users>();
            user = await _user.GetUsers();

            return Ok(user);
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<IEnumerable<LoginResponseModel>> Login(LoginRequestModel entity)
        {
            var login = _login.Login(entity);

            return Ok(login);
        }
    }
}
