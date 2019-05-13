using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.BLL.Interfaces
{
    public interface ICategoryService
    {
        string GetCategoryName(string categoryName);

        IEnumerable<Category> GetAll();
    }
}
