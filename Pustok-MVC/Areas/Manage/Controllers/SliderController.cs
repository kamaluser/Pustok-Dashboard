using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok_MVC.Areas.Manage.ViewModels;
using Pustok_MVC.Data;
using Pustok_MVC.Models;

namespace Pustok_MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            return View(PaginatedList<Slider>.Create(_context.Sliders, page, 2));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                return View(slider);
            }

            if (_context.Sliders.Any(x => x.Title1 == slider.Title1))
            {
                ModelState.AddModelError("Title1", "Slider already exists!");
                return View(slider);
            }

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
