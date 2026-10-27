using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;
using MoviesAdmin.Services;

public class MovieController : Controller
{
    private readonly MoviesAdminContext _context;
    private readonly IAzureStorageService _azureStorageService;

    public MovieController(MoviesAdminContext context, IAzureStorageService azureStorageService)
    {
        _context = context;
        _azureStorageService = azureStorageService;
    }

    // GET: MOVIES
    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.Director)
            .ToListAsync();
        return View(movies);
    }

    // GET: Movies/Details/5
    public async Task<IActionResult> Details(int? id, bool fromHome = false)
    {
        if (id == null) return NotFound();

        var movie = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.Director)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null) return NotFound();

        ViewData["FromHome"] = fromHome;

        return View(movie);
    }

    // GET: MOVIES/Create
    public IActionResult Create()
    {
        ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "FullName");
        ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "GenreName");
        return View();
    }

    // POST: MOVIES/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Synopsis,GenreId,Rating,Runtime,DirectorId,ImageUrl,ReleaseDate")] Movie movie, IFormFile? imageFile)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            movie.ImageUrl = await _azureStorageService.UploadFileAsync(imageFile, uniqueFileName);
        }

        if (ModelState.IsValid)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "FullName", movie.DirectorId);
        ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "GenreName", movie.GenreId);
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "FullName", movie.DirectorId);
        ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "GenreName", movie.GenreId);
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Synopsis,GenreId,Rating,Runtime,DirectorId,ImageUrl,ReleaseDate")] Movie movie, IFormFile? imageFile)
    {
        ModelState.Remove("ImageUrl");

        if (id != movie.Id) return NotFound();

        var movieToUpdate = await _context.Movie.FindAsync(id);
        if (movieToUpdate == null) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                movieToUpdate.Title = movie.Title;
                movieToUpdate.Synopsis = movie.Synopsis;
                movieToUpdate.GenreId = movie.GenreId;
                movieToUpdate.Rating = movie.Rating;
                movieToUpdate.Runtime = movie.Runtime;
                movieToUpdate.DirectorId = movie.DirectorId;
                movieToUpdate.ReleaseDate = movie.ReleaseDate;

                if (imageFile != null && imageFile.Length > 0)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    movieToUpdate.ImageUrl = await _azureStorageService.UploadFileAsync(imageFile, uniqueFileName);
                }

                _context.Update(movieToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["DirectorId"] = new SelectList(_context.Director, "Id", "FullName", movie.DirectorId);
        ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "GenreName", movie.GenreId);
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.Director)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}