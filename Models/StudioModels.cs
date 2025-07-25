using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SVMStudio.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        public int SortOrder { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }

    public class Service
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;
        
        public decimal Price { get; set; }
        
        [StringLength(100)]
        public string? Duration { get; set; }
        
        public List<string> Includes { get; set; } = new();
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; } = 0;
        
        [StringLength(500)]
        public string? IconUrl { get; set; }
        
        [StringLength(500)]
        public string? ImageUrl { get; set; }
        
        [StringLength(100)]
        public string? Icon { get; set; } // FontAwesome or similar icon class
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Booking
    {
        public int Id { get; set; }
        
        [StringLength(450)]
        public string? UserId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string ClientName { get; set; } = string.Empty;
        
        // Add convenience properties for view compatibility
        public string Name => ClientName;
        
        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string ClientEmail { get; set; } = string.Empty;
        
        // Add convenience properties for view compatibility
        public string Email => ClientEmail;
        
        [StringLength(50)]
        public string? ClientPhone { get; set; }
        
        // Add convenience properties for view compatibility
        public string? Phone => ClientPhone;
        
        [Required]
        [StringLength(100)]
        public string ServiceType { get; set; } = string.Empty;
        
        [Required]
        public DateTime PreferredDate { get; set; }
        
        [StringLength(2000)]
        public string? Notes { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        
        [StringLength(1000)]
        public string? StaffNotes { get; set; }
        
        // Navigation properties
        public virtual UserProfile? User { get; set; }
    }

    public class BlogPost
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(300)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Content { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Author { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? FeaturedImageUrl { get; set; }
        
        [StringLength(500)]
        public string? ImageUrl { get; set; }
        
        [StringLength(1000)]
        public string? Excerpt { get; set; }
        
        [StringLength(100)]
        public string? Category { get; set; }
        
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
        
        public bool IsPublished { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PublishedAt { get; set; }
    }

    public class TeamMember
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Role { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Bio { get; set; }
        
        [StringLength(500)]
        public string? ImageUrl { get; set; }
        
        [StringLength(200)]
        public string? InstagramUrl { get; set; }
        
        [StringLength(200)]
        public string? LinkedInUrl { get; set; }
        
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; } = 0;
    }

    public class ContactMessage
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? Phone { get; set; }
        
        [StringLength(300)]
        public string? Subject { get; set; }
        
        [Required]
        [StringLength(2000)]
        public string Message { get; set; } = string.Empty;
        
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    // Extended User Profile
    public class UserProfile
    {
        public string Id { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? FirstName { get; set; }
        
        [StringLength(100)]
        public string? LastName { get; set; }
        
        [StringLength(20)]
        public string? Phone { get; set; }
        
        [StringLength(500)]
        public string? AvatarUrl { get; set; }
        
        [StringLength(1000)]
        public string? Bio { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastOnline { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public virtual List<Booking> Bookings { get; set; } = new();
        public virtual List<ActivityLog> ActivityLogs { get; set; } = new();
        public virtual List<StaffNote> StaffNotes { get; set; } = new();
    }

    // Activity Log for tracking user actions
    public class ActivityLog
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Action { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Details { get; set; }
        
        [StringLength(50)]
        public string? IpAddress { get; set; }
        
        [StringLength(500)]
        public string? UserAgent { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual UserProfile? User { get; set; }
    }

    // Staff Notes for internal use
    public class StaffNote
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        public string StaffId { get; set; } = string.Empty;
        
        [Required]
        [StringLength(2000)]
        public string Note { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public virtual UserProfile? User { get; set; }
    }
}
