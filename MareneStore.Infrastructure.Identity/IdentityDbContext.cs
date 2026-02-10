using MareneStore.Infrastructure.Identity.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MareneStore.Infrastructure.Identity
{
    public class IdentityDbContext : IdentityDbContext<AppUser>
    {
        public IdentityDbContext() : base("Name=IdentityDefaultConnection") { }
        public static IdentityDbContext Create() => new IdentityDbContext();

    }
}
