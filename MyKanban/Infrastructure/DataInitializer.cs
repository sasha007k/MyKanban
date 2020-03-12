using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DataInitializer
    {
        public static async Task SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, KanbanContext deratContext)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager, deratContext);
        }

        public static async Task SeedUsers(UserManager<User> userManager, KanbanContext kanbanContext)
        {
            string password = "iamadmin";
            string email = "admin@gmail.com";
            if (await userManager.FindByNameAsync(email) == null)
            {
                User user = new User();
                user.Email = email;
                user.UserName = email;
                user.Name = "Misha";

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    await userManager.AddToRoleAsync(user, "Worker");
                }
            }
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Worker", "Admin" };
            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (roleExist == false)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
