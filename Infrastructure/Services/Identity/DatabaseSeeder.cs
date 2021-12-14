using Infrastructure.Context;
using Infrastructure.Helpers;
using Application.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Infrastructure.Models.Identity;
using Shared.Constants.Role;
using Shared.Constants.User;
using Shared.Permission;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.Identity
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<DatabaseSeeder> _logger;

        public DatabaseSeeder(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext db, ILogger<DatabaseSeeder> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _logger = logger;
        }

        public void Initialize()
        {
            AddAdministrator();
            AddBasicUser();
        }

        private void AddAdministrator()
        {
            Task.Run(async () =>
            {
                //Check if Role Exists
                var adminRole = new ApplicationRole(RoleConstants.AdministratorRole, "Administrator role with full permissions");
                var adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                if (adminRoleInDb == null)
                {
                    await _roleManager.CreateAsync(adminRole);
                    adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                    _logger.LogInformation("Seeded Administrator Role.");
                }
                //Check if User Exists
                var adminUser = new ApplicationUser
                {
                    FirstName = "Junior",
                    LastName = "Matlou",
                    Email = "junior@rus.com",
                    UserName = "JuniorM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    IsApproved = true
                };
                var adminUserInDb = await _userManager.FindByEmailAsync(adminUser.Email);
                if (adminUserInDb == null)
                {
                    await _userManager.CreateAsync(adminUser, UserConstants.DefaultPassword);
                    var result = await _userManager.AddToRoleAsync(adminUser, RoleConstants.AdministratorRole);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Seeded Default Admin User.");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError(error.Description);
                        }
                    }
                }
                foreach (var permission in Permissions.GetRegisteredPermissions())
                {
                    await _roleManager.AddPermissionClaim(adminRoleInDb, permission);
                }
            }).GetAwaiter().GetResult();
        }

        private void AddBasicUser()
        {
            Task.Run(async () =>
            {
                //Check if Role Exists
                var basicRole = new ApplicationRole(RoleConstants.BasicRole, "Basic role with default permissions");
                var basicRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.BasicRole);
                if (basicRoleInDb == null)
                {
                    await _roleManager.CreateAsync(basicRole);
                    _logger.LogInformation("Seeded Basic Role.");
                }
                //Check if User Exists
                var basicUser = new ApplicationUser
                {
                    FirstName = "Sipati",
                    LastName = "Matlou",
                    Email = "smatlou@rus.com",
                    UserName = "SeipatiM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };
                var basicUserInDb = await _userManager.FindByEmailAsync(basicUser.Email);
                if (basicUserInDb == null)
                {
                    await _userManager.CreateAsync(basicUser, UserConstants.DefaultPassword);
                    await _userManager.AddToRoleAsync(basicUser, RoleConstants.BasicRole);
                    _logger.LogInformation("Seeded User with Basic Role.");
                }
            }).GetAwaiter().GetResult();
        }
    }
}
