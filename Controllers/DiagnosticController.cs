using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SVMStudio.Controllers
{
    [Authorize]
    public class DiagnosticController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public DiagnosticController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> CheckRoles()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Content("User not found");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userInfo = $"User: {user.Email}\n";
            userInfo += $"Roles: {string.Join(", ", roles)}\n";
            userInfo += $"Is Admin: {User.IsInRole("Admin")}\n";
            userInfo += $"Claims: {string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}"))}\n";

            return Content(userInfo);
        }

        public async Task<IActionResult> FixAdminRole()
        {
            var adminEmail = "admin@svmstudio.com";
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            
            if (adminUser == null)
            {
                return Content("Admin user not found");
            }

            var roles = await _userManager.GetRolesAsync(adminUser);
            
            if (!roles.Contains("Admin"))
            {
                var result = await _userManager.AddToRoleAsync(adminUser, "Admin");
                if (result.Succeeded)
                {
                    return Content($"Admin role added successfully to {adminEmail}. Please logout and login again.");
                }
                else
                {
                    return Content($"Failed to add Admin role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            
            return Content($"Admin user already has Admin role: {string.Join(", ", roles)}");
        }
    
        [Authorize(Roles = "Admin")]
        public IActionResult TestAdminAccess()
        {
            return Content("Success! You have admin access using [Authorize(Roles = \"Admin\")]");
        }
        
        [Authorize(Policy = "AdminOnly")]
        public IActionResult TestAdminPolicy()
        {
            return Content("Success! You have admin access using [Authorize(Policy = \"AdminOnly\")]");
        }
    }
}
