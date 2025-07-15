using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SVMStudio.Data;
using SVMStudio.Models;

namespace SVMStudio.Controllers
{
    // [Authorize] // Temporarily disabled for development
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dashboard = new
            {
                TotalBookings = await _context.Bookings.CountAsync(),
                PendingBookings = await _context.Bookings.CountAsync(b => b.Status == "Pending"),
                TotalPortfolios = await _context.Portfolios.CountAsync(),
                TotalMessages = await _context.ContactMessages.CountAsync(m => !m.IsRead),
                RecentBookings = await _context.Bookings
                    .OrderByDescending(b => b.CreatedAt)
                    .Take(5)
                    .ToListAsync(),
                RecentMessages = await _context.ContactMessages
                    .OrderByDescending(m => m.CreatedAt)
                    .Take(5)
                    .ToListAsync()
            };

            return View(dashboard);
        }

        // Portfolio Management
        public async Task<IActionResult> Portfolios()
        {
            var portfolios = await _context.Portfolios
                .OrderBy(p => p.SortOrder)
                .ThenByDescending(p => p.CreatedAt)
                .ToListAsync();
            return View(portfolios);
        }

        public IActionResult CreatePortfolio()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePortfolio(Portfolio portfolio)
        {
            if (ModelState.IsValid)
            {
                _context.Portfolios.Add(portfolio);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Portfolio item created successfully!";
                return RedirectToAction(nameof(Portfolios));
            }
            return View(portfolio);
        }

        public async Task<IActionResult> EditPortfolio(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio == null)
            {
                return NotFound();
            }
            return View(portfolio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPortfolio(int id, Portfolio portfolio)
        {
            if (id != portfolio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolio);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Portfolio item updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PortfolioExists(portfolio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Portfolios));
            }
            return View(portfolio);
        }

        public async Task<IActionResult> DeletePortfolio(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio == null)
            {
                return NotFound();
            }
            return View(portfolio);
        }

        [HttpPost, ActionName("DeletePortfolio")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePortfolioConfirmed(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio != null)
            {
                _context.Portfolios.Remove(portfolio);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Portfolio item deleted successfully!";
            }
            return RedirectToAction(nameof(Portfolios));
        }

        // Services Management
        public async Task<IActionResult> Services()
        {
            var services = await _context.Services
                .OrderBy(s => s.SortOrder)
                .ToListAsync();
            return View(services);
        }

        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Service created successfully!";
                return RedirectToAction(nameof(Services));
            }
            return View(service);
        }

        public async Task<IActionResult> EditService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(int id, Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Service updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ServiceExists(service.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Services));
            }
            return View(service);
        }

        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost, ActionName("DeleteService")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteServiceConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Service deleted successfully!";
            }
            return RedirectToAction(nameof(Services));
        }

        // Bookings Management
        public async Task<IActionResult> Bookings()
        {
            var bookings = await _context.Bookings
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
            return View(bookings);
        }

        public async Task<IActionResult> BookingDetails(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBookingStatus(int id, string status)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            booking.Status = status;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Booking status updated successfully!";
            return RedirectToAction(nameof(Bookings));
        }

        // Messages Management
        public async Task<IActionResult> Messages()
        {
            var messages = await _context.ContactMessages
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
            return View(messages);
        }

        public async Task<IActionResult> MessageDetails(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            if (!message.IsRead)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return View(message);
        }

        // Blog Management
        public async Task<IActionResult> BlogPosts()
        {
            var blogPosts = await _context.BlogPosts
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
            return View(blogPosts);
        }

        public IActionResult CreateBlogPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBlogPost(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                if (blogPost.IsPublished)
                {
                    blogPost.PublishedAt = DateTime.UtcNow;
                }
                _context.BlogPosts.Add(blogPost);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Blog post created successfully!";
                return RedirectToAction(nameof(BlogPosts));
            }
            return View(blogPost);
        }

        public async Task<IActionResult> EditBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBlogPost(int id, BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (blogPost.IsPublished && blogPost.PublishedAt == null)
                    {
                        blogPost.PublishedAt = DateTime.UtcNow;
                    }
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Blog post updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BlogPostExists(blogPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(BlogPosts));
            }
            return View(blogPost);
        }

        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }

        [HttpPost, ActionName("DeleteBlogPost")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBlogPostConfirmed(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost != null)
            {
                _context.BlogPosts.Remove(blogPost);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Blog post deleted successfully!";
            }
            return RedirectToAction(nameof(BlogPosts));
        }

        // Team Management
        public async Task<IActionResult> TeamMembers()
        {
            var teamMembers = await _context.TeamMembers
                .OrderBy(t => t.SortOrder)
                .ToListAsync();
            return View(teamMembers);
        }

        public IActionResult CreateTeamMember()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeamMember(TeamMember teamMember)
        {
            if (ModelState.IsValid)
            {
                _context.TeamMembers.Add(teamMember);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Team member created successfully!";
                return RedirectToAction(nameof(TeamMembers));
            }
            return View(teamMember);
        }

        public async Task<IActionResult> EditTeamMember(int id)
        {
            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }
            return View(teamMember);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeamMember(int id, TeamMember teamMember)
        {
            if (id != teamMember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamMember);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Team member updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TeamMemberExists(teamMember.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(TeamMembers));
            }
            return View(teamMember);
        }

        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }
            return View(teamMember);
        }

        [HttpPost, ActionName("DeleteTeamMember")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTeamMemberConfirmed(int id)
        {
            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember != null)
            {
                _context.TeamMembers.Remove(teamMember);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Team member deleted successfully!";
            }
            return RedirectToAction(nameof(TeamMembers));
        }

        // Helper methods
        private async Task<bool> PortfolioExists(int id)
        {
            return await _context.Portfolios.AnyAsync(e => e.Id == id);
        }

        private async Task<bool> ServiceExists(int id)
        {
            return await _context.Services.AnyAsync(e => e.Id == id);
        }

        private async Task<bool> BlogPostExists(int id)
        {
            return await _context.BlogPosts.AnyAsync(e => e.Id == id);
        }

        private async Task<bool> TeamMemberExists(int id)
        {
            return await _context.TeamMembers.AnyAsync(e => e.Id == id);
        }
    }
}
