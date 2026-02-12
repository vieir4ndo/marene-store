using MareneStore.Api.Requests;
using MareneStore.Api.Responses;
using MareneStore.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MareneStore.Api.Controllers
{
    [RoutePrefix("api/v1/auth")]
    public class AuthController : ApiController
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register(UserRequest request)
        {
            request.Validate();
            var result = await _userService.RegisterAsync(request.Username, request.Password);
            return Content<ApiResponse>(HttpStatusCode.Created, ApiResponse.CreateSuccess(result));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login(UserRequest request)
        {
            request.Validate();

            var userId = await _userService.AuthenticateAsync(request.Username, request.Password);
            if (userId == null) return Unauthorized();

            var user = await _userService.GetByIdAsync(userId);

            var secret = ConfigurationManager.AppSettings["JwtSecret"];
            var issuer = ConfigurationManager.AppSettings["JwtIssuer"];
            var audience = ConfigurationManager.AppSettings["JwtAudience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var roles = await _userService.GetUserRolesAsync(userId);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(ApiResponse.CreateSuccess(new TokenResponse()
            {
                Token = tokenString,
                Expiration = token.ValidTo
            }));
        }
    }

}