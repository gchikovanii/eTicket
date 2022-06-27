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
    public class ActorsController : Controller
    {
        private readonly IActorsService _actorService;

        public ActorsController(IActorsService actorService)
        {
            _actorService = actorService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _actorService.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }


        [AllowAnonymous]

        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _actorService.GetByIdAsync(id);
            if(actorDetails == null)
            {
                return View("NotFound");
            }
            return View(actorDetails);
        }
        //Add
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureUrl,FullName,Bio")]Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _actorService.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        //Edit
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _actorService.GetByIdAsync(id);
            if (actorDetails == null)
            {
                return View("NotFound");
            }
            return View(actorDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,ProfilePictureUrl,FullName,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _actorService.UpdateAsync(id,actor);
            return RedirectToAction(nameof(Index));
        }
        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _actorService.GetByIdAsync(id);
            if (actorDetails == null)
            {
                return View("NotFound");
            }
            return View(actorDetails);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _actorService.GetByIdAsync(id);
            if (actorDetails == null)
            {
                return View("NotFound");
            }
            await _actorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }




    }
}
