using MareneStore.Api.Requests;
using MareneStore.Application.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace MareneStore.Api.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register(RegisterRequest request)
        {
            var result = await _userService.RegisterAsync(request.Username, request.Password, request.NomeCompleto);
            if (!result.Success) return BadRequest(string.Join(", ", result.Errors));
            return Ok("Usuário criado!");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login(LoginRequest request)
        {
            var userId = await _userService.AuthenticateAsync(request.Username, request.Password);
            if (userId == null) return Unauthorized();

            // Aqui você gera JWT
            return Ok(new { token = "JWT-Aqui" });
        }
    }

}