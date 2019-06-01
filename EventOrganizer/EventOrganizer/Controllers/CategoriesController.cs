using EventOrganizer.BLL.Interfaces;
using EventOrganizer.BLL.Services;
using EventOrganizer.DAL.Models;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizer.Controllers
{
    public class CategoriesController : Controller
    {
        ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            Category item = new Category
            {
                Name = model.Name,
                Description = model.Description
            };
            
            _categoryService.CreateItem(item);
            return RedirectToAction("List", "Events");
        }
    }
}
