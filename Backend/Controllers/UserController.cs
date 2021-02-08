using Backend.Services;
using Backend.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }


        /// <summary>
        /// Add new user
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _service.Register(model);

            return Created($"{user.Id}", user);
        }

        /// <summary>
        /// Sign In
        /// </summary>
        [HttpPost("authenticate")]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _service.Authenticate(model);

            if (user == null)
            {
                return UnprocessableEntity(new
                {
                    Message = "Email ou Password inválido"
                });
            }

            return Ok(user);

        }

        /// <summary>
        /// Lists user favorite musics
        /// </summary>
        [HttpGet("{id}/favorite-music")]
        public async Task<IActionResult> GetUserFavoriteMusic(Guid id)
        {
            return Ok(await _service.GetFavoriteMusics(id));
        }

        /// <summary>
        /// Add music to user favorite musics list
        /// </summary>
        [HttpPost("{id}/favorite-music/{musicId}")]
        public async Task<IActionResult> SaveUserFavoriteMusic(Guid id, Guid musicId)
        {
            return Ok(await _service.AddFavoriteMusic(id, musicId));
        }

        /// <summary>
        /// Deletes music from user favorite musics list
        /// </summary>
        [HttpDelete("{id}/favorite-music/{musicId}")]
        public async Task<IActionResult> RemoveUserFavoriteMusic(Guid id, Guid musicId)
        {
            return Ok(await _service.RemoveFavoriteMusic(id, musicId));
        }

    }
}
