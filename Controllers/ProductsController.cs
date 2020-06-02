using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_MVC_Store.Data;
using Core_MVC_Store.Models;
using Core_MVC_Store.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core_MVC_Store.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;

            // Инициализируем привязанное свойство в конструкторе значениями ID сопутствующих таблиц
            ProductsVm = new ProductsVM()
            {
                ProductTypes = _db.ProductTypeses.ToList(),
                SpecialTags = _db.SpecialTagses.ToList(),
                Products = new Products()
            };
        }

        [BindProperty] // Доступ к VM во всём контроллере
        public ProductsVM ProductsVm { get; set; }

        // GET: method Index
        public async Task<IActionResult> Index()
        {
            // Заполнить модель данными
            var products = _db.Productses
                .Include(x => x.ProductTypes)
                .Include(x => x.SpecialTags);

            return View(await products.ToListAsync());
        }
    }
}