using eTickets.Data.DataContext;
using eTickets.Data.Services.Abstraction;
using eTickets.Data.Services.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoivesService _moviesService;
        public MoviesController(IMoivesService moviesService)
        {
            _moviesService = moviesService;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await _moviesService.GetAllAsync();
            return View(allMovies);
        }
    }
}
