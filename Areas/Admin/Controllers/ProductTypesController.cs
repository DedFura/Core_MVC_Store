using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_MVC_Store.Data;
using Core_MVC_Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core_MVC_Store.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductTypesController (ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ProductTypeses.ToList());
        }

        // GET: Create Action method
        public IActionResult Create()
        {
            return View();
        }

        // POST: Async Create action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Add(productTypes);

                await _db.SaveChangesAsync();

                // Создаём экземпляр ViewData["Seccessful"]
                // Добавить сообщение о успешном добавлении категории

                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }
    }
}