using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        AuthService authserv;
        NoteService noteserv;
        public AuthController(AuthService authserv, NoteService noteserv)
        {
            this.authserv = authserv;
            this.noteserv = noteserv;
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (success, error) = authserv.Register(dto);
            if (!success)
            {
                return BadRequest(new { Msg = error });
            }

            return StatusCode(201, new { Msg = "User registered" });
        }
        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = authserv.Login(dto);
            if (user == null)
            {
                return Unauthorized(new { Msg = "Invalid email or password" });
            }

            var summaries = noteserv.GetAllSummaries();
            return Ok(new
            {
                Welcome = $"Welcome {user.Name}",
                Notes = summaries,
            });
        }

    }
}
