using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core_WebApp.Models;
using Core_WebEV.Services;

namespace Core_WebApp.Controllers
{
    /// <summary>
    /// Controllers is a base class for ASP.NET Core MVC Coltroller for following
    /// 
    /// </summary>
    public class CategoryController : Controller
    {
        private IRepository<Category, int> repository;

        public CategoryController(IRepository<Category, int> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Returm the List of Model Data, in our case it will be Category
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var res = await repository.GetAsync();
            return View(res); // thsi will return Index View as the name of the method os "Index"
        }

        /// <summary>
        /// This will accept teh Category Object to create a new Category
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            var res = new Category();
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            // Validate the received category
            if (ModelState.IsValid)
            {
                var res = await repository.CreateAsync(category);
                return RedirectToAction("Index"); // Redirect to Index action method
            }
            else
                return View(category);// Stay on Create View with Errors
        }

        /// <summary>
        /// This will accept teh Category Object to create a new Category
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var res = await repository.GetAsync(id);
            return View(res); // Return Edit view with data to be Edited
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            // Validate the received category
            if (ModelState.IsValid)
            {
                var res = await repository.UpdateAsync(id, category);
                return RedirectToAction("Index"); // Redirect to Index action method
            }
            else
                return View(category);// Stay on Create View with Errors
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await repository.DeleteAsync(id);
            if(res)
            {
                return RedirectToAction("Index"); // with Success in Delete
            }
            return RedirectToAction("Index");
        }
    }
}