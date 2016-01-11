namespace Website.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Website.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Website.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Website.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<Website.Models.ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);
              
             roleManager.Create(new IdentityRole("Admin"));
                roleManager.Create(new IdentityRole("Agent"));
                roleManager.Create(new IdentityRole("Manager"));
                roleManager.Create(new IdentityRole("Doctor"));
              }
    }
}
