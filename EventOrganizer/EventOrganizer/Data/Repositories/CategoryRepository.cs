using EventOrganizer.Data.Interfaces;
using EventOrganizer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EventOrganizerDbContext _eventOrganizerDbContext;
        public CategoryRepository(EventOrganizerDbContext eventOrganizerDbContext)
        {
            _eventOrganizerDbContext = eventOrganizerDbContext;
        }
        public IEnumerable<Category> Categories => _eventOrganizerDbContext.Categories;
    }
}
