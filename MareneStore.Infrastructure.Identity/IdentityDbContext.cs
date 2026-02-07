using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;

namespace MareneStore.Infrastructure.Identity
{
    public class IdentityDbContext : IdentityDbContext<AppUser>
    {
        public IdentityDbContext() : base("Name=IdentityDefaultConnection") { }
        public static IdentityDbContext Create() => new IdentityDbContext();

    }
}
