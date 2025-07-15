using System.ComponentModel.DataAnnotations;
using SVMStudio.Models;

namespace SVMStudio.ViewModels
{
    public class BookingViewModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(50, ErrorMessage = "Phone number cannot exceed 50 characters")]
        public string? Phone { get; set; }

        [StringLength(100, ErrorMessage = "Instagram handle cannot exceed 100 characters")]
        public string? InstagramHandle { get; set; }

        [Required(ErrorMessage = "Please select a service type")]
        [StringLength(100, ErrorMessage = "Service type cannot exceed 100 characters")]
        public string ServiceType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a preferred date")]
        public DateTime PreferredDate { get; set; }

        [StringLength(100, ErrorMessage = "Preferred time cannot exceed 100 characters")]
        public string? PreferredTime { get; set; }

        [StringLength(100, ErrorMessage = "Duration cannot exceed 100 characters")]
        public string? Duration { get; set; }

        [StringLength(300, ErrorMessage = "Location cannot exceed 300 characters")]
        public string? Location { get; set; }

        public DateTime? EventDate { get; set; }

        public int? GuestCount { get; set; }

        [StringLength(1000, ErrorMessage = "Venue details cannot exceed 1000 characters")]
        public string? VenueDetails { get; set; }

        [StringLength(100, ErrorMessage = "Budget cannot exceed 100 characters")]
        public string? Budget { get; set; }

        [Required(ErrorMessage = "Please describe your vision")]
        [StringLength(2000, ErrorMessage = "Vision description cannot exceed 2000 characters")]
        public string VisionDescription { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Special requests cannot exceed 1000 characters")]
        public string? SpecialRequests { get; set; }

        [StringLength(100, ErrorMessage = "Referral source cannot exceed 100 characters")]
        public string? ReferralSource { get; set; }
    }

    public class ContactViewModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(50, ErrorMessage = "Phone number cannot exceed 50 characters")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Please select a service type")]
        [StringLength(100, ErrorMessage = "Service type cannot exceed 100 characters")]
        public string ServiceType { get; set; } = string.Empty;

        public DateTime? EventDate { get; set; }

        [Required(ErrorMessage = "Please enter your message")]
        [StringLength(2000, ErrorMessage = "Message cannot exceed 2000 characters")]
        public string Message { get; set; } = string.Empty;
    }

    public class GalleryViewModel
    {
        public List<Portfolio> Portfolios { get; set; } = new();
        public List<Portfolio> PortfolioItems { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public string? SelectedCategory { get; set; }
    }

    public class HomeViewModel
    {
        public List<Portfolio> FeaturedPortfolios { get; set; } = new();
        public List<Service> Services { get; set; } = new();
        public List<BlogPost> RecentBlogPosts { get; set; } = new();
    }

    public class AboutViewModel
    {
        public List<TeamMember> TeamMembers { get; set; } = new();
        public string StudioPhilosophy { get; set; } = string.Empty;
    }

    public class ServicesViewModel
    {
        public List<Service> Services { get; set; } = new();
    }

    public class BlogViewModel
    {
        public List<BlogPost> BlogPosts { get; set; } = new();
        public BlogPost? FeaturedPost { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }

    public class BlogPostViewModel
    {
        public BlogPost BlogPost { get; set; } = new();
        public List<BlogPost> RelatedPosts { get; set; } = new();
    }
}
