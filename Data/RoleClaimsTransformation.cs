using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SVMStudio.Data
{
    public class RoleClaimsTransformation : IClaimsTransformation
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RoleClaimsTransformation(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(principal);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var claimsIdentity = (ClaimsIdentity)principal.Identity;
                    
                    // Add role claims if they don't exist
                    foreach (var role in roles)
                    {
                        if (!principal.HasClaim(ClaimTypes.Role, role))
                        {
                            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                        }
                    }
                }
            }
            
            return principal;
        }
    }
}
