using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;

namespace MareneStore.Infrastructure.Identity
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("Name=IdentityDefaultConnection") { }
        public static AppDbContext Create() => new AppDbContext();

    }
}
