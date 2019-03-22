using EventOrganizer.Data.Interfaces;
using EventOrganizer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Data.Mocks
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> Categories
        {
            get
            {
                return new List<Category>
                {
                    new Category { Name = "Bussiness", Description = "Bussiness conferences, meetings and..." },

                    new Category { Name = "Education", Description = "Educational seminars, lectures and..." },

                    new Category { Name = "Marketing", Description = "Marketing trade shows, product launches and..." },

                    new Category { Name = "Entertainment", Description = "Theme parties, weddings, birthdays and..." },

                    new Category { Name = "Culture", Description = "Opening ceremonies, award ceremonies and..." }
                };
            }
        }
    }
}
