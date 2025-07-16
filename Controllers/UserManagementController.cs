using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SVMStudio.Data;
using SVMStudio.Models;
using SVMStudio.ViewModels;
using System.Text;

namespace SVMStudio.Controllers
{
    [Authorize(Policy = "StaffOnly")]
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: UserManagement
        public async Task<IActionResult> Index(
            string? searchTerm, 
            string? filterRole, 
            string? filterStatus, 
            int page = 1, 
            int pageSize = 10, 
            string sortBy = "LastOnline", 
            string sortOrder = "desc")
        {
            var usersQuery = _userManager.Users.AsQueryable();

            // Search functionality
            if (!string.IsNullOrEmpty(searchTerm))
            {
                usersQuery = usersQuery.Where(u => 
                    u.UserName!.Contains(searchTerm) || 
                    u.Email!.Contains(searchTerm));
            }

            // Get user profiles
            var userProfiles = await _context.UserProfiles
                .Include(up => up.Bookings)
                .Include(up => up.ActivityLogs.OrderByDescending(al => al.CreatedAt).Take(5))
                .ToListAsync();

            var userProfileDict = userProfiles.ToDictionary(up => up.Id, up => up);

            // Build user list
            var userList = new List<UserDetailViewModel>();
            
            foreach (var user in await usersQuery.ToListAsync())
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userProfile = userProfileDict.GetValueOrDefault(user.Id);
                
                // Apply role filter
                if (!string.IsNullOrEmpty(filterRole) && !roles.Contains(filterRole))
                    continue;

                // Apply status filter
                if (!string.IsNullOrEmpty(filterStatus))
                {
                    var isActive = userProfile?.IsActive ?? true;
                    if (filterStatus == "Active" && !isActive) continue;
                    if (filterStatus == "Inactive" && isActive) continue;
                }

                var userDetail = new UserDetailViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName ?? "",
                    Email = user.Email ?? "",
                    FirstName = userProfile?.FirstName,
                    LastName = userProfile?.LastName,
                    Phone = userProfile?.Phone,
                    AvatarUrl = userProfile?.AvatarUrl,
                    Roles = roles.ToList(),
                    LastOnline = userProfile?.LastOnline,
                    CreatedAt = userProfile?.CreatedAt ?? DateTime.UtcNow,
                    IsActive = userProfile?.IsActive ?? true,
                    EmailConfirmed = user.EmailConfirmed,
                    TotalBookings = userProfile?.Bookings?.Count ?? 0,
                    CompletedBookings = userProfile?.Bookings?.Count(b => b.Status == "Completed") ?? 0,
                    PendingBookings = userProfile?.Bookings?.Count(b => b.Status == "Pending") ?? 0,
                    LastBookingDate = userProfile?.Bookings?.OrderByDescending(b => b.CreatedAt).FirstOrDefault()?.CreatedAt,
                    Bio = userProfile?.Bio
                };

                userList.Add(userDetail);
            }

            // Sorting
            userList = sortBy.ToLower() switch
            {
                "username" => sortOrder == "asc" ? userList.OrderBy(u => u.UserName).ToList() : userList.OrderByDescending(u => u.UserName).ToList(),
                "email" => sortOrder == "asc" ? userList.OrderBy(u => u.Email).ToList() : userList.OrderByDescending(u => u.Email).ToList(),
                "role" => sortOrder == "asc" ? userList.OrderBy(u => u.PrimaryRole).ToList() : userList.OrderByDescending(u => u.PrimaryRole).ToList(),
                "createdat" => sortOrder == "asc" ? userList.OrderBy(u => u.CreatedAt).ToList() : userList.OrderByDescending(u => u.CreatedAt).ToList(),
                "totalbookings" => sortOrder == "asc" ? userList.OrderBy(u => u.TotalBookings).ToList() : userList.OrderByDescending(u => u.TotalBookings).ToList(),
                _ => sortOrder == "asc" ? userList.OrderBy(u => u.LastOnline).ToList() : userList.OrderByDescending(u => u.LastOnline).ToList()
            };

            // Pagination
            var totalUsers = userList.Count;
            var pagedUsers = userList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new UserManagementListViewModel
            {
                Users = pagedUsers,
                TotalUsers = totalUsers,
                CurrentPage = page,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                FilterRole = filterRole,
                FilterStatus = filterStatus,
                SortBy = sortBy,
                SortOrder = sortOrder
            };

            return View(viewModel);
        }

        // GET: UserManagement/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles
                .Include(up => up.Bookings.OrderByDescending(b => b.CreatedAt))
                .Include(up => up.ActivityLogs.OrderByDescending(al => al.CreatedAt))
                .Include(up => up.StaffNotes.OrderByDescending(sn => sn.CreatedAt))
                .FirstOrDefaultAsync(up => up.Id == id);

            var roles = await _userManager.GetRolesAsync(user);

            var userDetail = new UserDetailViewModel
            {
                Id = user.Id,
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",
                FirstName = userProfile?.FirstName,
                LastName = userProfile?.LastName,
                Phone = userProfile?.Phone,
                AvatarUrl = userProfile?.AvatarUrl,
                Roles = roles.ToList(),
                LastOnline = userProfile?.LastOnline,
                CreatedAt = userProfile?.CreatedAt ?? DateTime.UtcNow,
                IsActive = userProfile?.IsActive ?? true,
                EmailConfirmed = user.EmailConfirmed,
                Bio = userProfile?.Bio,
                DateOfBirth = userProfile?.DateOfBirth,
                TotalBookings = userProfile?.Bookings?.Count ?? 0,
                CompletedBookings = userProfile?.Bookings?.Count(b => b.Status == "Completed") ?? 0,
                PendingBookings = userProfile?.Bookings?.Count(b => b.Status == "Pending") ?? 0,
                LastBookingDate = userProfile?.Bookings?.OrderByDescending(b => b.CreatedAt).FirstOrDefault()?.CreatedAt,
                RecentBookings = userProfile?.Bookings?.Select(b => new UserBookingViewModel
                {
                    Id = b.Id,
                    ServiceType = b.ServiceType,
                    PreferredDate = b.PreferredDate,
                    Status = b.Status,
                    CreatedAt = b.CreatedAt,
                    CompletedAt = b.CompletedAt,
                    Notes = b.Notes,
                    StaffNotes = b.StaffNotes,
                    ClientName = b.ClientName,
                    ClientEmail = b.ClientEmail,
                    ClientPhone = b.ClientPhone
                }).ToList() ?? new List<UserBookingViewModel>(),
                RecentActivity = userProfile?.ActivityLogs?.Select(al => new ActivityLogViewModel
                {
                    Id = al.Id,
                    Action = al.Action,
                    Details = al.Details,
                    CreatedAt = al.CreatedAt,
                    IpAddress = al.IpAddress
                }).ToList() ?? new List<ActivityLogViewModel>(),
                StaffNotes = userProfile?.StaffNotes?.Select(sn => new StaffNoteViewModel
                {
                    Id = sn.Id,
                    Note = sn.Note,
                    StaffId = sn.StaffId,
                    StaffName = "Staff", // TODO: Get staff name
                    CreatedAt = sn.CreatedAt
                }).ToList() ?? new List<StaffNoteViewModel>()
            };

            return View(userDetail);
        }

        // POST: UserManagement/ToggleStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                // Create user profile if it doesn't exist
                userProfile = new UserProfile
                {
                    Id = id,
                    IsActive = false,
                    CreatedAt = DateTime.UtcNow,
                    LastOnline = DateTime.UtcNow
                };
                _context.UserProfiles.Add(userProfile);
            }
            else
            {
                userProfile.IsActive = !userProfile.IsActive;
            }

            await _context.SaveChangesAsync();

            // Log activity
            await LogActivity(id, userProfile.IsActive ? "User Activated" : "User Deactivated", 
                $"User status changed to {(userProfile.IsActive ? "Active" : "Inactive")}");

            return RedirectToAction(nameof(Index));
        }

        // POST: UserManagement/UpdateRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateRole(UpdateUserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { id = model.UserId });
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            // Remove all existing roles
            var existingRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, existingRoles);

            // Add new role
            await _userManager.AddToRoleAsync(user, model.Role);

            // Log activity
            await LogActivity(model.UserId, "Role Updated", $"Role changed to {model.Role}");

            return RedirectToAction(nameof(Details), new { id = model.UserId });
        }

        // POST: UserManagement/AddNote
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNote(CreateStaffNoteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { id = model.UserId });
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Forbid();
            }

            var staffNote = new StaffNote
            {
                UserId = model.UserId,
                StaffId = currentUser.Id,
                Note = model.Note,
                CreatedAt = DateTime.UtcNow
            };

            _context.StaffNotes.Add(staffNote);
            await _context.SaveChangesAsync();

            // Log activity
            await LogActivity(model.UserId, "Staff Note Added", "Staff note was added to user profile");

            return RedirectToAction(nameof(Details), new { id = model.UserId });
        }

        // GET: UserManagement/Export
        public async Task<IActionResult> Export()
        {
            var users = await _userManager.Users.ToListAsync();
            var userProfiles = await _context.UserProfiles
                .Include(up => up.Bookings)
                .ToListAsync();

            var userProfileDict = userProfiles.ToDictionary(up => up.Id, up => up);

            var csv = new StringBuilder();
            csv.AppendLine("Username,Email,Full Name,Phone,Role,Status,Registration Date,Last Online,Total Bookings,Completed Bookings");

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userProfile = userProfileDict.GetValueOrDefault(user.Id);
                
                var fullName = $"{userProfile?.FirstName} {userProfile?.LastName}".Trim();
                var primaryRole = roles.FirstOrDefault() ?? "User";
                var status = userProfile?.IsActive ?? true ? "Active" : "Inactive";
                var totalBookings = userProfile?.Bookings?.Count ?? 0;
                var completedBookings = userProfile?.Bookings?.Count(b => b.Status == "Completed") ?? 0;

                csv.AppendLine($"\"{user.UserName}\",\"{user.Email}\",\"{fullName}\",\"{userProfile?.Phone}\",\"{primaryRole}\",\"{status}\",\"{userProfile?.CreatedAt:yyyy-MM-dd}\",\"{userProfile?.LastOnline:yyyy-MM-dd}\",{totalBookings},{completedBookings}");
            }

            var fileName = $"users_export_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", fileName);
        }

        // GET: UserManagement/Stats
        public async Task<IActionResult> Stats()
        {
            var users = await _userManager.Users.ToListAsync();
            var userProfiles = await _context.UserProfiles
                .Include(up => up.Bookings)
                .Include(up => up.ActivityLogs.OrderByDescending(al => al.CreatedAt).Take(10))
                .ToListAsync();

            var userProfileDict = userProfiles.ToDictionary(up => up.Id, up => up);

            var now = DateTime.UtcNow;
            var today = now.Date;
            var thisWeek = today.AddDays(-(int)today.DayOfWeek);
            var thisMonth = new DateTime(today.Year, today.Month, 1);

            var stats = new UserStatsViewModel
            {
                TotalUsers = users.Count,
                ActiveUsers = userProfiles.Count(up => up.IsActive),
                InactiveUsers = userProfiles.Count(up => !up.IsActive),
                UsersRegisteredToday = userProfiles.Count(up => up.CreatedAt.Date == today),
                UsersRegisteredThisWeek = userProfiles.Count(up => up.CreatedAt >= thisWeek),
                UsersRegisteredThisMonth = userProfiles.Count(up => up.CreatedAt >= thisMonth),
                UsersOnlineToday = userProfiles.Count(up => up.LastOnline.Date == today),
                TotalBookings = userProfiles.Sum(up => up.Bookings.Count),
                BookingsToday = userProfiles.Sum(up => up.Bookings.Count(b => b.CreatedAt.Date == today)),
                BookingsThisWeek = userProfiles.Sum(up => up.Bookings.Count(b => b.CreatedAt >= thisWeek)),
                BookingsThisMonth = userProfiles.Sum(up => up.Bookings.Count(b => b.CreatedAt >= thisMonth))
            };

            // Count users by role
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var primaryRole = roles.FirstOrDefault() ?? "User";
                
                switch (primaryRole)
                {
                    case "Admin":
                        stats.AdminUsers++;
                        break;
                    case "Staff":
                        stats.StaffUsers++;
                        break;
                    default:
                        stats.ClientUsers++;
                        break;
                }
            }

            // VIP users (users with 5+ bookings)
            var vipUserProfiles = userProfiles.Where(up => up.Bookings.Count >= 5).Take(10);
            stats.VipUsers = await BuildUserDetailViewModels(vipUserProfiles);

            // Recent users
            var recentUserProfiles = userProfiles.OrderByDescending(up => up.CreatedAt).Take(10);
            stats.RecentUsers = await BuildUserDetailViewModels(recentUserProfiles);

            // Recent activity
            stats.RecentActivity = userProfiles
                .SelectMany(up => up.ActivityLogs)
                .OrderByDescending(al => al.CreatedAt)
                .Take(20)
                .Select(al => new ActivityLogViewModel
                {
                    Id = al.Id,
                    Action = al.Action,
                    Details = al.Details,
                    CreatedAt = al.CreatedAt,
                    IpAddress = al.IpAddress
                })
                .ToList();

            return View(stats);
        }

        private async Task<List<UserDetailViewModel>> BuildUserDetailViewModels(IEnumerable<UserProfile> userProfiles)
        {
            var userDetailViewModels = new List<UserDetailViewModel>();

            foreach (var userProfile in userProfiles)
            {
                var user = await _userManager.FindByIdAsync(userProfile.Id);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    
                    var userDetail = new UserDetailViewModel
                    {
                        Id = user.Id,
                        UserName = user.UserName ?? "",
                        Email = user.Email ?? "",
                        FirstName = userProfile.FirstName,
                        LastName = userProfile.LastName,
                        Phone = userProfile.Phone,
                        AvatarUrl = userProfile.AvatarUrl,
                        Roles = roles.ToList(),
                        LastOnline = userProfile.LastOnline,
                        CreatedAt = userProfile.CreatedAt,
                        IsActive = userProfile.IsActive,
                        EmailConfirmed = user.EmailConfirmed,
                        TotalBookings = userProfile.Bookings.Count,
                        CompletedBookings = userProfile.Bookings.Count(b => b.Status == "Completed"),
                        PendingBookings = userProfile.Bookings.Count(b => b.Status == "Pending"),
                        LastBookingDate = userProfile.Bookings.OrderByDescending(b => b.CreatedAt).FirstOrDefault()?.CreatedAt,
                        Bio = userProfile.Bio
                    };

                    userDetailViewModels.Add(userDetail);
                }
            }

            return userDetailViewModels;
        }

        private async Task LogActivity(string userId, string action, string details)
        {
            var activityLog = new ActivityLog
            {
                UserId = userId,
                Action = action,
                Details = details,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserAgent = HttpContext.Request.Headers["User-Agent"].ToString(),
                CreatedAt = DateTime.UtcNow
            };

            _context.ActivityLogs.Add(activityLog);
            await _context.SaveChangesAsync();
        }
    }
}
