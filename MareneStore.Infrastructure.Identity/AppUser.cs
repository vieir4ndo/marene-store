using Microsoft.AspNet.Identity.EntityFramework;

namespace MareneStore.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public string NomeCompleto { get; set; }
    }
}
