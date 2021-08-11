using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public static class IdentityDataInitializer
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                roleManager.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "Admin" });
            }

        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("hozaifa").Result == null)

            {
                var user = new IdentityUser { UserName = "Hozaifa", Email = "hozaifa69@yahoo.com", NormalizedEmail = "Hozaifa Mohammed", PhoneNumber = "0914939992" };
                var result = userManager.CreateAsync(user, "Hozaifa@12345").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "admin").Wait();
                }
            }
        }
    }
}
