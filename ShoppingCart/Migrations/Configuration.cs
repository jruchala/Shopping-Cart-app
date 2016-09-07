    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity.Migrations;
    using ShoppingApp.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;


namespace ShoppingApp.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<ShoppingApp.Models.ApplicationDbContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;        
        }

        protected override void Seed(ShoppingApp.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            if(!context.Users.Any(u => u.Email == "jruchala@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "jruchala@gmail.com",
                    Email = "jruchala@gmail.com",

                }, "password");
            }
            var userId = userManager.FindByEmail("jruchala@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

        }
    }
}
    
