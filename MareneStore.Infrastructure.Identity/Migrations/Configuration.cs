namespace MareneStore.Infrastructure.Identity.Migrations
{
    using MareneStore.Infrastructure.Identity.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MareneStore.Infrastructure.Identity.IdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MareneStore.Infrastructure.Identity.IdentityDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            var userManager = new UserManager<AppUser>(
                new UserStore<AppUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            string email = "admin@admin.com";
            string password = "Admin@123";

            var user = userManager.FindByEmail(email);

            if (user == null)
            {
                var admin = new AppUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = userManager.Create(admin, password);

                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, "Admin");
                }
            }
            else
            {
                if (!userManager.IsInRole(user.Id, "Admin"))
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
        }

    }
}
