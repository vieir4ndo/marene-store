using MareneStore.Application.Interfaces;
using MareneStore.Infrastructure.Identity;
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

        public async Task<Result> RegisterAsync(string username, string password, string nomeCompleto)
        {
            var user = new AppUser { UserName = username, NomeCompleto = nomeCompleto };
            var result = await _userManager.CreateAsync(user, password);
            return new Result { Success = result.Succeeded, Errors = result.Errors.ToArray() };
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _userManager.FindAsync(username, password);
            return user?.Id; // API gera JWT, MVC usa Cookie
        }

        public async Task<AppUserDto> GetByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;
            return new AppUserDto { Id = user.Id, Username = user.UserName, NomeCompleto = user.NomeCompleto };
        }
    }
}
