using MareneStore.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MareneStore.Application.Interfaces
{
    public interface IUserService
    {
        Task<AppUserDto> RegisterAsync(string username, string password);
        Task<string> AuthenticateAsync(string username, string password);
        Task<AppUserDto> GetByIdAsync(string userId);
        Task<IList<string>> GetUserRolesAsync(string userId);
    }
}