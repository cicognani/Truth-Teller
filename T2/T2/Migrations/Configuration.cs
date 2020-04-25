namespace T2.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using T2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<T2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "T2.Models.ApplicationDbContext";
        }

        protected override void Seed(T2.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.


            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var userAdmin = new ApplicationUser()
            {

                UserName = "SuperPowerUser",
                Email = "vittorio.cicognani@spekno.eu",
                EmailConfirmed = true,
                FirstName = "Vittorio",
                LastName = "Cicognani",
                Level = 1,
                JoinDate = DateTime.Now,
                NCorrectAnswers = 0,
                NFaultAnswers = 0
            };

            manager.Create(userAdmin, "MySuperP@ssword!!");


            var userCertificator = new ApplicationUser()
            {
                UserName = "CertificatorUser",
                Email = "maurizio.ori@spekno.eu",
                EmailConfirmed = true,
                FirstName = "Maurizio   ",
                LastName = "Ori",
                Level = 2,
                JoinDate = DateTime.Now,
                NCorrectAnswers = 0,
                NFaultAnswers = 0
            };

            manager.Create(userCertificator, "MyCertificatorP@ssword!!");


            var userSimple = new ApplicationUser()
            {
                UserName = "SimpleUser",
                Email = "matteo.argnani@spekno.eu",
                EmailConfirmed = true,
                FirstName = "Matteo",
                LastName = "Argnani",
                Level = 3,
                JoinDate = DateTime.Now,
                NCorrectAnswers = 0,
                NFaultAnswers = 0
            };

            manager.Create(userSimple, "MySimpleP@ssword!!!");



            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("SuperPowerUser");

            manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin" });

            var certificatorUser = manager.FindByName("CertificatorUser");

            manager.AddToRoles(certificatorUser.Id, new string[] { "Admin" });

            var simpleUser = manager.FindByName("SimpleUser");

            manager.AddToRoles(simpleUser.Id, new string[] { "User" });



        }
    }
}
