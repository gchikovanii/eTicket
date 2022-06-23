using eTickets.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await _context.Movies.Include(i =>i.Cinema)
                .OrderBy(i => i.Name).ToListAsync();
            return View(allMovies);
        }
    }
}
