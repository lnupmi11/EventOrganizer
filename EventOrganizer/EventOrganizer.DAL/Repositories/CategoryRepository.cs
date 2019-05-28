using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EventOrganizerDbContext _eventOrganizerDbContext;
        public CategoryRepository(EventOrganizerDbContext eventOrganizerDbContext)
        {
            _eventOrganizerDbContext = eventOrganizerDbContext;
        }
        public virtual IEnumerable<Category> Categories => _eventOrganizerDbContext.Categories;
    }
}
