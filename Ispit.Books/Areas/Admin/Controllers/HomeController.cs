using Ispit.Books.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ispit.Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _user;
        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> user)
        {
            _context = context;
            _user = user;
        }

        // GET: Admin
        public IActionResult Index()
        {
            var result = _context.Books.Where(p => p.UserId == _user.GetUserId(User))
                .Include(a => a.Author)
                .Include(p => p.Publisher)
                .Include(u => u.User)
                .ToList();

            return View(result);
        }
    }
}
