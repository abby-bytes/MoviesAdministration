using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;

namespace MoviesAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly MoviesAdminContext _context;

        // Putting database context for view
        public HomeController(MoviesAdminContext context)
        {
            _context = context;
        }

        // Fetch movies and send them to the Home view
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movie.ToListAsync();
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}