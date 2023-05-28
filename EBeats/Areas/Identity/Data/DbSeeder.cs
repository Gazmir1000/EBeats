using EBeats.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace EBeats.Areas.Identity.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            if(roleManager!=null) {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            }
         

            // creating admin

            var user = new ApplicationUser
            {
                Firstname = "Admin",
                Lastname = "Admin",
                Email = "admin@gmail.com",
                Birthday= DateTime.Now,
                UserName = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if(userManager!=null) {
                var userInDb = await userManager.FindByEmailAsync(user.Email);
                if (userInDb == null && userManager!=null)
                {
                         await userManager.CreateAsync(user, "Admin@123");
                    await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                }
               }
          
        }

    }
}
