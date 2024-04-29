using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Pustok_MVC.Areas.Manage.ViewModels;
using Pustok_MVC.Data;
using Pustok_MVC.Models;

namespace Pustok_MVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            return View(PaginatedList<Author>.Create(_context.Authors.Include(x=>x.Books), page, 2));
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                return View(author);
            }

            if (_context.Authors.Any(x => x.Fullname == author.Fullname))
            {
                ModelState.AddModelError("Fullname", "Author already exists!");
                return View(author);
            }

            _context.Authors.Add(author);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }

    
}
