using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MoviesAdmin.Models;

namespace MoviesAdmin.Controllers
{
    public class HomeController : Controller
    {
        // Constructor
        public HomeController()
        {

        }

        // Action Methods
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetAllMovies()
    
        {
            List<Movie> movies = new List<Movie>();

            // Movie 1
            Movie movie1 = new Movie();
            movie1.Id = 1;
            movie1.Title = "Spider-Man: Brand New Day";
            movie1.Synopsis = "It's a BRAND NEW DAY for Peter Parker...";
            movie1.Genre = "Action";
            movie1.Runtime = 2.25;
            movie1.Director = "Destin Daniel Cretton";

            // Movie 2
            Movie movie2 = new Movie();
            movie2.Id = 2;
            movie2.Title = "Five Nights at Freddy's";
            movie2.Synopsis = "Follows Mike Schmidt, a troubled young man...";
            movie2.Genre = "Horror";
            movie2.Runtime = 1.50;

            // Add both to the list
            movies.Add(movie1);
            movies.Add(movie2);

            return View(movies);
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}