using AdotAqui.Models;
using AdotAqui.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Application Data
/// </summary>
namespace AdotAqui.Data
{
    /// <summary>
    /// Types of Roles
    /// </summary>
    public enum Role
    {
        Administrator,
        Employee,
        Veterinary,
        User
    }

    /// <summary>
    /// Class used to seed database when no data found.
    /// It seeds the roles and the default administrator
    /// </summary>
    public static class DataSeeder
    {
        /// <summary>
        /// Constructor responsable for seed the roles and users
        /// </summary>
        /// <param name="userManager">UserManager</param>
        /// <param name="roleManager">RoleManager</param>
        public static void SeedDatabase(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        /// <summary>
        /// Used to seed the roles
        /// </summary>
        /// <param name="roleManager">RoleManager</param>
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

        /// <summary>
        /// Seed the administrator user
        /// </summary>
        /// <param name="userManager">UserManager</param>
        private static void SeedUsers(UserManager<User> userManager)
        {
            //Adicionar Admin se não existir
            var users = userManager.GetUsersInRoleAsync(Role.Administrator.ToString()).Result;
            if (users.Count == 0)
            {
                var email = "admin@adotaqui.com";
                if (userManager.FindByNameAsync(email).Result == null)
                {
                    var user = new User { UserName = email, Email = email, Name = "Administrador", EmailConfirmed = true, Birthday = DateTime.Now.ToString() };
                    userManager.CreateAsync(user, "projectESW!2018").Wait();
                    userManager.AddToRoleAsync(user, Role.Administrator.ToString()).Wait();
                }
            }
        }

    }
}
