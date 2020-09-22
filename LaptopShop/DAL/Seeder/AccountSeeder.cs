using LaptopShop.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.DAL.Seeder
{
    public class AccountSeeder
    {
        public static void SeedUsers(UserManager<DbUser> userManager,
            RoleManager<DbRole> roleManager)
        {
            var count = roleManager.Roles.Count();
            var roleName = "Admin";
            if (roleManager.FindByNameAsync(roleName).Result == null)
            {
                var result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
            }

            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                string email = "admin@gmail.com";
                var user = new DbUser
                {
                    Email = email,
                    UserName = email,
                    PhoneNumber = "+11(111)111-11-11"
                };
                var result = userManager.CreateAsync(user, "8Ki9x9-3of+s").Result;
                result = userManager.AddToRoleAsync(user, roleName).Result;
            }
            if (userManager.FindByEmailAsync("novakvova@gmail.com").Result == null)
            {
                string email = "novakvova@gmail.com";
                var user = new DbUser
                {
                    Email = email,
                    UserName = email,
                    PhoneNumber = "+21(111)111-11-11"
                };
                var result = userManager.CreateAsync(user, "R2-=x*x1PxsE9").Result;
                result = userManager.AddToRoleAsync(user, roleName).Result;
            }
        }
    }
}
