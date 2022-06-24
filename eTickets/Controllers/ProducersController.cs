using eTickets.Data.DataContext;
using eTickets.Data.Services.Abstraction;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _producersService;
        public ProducersController(IProducersService producersService)
        {
            _producersService = producersService;
        }

        public async Task<IActionResult> Index()
        {
            var allProducers = await _producersService.GetAllAsync();
            return View(allProducers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _producersService.GetByIdAsync(id);
            if (producerDetails == null)
                return View("NotFound");
            return View(producerDetails);
        }

        //Create producers
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureUrl,FullName,Bio")]Producer producer)
        {
            if (!ModelState.IsValid)
                return View(producer);
            await _producersService.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        //Edit producers
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _producersService.GetByIdAsync(id);
            if (producerDetails == null)
                return View("NotFound");
            return View(producerDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,ProfilePictureUrl,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
                return View(producer);
            if(id == producer.Id)
            {
                await _producersService.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }
        //Delete producer
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _producersService.GetByIdAsync(id);
            if (producerDetails == null)
                return View("NotFound");
            return View(producerDetails);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _producersService.GetByIdAsync(id);
            if (producerDetails == null)
                return View("NotFound");
            await _producersService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
