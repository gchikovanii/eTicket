using eTickets.Data.DataContext;
using eTickets.Data.Services.Abstraction;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemasService _cinemasService;
        public CinemasController(ICinemasService cinemasService)
        {
            _cinemasService = cinemasService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _cinemasService.GetAllAsync();
            return View(allCinemas);
        }

        //Create Cinema
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
                return View(cinema);
            await _cinemasService.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _cinemasService.GetByIdAsync(id);
            if (cinemaDetails == null)
                return View("NotFound");
            return View(cinemaDetails);
        }
        //Add Cinema

        [HttpPost]
        public async Task<IActionResult> Details([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
                return View(cinema);
            await _cinemasService.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        //Update Cinema
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _cinemasService.GetByIdAsync(id);
            if (cinemaDetails == null)
                return View("NotFound");
            return View(cinemaDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
                return View(cinema);
            await _cinemasService.UpdateAsync(id,cinema);
            return RedirectToAction(nameof(Index));
        }

        //Delete Cinema
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _cinemasService.GetByIdAsync(id);
            if (cinemaDetails == null)
                return View("NotFound");
            return View(cinemaDetails);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var cinemaDetails = await _cinemasService.GetByIdAsync(id);
            if (cinemaDetails == null)
                return View("NotFound");
            await _cinemasService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
