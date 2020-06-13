using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core_MVC_Store.Data;
using Core_MVC_Store.Models;
using Core_MVC_Store.Models.ViewModels;
using Core_MVC_Store.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core_MVC_Store.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        // Новая зависимость для нахождения пути к корневому статическому каталогу
        private readonly IHostingEnvironment _hostingEnvironment;


        public ProductsController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;

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
        //GET: method Create Product
        public IActionResult Create()
        {
            return View(ProductsVm);
        }
        //POST: method Create Product
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            // Проверить модель на валидность
            if (!ModelState.IsValid) return View(ProductsVm);

            // Если модель валидна, добавляем данные в модель Entity
            _db.Productses.Add(ProductsVm.Products);

            // Сохраняем изменения асинхронно в базе данных
            await _db.SaveChangesAsync();

            // ---- Логика сохранения картинок ----

            // 1. Получить путь до корневой статической папки (wwwroot)
            string webRootPath = _hostingEnvironment.WebRootPath;

            // 2. Создаём переменную для хранения картинки полученной из формы
            var files = HttpContext.Request.Form.Files;

            // 7. Получаем все данные товара из базы
            var productsFromDb = _db.Productses.Find(ProductsVm.Products.Id);

            // 3. Проверяем, передана ли вообще картинка
            if (files.Count != 0)
            {

                // 4. Создаём путь сохранения картинки
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);

                // 5. Получаем расширение переданного файла
                var extension = Path.GetExtension(files[0].FileName);

                // 6. Сохраняем изображение
                using (var fileStream = new FileStream(Path.Combine(uploads, ProductsVm.Products.Id + extension), FileMode.Create))
                {
                    // Копируем изображение на сервер
                    files[0].CopyTo(fileStream);
                }

                // Обновляем модель данных и добавляем в неё созданный путь
                productsFromDb.Image = @"\" + SD.ImageFolder + @"\" + ProductsVm.Products.Id + extension;
            }
            else
            {
                // Формируем путь к изображению по умолчанию
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultProductImage);

                // Копируем картинку по умолчанию в директорию конкретного продукта
                System.IO.File.Copy(uploads,
                    webRootPath + @"\" + SD.ImageFolder + @"\" + ProductsVm.Products.Id + ".jpg");

                // Обновляем путь в локальной модели
                productsFromDb.Image = @"\" + SD.ImageFolder + @"\" + ProductsVm.Products.Id + ".jpg";
            }
            // Сохраняем изменения в базу асинхронно

            await _db.SaveChangesAsync();
            // ----                           ----

            // Создаём сообщение об успешном добавлении товара
            TempData["Succesfule"] = $"Товар {productsFromDb.Name} успешно добавлен";

            // Переадресовываем пользователя на страницу Products -> Index
            return RedirectToAction(nameof(Index));
        }
    }
}