using EventOrganizer.BLL.Interfaces;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.DAL.Models;
using EventOrganizer.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventOrganizer.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public string GetCategoryName(string categoryName)
        {
            string _categoryName = null;
            Category category = null;
            if (_categoryRepository.Categories.Any((c) => string.Equals(c.Name, categoryName, StringComparison.OrdinalIgnoreCase)) )
            {
                category = _categoryRepository.Categories.Where(
                    (c) => string.Equals(c.Name, categoryName, StringComparison.OrdinalIgnoreCase)).First();
                _categoryName = category.Name;
            }
            else
            {
                _categoryName = "All";
            }

            return _categoryName;
        }


    }
}
