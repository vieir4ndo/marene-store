using MareneStore.Application.Dtos;
using System.Threading.Tasks;

namespace MareneStore.Application.Interfaces
{
    public interface IUserService
    {
        Task<AppUserDto> RegisterAsync(string username, string password);
        Task<string> AuthenticateAsync(string username, string password);
        Task<AppUserDto> GetByIdAsync(string userId);
    }
}