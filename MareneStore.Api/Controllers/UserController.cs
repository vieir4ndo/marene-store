using MareneStore.Api.Responses;
using MareneStore.Application.Interfaces;
using System.Security.Claims;
using System.Web.Http;

namespace MareneStore.Api.Controllers
{
    [RoutePrefix("api/v1/user")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public IHttpActionResult GetUser()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = _userService.GetByIdAsync(userId).Result;
            return Ok(ApiResponse.CreateSuccess(user));
        }
    }
}