using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core_WebApp.Models;
using Core_WebEV.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core_WebApp.Controllers
{
    public class ProductController : Controller
    {
        private IRepository<Product, int> repository;
        private IRepository<Category, int> catRepository;

        public ProductController(IRepository<Product,int> repository, IRepository<Category, int> catRepository)
        {
            this.repository = repository;
            this.catRepository = catRepository;
        }

        
        public async Task<IActionResult> Index()
        {
            var res = await repository.GetAsync();
            return View(res);
        }

        public async Task<IActionResult> Create()
        {
            var res = new Product();
            ViewBag.CategoryRowId = new SelectList(await catRepository.GetAsync(), "CategoryRowID", "CategoryName");
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var res = await repository.CreateAsync(product);
                return RedirectToAction("Index");
            }
            else
                return View(product);
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
        public async Task<IActionResult> Edit(int id, Product product)
        {
            // Validate the received category
            if (ModelState.IsValid)
            {
                var res = await repository.UpdateAsync(id, product);
                return RedirectToAction("Index"); // Redirect to Index action method
            }
            else
                return View(product);// Stay on Create View with Errors
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await repository.DeleteAsync(id);
            if (res)
            {
                return RedirectToAction("Index"); // with Success in Delete
            }
            return RedirectToAction("Index");
        }
    }
}