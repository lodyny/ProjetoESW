using AdotAqui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Data
{
    public enum Role
    {
        Administrator,
        Employee,
        Veterinary,
        User
    }

    public static class DataSeeder
    {
        public static void SeedDatabase(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            //Adicionar Roles que se encontram no enum Roles
            var roles = Enum.GetValues(typeof(Role));
            foreach (var role in roles)
            {
                string roleName = role.ToString();
                if (roleManager.FindByNameAsync(roleName).Result == null)
                {
                    roleManager.CreateAsync(new IdentityRole<int>(roleName)).Wait();
                }
            }
        }


        private static void SeedUsers(UserManager<User> userManager)
        {
            //Adicionar Admin se não existir
            var users = userManager.GetUsersInRoleAsync(Role.Administrator.ToString()).Result;
            if (users.Count == 0)
            {
                var email = "admin@adotaqui.com";
                if (userManager.FindByNameAsync(email).Result == null)
                {
                    var user = new User { UserName = email, Email = email, Name = "Administrador", EmailConfirmed = true };
                    userManager.CreateAsync(user, "projectESW!2018").Wait();
                    userManager.AddToRoleAsync(user, Role.Administrator.ToString()).Wait();
                }
            }
        }

    }
}
