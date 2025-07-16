using System.ComponentModel.DataAnnotations;
using SVMStudio.Models;

namespace SVMStudio.ViewModels
{
    public class UserManagementListViewModel
    {
        public List<UserDetailViewModel> Users { get; set; } = new();
        public int TotalUsers { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling((double)TotalUsers / PageSize);
        public string? SearchTerm { get; set; }
        public string? FilterRole { get; set; }
        public string? FilterStatus { get; set; }
        public string? SortBy { get; set; } = "LastOnline";
        public string? SortOrder { get; set; } = "desc";
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }

    public class UserDetailViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string? Phone { get; set; }
        public string? AvatarUrl { get; set; }
        public List<string> Roles { get; set; } = new();
        public string PrimaryRole => Roles.FirstOrDefault() ?? "User";
        public DateTime? LastOnline { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public bool EmailConfirmed { get; set; }
        public int TotalBookings { get; set; }
        public int CompletedBookings { get; set; }
        public int PendingBookings { get; set; }
        public DateTime? LastBookingDate { get; set; }
        public List<UserBookingViewModel> RecentBookings { get; set; } = new();
        public List<ActivityLogViewModel> RecentActivity { get; set; } = new();
        public List<StaffNoteViewModel> StaffNotes { get; set; } = new();
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    public class UserBookingViewModel
    {
        public int Id { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public DateTime PreferredDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? Notes { get; set; }
        public string? StaffNotes { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ClientEmail { get; set; } = string.Empty;
        public string? ClientPhone { get; set; }
    }

    public class ActivityLogViewModel
    {
        public int Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public string? Details { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? IpAddress { get; set; }
    }

    public class StaffNoteViewModel
    {
        public int Id { get; set; }
        public string Note { get; set; } = string.Empty;
        public string StaffId { get; set; } = string.Empty;
        public string StaffName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class CreateStaffNoteViewModel
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        [StringLength(2000)]
        public string Note { get; set; } = string.Empty;
    }

    public class UpdateUserRoleViewModel
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        public string Role { get; set; } = string.Empty;
    }

    public class UserFilterViewModel
    {
        public string? SearchTerm { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public DateTime? RegisteredAfter { get; set; }
        public DateTime? RegisteredBefore { get; set; }
        public DateTime? LastOnlineAfter { get; set; }
        public DateTime? LastOnlineBefore { get; set; }
        public int? MinBookings { get; set; }
        public int? MaxBookings { get; set; }
        public bool? IsVipCustomer { get; set; }
        public bool? IsActive { get; set; }
    }

    public class UserStatsViewModel
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public int AdminUsers { get; set; }
        public int StaffUsers { get; set; }
        public int ClientUsers { get; set; }
        public int UsersRegisteredToday { get; set; }
        public int UsersRegisteredThisWeek { get; set; }
        public int UsersRegisteredThisMonth { get; set; }
        public int UsersOnlineToday { get; set; }
        public int TotalBookings { get; set; }
        public int BookingsToday { get; set; }
        public int BookingsThisWeek { get; set; }
        public int BookingsThisMonth { get; set; }
        public List<UserDetailViewModel> VipUsers { get; set; } = new();
        public List<UserDetailViewModel> RecentUsers { get; set; } = new();
        public List<ActivityLogViewModel> RecentActivity { get; set; } = new();
    }
}
