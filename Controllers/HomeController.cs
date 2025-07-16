using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SVMStudio.Data;
using SVMStudio.Models;
using SVMStudio.ViewModels;

namespace SVMStudio.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeViewModel
        {
            FeaturedPortfolios = await _context.Portfolios
                .Where(p => p.IsActive)
                .OrderBy(p => p.SortOrder)
                .Take(8)
                .ToListAsync(),
            Services = await _context.Services
                .Where(s => s.IsActive)
                .OrderBy(s => s.SortOrder)
                .Take(3)
                .ToListAsync(),
            RecentBlogPosts = await _context.BlogPosts
                .Where(b => b.IsPublished)
                .OrderByDescending(b => b.PublishedAt)
                .Take(3)
                .ToListAsync()
        };

        return View(viewModel);
    }

    public async Task<IActionResult> About()
    {
        var viewModel = new AboutViewModel
        {
            TeamMembers = await _context.TeamMembers
                .Where(t => t.IsActive)
                .OrderBy(t => t.SortOrder)
                .ToListAsync(),
            StudioPhilosophy = "At SVM Studio, we believe that every moment deserves to be captured with artistry and elegance. Our approach combines technical excellence with creative vision, creating timeless images that tell your unique story. We specialize in high-end photography and styling, bringing together a team of passionate professionals who understand that photography is more than just capturing images—it's about creating art that resonates with emotion and beauty."
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Gallery(string? category)
    {
        var query = _context.Portfolios.Where(p => p.IsActive);
        
        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(p => p.Category == category);
        }

        var viewModel = new GalleryViewModel
        {
            Portfolios = await query
                .OrderBy(p => p.SortOrder)
                .ThenByDescending(p => p.CreatedAt)
                .ToListAsync(),
            Categories = await _context.Portfolios
                .Where(p => p.IsActive)
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync(),
            SelectedCategory = category
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Services()
    {
        var viewModel = new ServicesViewModel
        {
            Services = await _context.Services
                .Where(s => s.IsActive)
                .OrderBy(s => s.SortOrder)
                .ToListAsync()
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Contact()
    {
        return View(new ContactViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactViewModel model)
    {
        if (ModelState.IsValid)
        {
            var contactMessage = new ContactMessage
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.ServiceType,
                Message = model.Message,
                CreatedAt = DateTime.UtcNow
            };

            _context.ContactMessages.Add(contactMessage);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thank you for your message! We'll get back to you soon.";
            return RedirectToAction(nameof(Contact));
        }

        return View(model);
    }

    public async Task<IActionResult> Booking()
    {
        return View(new BookingViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Booking(BookingViewModel model)
    {
        if (ModelState.IsValid)
        {
            var booking = new Booking
            {
                ClientName = model.Name,
                ClientEmail = model.Email,
                ClientPhone = model.Phone,
                ServiceType = model.ServiceType,
                PreferredDate = model.PreferredDate,
                Notes = model.VisionDescription,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Your booking request has been submitted! We'll contact you soon to confirm the details.";
            return RedirectToAction(nameof(Booking));
        }

        return View(model);
    }

    public async Task<IActionResult> Blog(int page = 1)
    {
        const int pageSize = 6;
        var totalPosts = await _context.BlogPosts.CountAsync(b => b.IsPublished);
        var totalPages = (int)Math.Ceiling(totalPosts / (double)pageSize);

        var viewModel = new BlogViewModel
        {
            BlogPosts = await _context.BlogPosts
                .Where(b => b.IsPublished)
                .OrderByDescending(b => b.PublishedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(),
            CurrentPage = page,
            TotalPages = totalPages,
            PageSize = pageSize
        };

        return View(viewModel);
    }

    public async Task<IActionResult> BlogPost(int id)
    {
        var blogPost = await _context.BlogPosts
            .FirstOrDefaultAsync(b => b.Id == id && b.IsPublished);

        if (blogPost == null)
        {
            return NotFound();
        }

        var viewModel = new BlogPostViewModel
        {
            BlogPost = blogPost,
            RelatedPosts = await _context.BlogPosts
                .Where(b => b.IsPublished && b.Id != id)
                .OrderByDescending(b => b.PublishedAt)
                .Take(3)
                .ToListAsync()
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // Test action to check authentication state
    public IActionResult AuthTest()
    {
        ViewData["IsAuthenticated"] = User.Identity?.IsAuthenticated ?? false;
        ViewData["UserName"] = User.Identity?.Name ?? "Not logged in";
        ViewData["Roles"] = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
