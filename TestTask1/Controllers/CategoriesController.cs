using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask1.Models;
using TestTask1.Repositories.Interfaces.Derived;

namespace TestTask1.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoriesRepository.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoriesRepository.AddAsync(category);
                return RedirectToAction("Index");
            }
            else
            {
                return Create();
            }
        }

        public async Task<IActionResult> Get(long id)
        {
            return View(await _categoriesRepository.GetAsync(id));
        }

        public async Task<IActionResult> Update(long id)
        {
            return View(await _categoriesRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoriesRepository.UpdateAsync(category);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            return View(await _categoriesRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {
            await _categoriesRepository.RemoveAsync(category.Id);
            return RedirectToAction("Index");
        }
    }
}
