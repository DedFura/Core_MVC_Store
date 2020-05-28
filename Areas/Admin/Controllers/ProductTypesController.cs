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

                TempData["Successful"] = $"New product type: {productTypes.Name} is added!";
                // Добавить сообщение о успешном добавлении категории

                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }

        // Lesson 3
        // GET: Edit action method
        public async Task<IActionResult> Edit(int? id)
        {
            // Проверить ID на NULL
            if (id == null)
                return NotFound();

            // Получаем редактируемую модель по ID асинхронно
            var productType = await _db.ProductTypeses.FindAsync(id);

            // Проверяем полученную модель на NULL
            if (productType == null)
                return NotFound();

            // Если все условия выполнены, то возвращаем модель в представление
            return View(productType);
        }

        // Lesson 3
        // POST: Edit action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductTypes productTypes)
        {
            // Проверяем ID модели и переданный формой ID на соответствие
            if (id != productTypes.Id)
                return NotFound();

            // Проверяем модель на валидность
            if (ModelState.IsValid)
            {
                // Обновляем данные в модели Entity
                _db.Update(productTypes);

                // Сохраняем изменения в базу данных асинхронно
                await _db.SaveChangesAsync();

                // Добавляем сообщение об успешном обновлении
                TempData["Successful"] = $"Product type: {productTypes.Name} update success!";

                // Переадресовываем пользователя на страницу Index
                return RedirectToAction(nameof(Index));
            }

            // Возвращаем представление с моделью, если какое-то условие не верно
            return View(productTypes);
        }

        // Lesson 3
        // GET: Edit action method
        public async Task<IActionResult> Details(int? id)
        {
            // Проверить ID на NULL
            if (id == null)
                return NotFound();

            // Получаем редактируемую модель по ID асинхронно
            var productType = await _db.ProductTypeses.FindAsync(id);

            // Проверяем полученную модель на NULL
            if (productType == null)
                return NotFound();

            // Если все условия выполнены, то возвращаем модель в представление
            return View(productType);
        }

        // Lesson 3
        // GET: Delete action method
        public async Task<IActionResult> Delete(int? id)
        {
            // Проверить ID на NULL
            if (id == null)
                return NotFound();

            // Получаем редактируемую модель по ID асинхронно
            var productType = await _db.ProductTypeses.FindAsync(id);

            // Проверяем полученную модель на NULL
            if (productType == null)
                return NotFound();

            // Если все условия выполнены, то возвращаем модель в представление
            return View(productType);
        }

        // Lesson 3
        // POST: Delete action method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Получаем редактируемую модель по ID асинхронно
            var productType = await _db.ProductTypeses.FindAsync(id);

            // Гдаляем модель из базы
            _db.ProductTypeses.Remove(productType);

            // Сохраняем изменения в базу данных асинхронно
            await _db.SaveChangesAsync();

            // Добавляем сообщение об успешном обновлении
            TempData["Successful"] = $"Product type: {productType.Name} delete success!";

            // Переадресовываем пользователя на страницу Index
            return RedirectToAction(nameof(Index));
        }
    }
}