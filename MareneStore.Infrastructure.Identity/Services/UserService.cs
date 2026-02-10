using MareneStore.Application.Dtos;
using MareneStore.Application.Interfaces;
using MareneStore.Domain.Exceptions;
using MareneStore.Infrastructure.Identity.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Threading.Tasks;

namespace MareneStore.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(IdentityDbContext identityDbContext)
        {
            _userManager = new UserManager<AppUser>(new UserStore<AppUser>(identityDbContext));
        }

        public async Task<AppUserDto> RegisterAsync(string username, string password)
        {
            var user = new AppUser { UserName = username };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.ToList());
            }

            return user.ToDto();
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _userManager.FindAsync(username, password);
            return user?.Id;
        }

        public async Task<AppUserDto> GetByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException();
            }
            return new AppUserDto { Id = user.Id, Username = user.UserName };
        }
    }
}
