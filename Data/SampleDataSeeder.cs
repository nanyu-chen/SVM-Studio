using SVMStudio.Data;
using SVMStudio.Models;

namespace SVMStudio.Data
{
    public static class SampleDataSeeder
    {
        public static async Task SeedSampleData(ApplicationDbContext context)
        {
            // Only seed if database is empty
            if (context.Portfolios.Any() || context.Services.Any() || context.TeamMembers.Any() || context.BlogPosts.Any())
            {
                return;
            }

            // Sample Portfolio Items
            var portfolioItems = new List<Portfolio>
            {
                new Portfolio
                {
                    Title = "Romantic Garden Wedding",
                    Description = "A dreamy outdoor wedding capturing the essence of romance in a botanical garden setting.",
                    ImageUrl = "https://images.unsplash.com/photo-1519741497674-611481863552?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    Category = "Wedding",
                    SortOrder = 1,
                    IsFeatured = true,
                    CreatedDate = DateTime.UtcNow.AddDays(-30)
                },
                new Portfolio
                {
                    Title = "Corporate Headshots",
                    Description = "Professional headshots for business executives and entrepreneurs.",
                    ImageUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    Category = "Portrait",
                    SortOrder = 2,
                    IsFeatured = true,
                    CreatedDate = DateTime.UtcNow.AddDays(-25)
                },
                new Portfolio
                {
                    Title = "Fashion Editorial",
                    Description = "High-fashion editorial shoot featuring avant-garde styling and dramatic lighting.",
                    ImageUrl = "https://images.unsplash.com/photo-1469334031218-e382a71b716b?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    Category = "Fashion",
                    SortOrder = 3,
                    IsFeatured = true,
                    CreatedDate = DateTime.UtcNow.AddDays(-20)
                },
                new Portfolio
                {
                    Title = "Family Portrait Session",
                    Description = "Warm and natural family portraits capturing genuine moments and connections.",
                    ImageUrl = "https://images.unsplash.com/photo-1511895426328-dc8714191300?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    Category = "Portrait",
                    SortOrder = 4,
                    IsFeatured = false,
                    CreatedDate = DateTime.UtcNow.AddDays(-15)
                },
                new Portfolio
                {
                    Title = "Product Photography",
                    Description = "Commercial product photography for luxury jewelry and accessories.",
                    ImageUrl = "https://images.unsplash.com/photo-1515562141207-7a88fb7ce338?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    Category = "Commercial",
                    SortOrder = 5,
                    IsFeatured = false,
                    CreatedDate = DateTime.UtcNow.AddDays(-10)
                },
                new Portfolio
                {
                    Title = "Maternity Session",
                    Description = "Elegant maternity photography celebrating the beauty of pregnancy.",
                    ImageUrl = "https://images.unsplash.com/photo-1578662996442-48f60103fc96?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    Category = "Portrait",
                    SortOrder = 6,
                    IsFeatured = false,
                    CreatedDate = DateTime.UtcNow.AddDays(-5)
                }
            };

            // Sample Services
            var services = new List<Service>
            {
                new Service
                {
                    Name = "Wedding Photography",
                    Description = "Complete wedding day coverage including ceremony, reception, and portrait sessions. Includes edited high-resolution images and online gallery.",
                    ImageUrl = "https://images.unsplash.com/photo-1606216794074-735e91aa2c92?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    Price = 2500,
                    Duration = "8-10 hours",
                    SortOrder = 1,
                    Icon = "fas fa-heart"
                },
                new Service
                {
                    Name = "Portrait Session",
                    Description = "Professional portrait photography for individuals, couples, or families. Includes styling consultation and 20+ edited images.",
                    ImageUrl = "https://images.unsplash.com/photo-1531746020798-e6953c6e8e04?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    Price = 350,
                    Duration = "1-2 hours",
                    SortOrder = 2,
                    Icon = "fas fa-user-astronaut"
                },
                new Service
                {
                    Name = "Fashion Editorial",
                    Description = "High-fashion editorial photography for magazines, brands, and portfolios. Includes creative direction and post-production.",
                    ImageUrl = "https://images.unsplash.com/photo-1445205170230-053b83016050?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    Price = 1500,
                    Duration = "4-6 hours",
                    SortOrder = 3,
                    Icon = "fas fa-crown"
                },
                new Service
                {
                    Name = "Commercial Photography",
                    Description = "Product photography, headshots, and branding imagery for businesses. Includes licensing and usage rights.",
                    ImageUrl = "https://images.unsplash.com/photo-1560472354-b33ff0c44a43?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    Price = 0, // Contact for pricing
                    Duration = "2-4 hours",
                    SortOrder = 4,
                    Icon = "fas fa-briefcase"
                },
                new Service
                {
                    Name = "Event Photography",
                    Description = "Professional event coverage for corporate events, parties, and special occasions. Includes candid and formal shots.",
                    ImageUrl = "https://images.unsplash.com/photo-1511795409834-ef04bbd61622?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    Price = 150,
                    Duration = "Per hour",
                    SortOrder = 5,
                    Icon = "fas fa-calendar-alt"
                }
            };

            // Sample Team Members
            var teamMembers = new List<TeamMember>
            {
                new TeamMember
                {
                    Name = "Sarah Mitchell",
                    Role = "Lead Photographer & Creative Director",
                    Bio = "With over 12 years of experience in fashion and wedding photography, Sarah brings an artistic eye and passion for storytelling to every shoot. She specializes in capturing authentic moments and creating timeless images.",
                    ImageUrl = "https://images.unsplash.com/photo-1494790108755-2616b612b786?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    SortOrder = 1
                },
                new TeamMember
                {
                    Name = "Michael Chen",
                    Role = "Senior Photographer",
                    Bio = "Michael specializes in commercial and portrait photography, bringing technical expertise and creative vision to every project. His background in fine arts informs his approach to composition and lighting.",
                    ImageUrl = "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    SortOrder = 2
                },
                new TeamMember
                {
                    Name = "Emily Rodriguez",
                    Role = "Stylist & Creative Assistant",
                    Bio = "Emily brings fashion expertise and styling knowledge to our team. She ensures every shoot has the perfect aesthetic and helps clients feel confident and beautiful in front of the camera.",
                    ImageUrl = "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    SortOrder = 3
                }
            };

            // Sample Blog Posts
            var blogPosts = new List<BlogPost>
            {
                new BlogPost
                {
                    Title = "5 Tips for the Perfect Wedding Photos",
                    Content = "Planning your wedding photography is one of the most important decisions you'll make. Here are our top 5 tips to ensure your wedding photos are everything you've dreamed of...",
                    Excerpt = "Expert advice on getting the most out of your wedding photography session.",
                    Author = "Sarah Mitchell",
                    ImageUrl = "https://images.unsplash.com/photo-1511285560929-80b456fea0bc?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    IsPublished = true,
                    PublishedAt = DateTime.UtcNow.AddDays(-14),
                    CreatedAt = DateTime.UtcNow.AddDays(-15)
                },
                new BlogPost
                {
                    Title = "The Art of Portrait Photography",
                    Content = "Portrait photography is more than just taking pictures of people. It's about capturing their essence, their personality, and their unique story...",
                    Excerpt = "Discover the techniques and philosophy behind compelling portrait photography.",
                    Author = "Michael Chen",
                    ImageUrl = "https://images.unsplash.com/photo-1544005313-94ddf0286df2?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    IsPublished = true,
                    PublishedAt = DateTime.UtcNow.AddDays(-7),
                    CreatedAt = DateTime.UtcNow.AddDays(-8)
                },
                new BlogPost
                {
                    Title = "Behind the Scenes: Fashion Editorial Shoot",
                    Content = "Take a look behind the scenes of our latest fashion editorial shoot. From concept development to final image selection, we'll walk you through our creative process...",
                    Excerpt = "An inside look at how we create stunning fashion editorials.",
                    Author = "Emily Rodriguez",
                    ImageUrl = "https://images.unsplash.com/photo-1551698618-1dfe5d97d256?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2340&q=80",
                    IsPublished = false,
                    PublishedAt = null,
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                }
            };

            // Add sample data to context
            context.Portfolios.AddRange(portfolioItems);
            context.Services.AddRange(services);
            context.TeamMembers.AddRange(teamMembers);
            context.BlogPosts.AddRange(blogPosts);

            // Save changes
            await context.SaveChangesAsync();
        }
    }
}
