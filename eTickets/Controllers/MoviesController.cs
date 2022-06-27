using eTickets.Data.DataContext;
using eTickets.Data.Services.Abstraction;
using eTickets.Data.Services.Implementation;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;
        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allMovies = await _moviesService.GetAllAsync(i => i.Cinema);
            return View(allMovies);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _moviesService.GetAllAsync(i => i.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(i => i.Name.ToLower().Contains(searchString.ToLower()) 
                || i.Description.ToLower().Contains(searchString.ToLower()))
                    .ToList();
                return View("Index", filteredResult);
            }
            
            return View("Index",allMovies);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _moviesService.GetMovieByIdAsync(id);
            if(movieDetail == null)
            {
                return View("NotFound");
            }
            return View(movieDetail);
        }

        //Create Movies
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _moviesService.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cienmas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _moviesService.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cienmas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await _moviesService.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        //Edit
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _moviesService.GetMovieByIdAsync(id);
            if(movieDetails == null)
            {
                return View("NotFound");
            }
            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                ImageUrl = movieDetails.ImageUrl,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actor_Movies.Select(i => i.ActorId).ToList()
            };
            var movieDropdownsData = await _moviesService.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cienmas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewMovieVM movie)
        {
            if (id != movie.Id)
                return View("NotFound");
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _moviesService.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cienmas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await _moviesService.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}
