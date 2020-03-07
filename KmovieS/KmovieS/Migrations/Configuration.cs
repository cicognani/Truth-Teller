namespace KmovieS.Migrations
{
    using KmovieS.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KmovieS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "KmovieS.Models.ApplicationDbContext";
        }

        protected override void Seed(KmovieS.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var userAdmin = new ApplicationUser()
            {
                UserName = "SuperPowerUser",
                Email = "cicognani.vittorio@gmail.com",
                EmailConfirmed = true,
                FirstName = "Vittorio",
                LastName = "Cicognani",
                Company = "Spekno",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(userAdmin, "MySuperP@ssword!");


            var userSimple = new ApplicationUser()
            {
                UserName = "SimpleUser",
                Email = "cicognani@movinsoft.com",
                EmailConfirmed = true,
                FirstName = "Vittorio",
                LastName = "Cicognani",
                Company= "Movinsoft",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(userSimple, "MySimpleP@ssword!");




            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("SuperPowerUser");

            manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin" });

            var simpleUser = manager.FindByName("SimpleUser");

            manager.AddToRoles(simpleUser.Id, new string[] { "User" });



        }
    }
}
