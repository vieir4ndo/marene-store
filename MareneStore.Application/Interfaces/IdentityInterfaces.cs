using System.Threading.Tasks;

namespace MareneStore.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result> RegisterAsync(string username, string password, string nomeCompleto);
        Task<string> AuthenticateAsync(string username, string password);
        Task<AppUserDto> GetByIdAsync(string userId);
    }

    public class Result
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }

    public class AppUserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string NomeCompleto { get; set; }
    }
}