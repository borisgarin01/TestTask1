using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask1.Models;
using TestTask1.Repositories.Interfaces.Derived;

namespace TestTask1.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsRepository _carsRepository;
        private readonly ICategoriesRepository _categoriesRepository;


        public CarsController(ICarsRepository carsRepository, ICategoriesRepository categoriesRepository)
        {
            _carsRepository = carsRepository;
            _categoriesRepository = categoriesRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _carsRepository.GetAllWithCategoriesAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoriesRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                await _carsRepository.AddAsync(car);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Get(long id)
        {
            return View(await _carsRepository.GetWithCategoryAsync(id));
        }

        public async Task<IActionResult> Update(long id)
        {
            return View(await _carsRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(Car car)
        {
            if (ModelState.IsValid)
            {
                await _carsRepository.UpdateAsync(car);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            return View(await _carsRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Car car)
        {
            await _carsRepository.RemoveAsync(car.Id);
            return RedirectToAction("Index");
        }
    }
}
