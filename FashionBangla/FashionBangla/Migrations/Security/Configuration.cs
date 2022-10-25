namespace FashionBangla.Migrations.Security
{
    using FashionBangla.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FashionBangla.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Security";
        }

        protected override void Seed(FashionBangla.Models.ApplicationDbContext context)
        {
            var rolestore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var rolemanager = new RoleManager<IdentityRole>(rolestore);
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var result = rolemanager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                var result = rolemanager.CreateAsync(new IdentityRole("Member"));
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u => u.UserName == "admin1"))
            {
                var u = new ApplicationUser { UserName = "admin1" };
                var result = userManager.CreateAsync(u, "@password");
                if (result.Result.Succeeded)
                {
                    userManager.AddToRole(u.Id, "Admin");
                }
            }
        }
    }
}
