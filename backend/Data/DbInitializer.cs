using backend.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {

            if (context.Roles.Any()) {
                return; // Db already initialized
            }

            // "User" role user 
            var userRole = "User";
            await roleManager.CreateAsync(new IdentityRole(userRole));

            var user = new AppUser { Email = "user@email.com", UserName = "user", FirstName = "Simple", LastName = "User" };
            await userManager.CreateAsync(user, "password");
            await userManager.AddToRoleAsync(user, userRole);

            // "Admin" role user 
            var adminRole = "Admin";
            await roleManager.CreateAsync(new IdentityRole(adminRole));

            var admin = new AppUser { Email = "admin@email.com", UserName = "admin", FirstName = "Admin", LastName = "User" };
            await userManager.CreateAsync(admin, "password");
            await userManager.AddToRoleAsync(admin, adminRole);
        }
    }
}
