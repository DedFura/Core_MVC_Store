using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_MVC_Store.Data;
using Core_MVC_Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core_MVC_Store.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class SpecialTagsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SpecialTagsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.SpecialTagses.ToList());
        }

        // GET: Create Action method
        // GET: Create Action method
        public IActionResult Create()
        {
            return View();
        }

        // POST: Async Create action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags specialTags)
        {
            if (ModelState.IsValid)
            {
                _db.Add(specialTags);

                await _db.SaveChangesAsync();

                TempData["Successful"] = $"New special tag: { specialTags.Name} is added!";
                // Добавить сообщение о успешном добавлении категории

                return RedirectToAction(nameof(Index));
            }

            return View(specialTags);
        }

        // Lesson 3
        // GET: Edit action method
        public async Task<IActionResult> Edit(int? id)
        {
            // Проверить ID на NULL
            if (id == null)
                return NotFound();

            // Получаем редактируемую модель по ID асинхронно
            var specialTag = await _db.SpecialTagses.FindAsync(id);

            // Проверяем полученную модель на NULL
            if (specialTag == null)
                return NotFound();

            // Если все условия выполнены, то возвращаем модель в представление
            return View(specialTag);
        }

        // Lesson 3
        // POST: Edit action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpecialTags specialTags)
        {
            // Проверяем ID модели и переданный формой ID на соответствие
            if (id != specialTags.Id)
                return NotFound();

            // Проверяем модель на валидность
            if (ModelState.IsValid)
            {
                // Обновляем данные в модели Entity
                _db.Update(specialTags);

                // Сохраняем изменения в базу данных асинхронно
                await _db.SaveChangesAsync();

                // Добавляем сообщение об успешном обновлении
                TempData["Successful"] = $"Special tag: { specialTags.Name} update success!";

                // Переадресовываем пользователя на страницу Index
                return RedirectToAction(nameof(Index));
            }

            // Возвращаем представление с моделью, если какое-то условие не верно
            return View(specialTags);
        }

        // Lesson 3
        // GET: Edit action method
        public async Task<IActionResult> Details(int? id)
        {
            // Проверить ID на NULL
            if (id == null)
                return NotFound();

            // Получаем редактируемую модель по ID асинхронно
            var specialTag = await _db.SpecialTagses.FindAsync(id);

            // Проверяем полученную модель на NULL
            if (specialTag == null)
                return NotFound();

            // Если все условия выполнены, то возвращаем модель в представление
            return View(specialTag);
        }

        // Lesson 3
        // GET: Delete action method
        public async Task<IActionResult> Delete(int? id)
        {
            // Проверить ID на NULL
            if (id == null)
                return NotFound();

            // Получаем редактируемую модель по ID асинхронно
            var specialTag = await _db.SpecialTagses.FindAsync(id);

            // Проверяем полученную модель на NULL
            if (specialTag == null)
                return NotFound();

            // Если все условия выполнены, то возвращаем модель в представление
            return View(specialTag);
        }

        // Lesson 3
        // POST: Delete action method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Получаем редактируемую модель по ID асинхронно
            var specialTag = await _db.SpecialTagses.FindAsync(id);

            // Гдаляем модель из базы
            _db.SpecialTagses.Remove(specialTag);

            // Сохраняем изменения в базу данных асинхронно
            await _db.SaveChangesAsync();

            // Добавляем сообщение об успешном обновлении
            TempData["Successful"] = $"Special tag: { specialTag.Name} delete success!";

            // Переадресовываем пользователя на страницу Index
            return RedirectToAction(nameof(Index));
        }
    }
}
