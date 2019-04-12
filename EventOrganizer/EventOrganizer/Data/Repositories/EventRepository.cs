using EventOrganizer.Data.Interfaces;
using EventOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _appDbContext;

        public EventRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Event> Events => _appDbContext.Events.Include(c => c.Category);

        public IEnumerable<Event> PreferredEvents => _appDbContext.Events.Where(p => p.IsPreferredEvent).Include(c => c.Category);

        public Event GetEventById(int eventId) => _appDbContext.Events.FirstOrDefault(p => p.EventId == eventId);
    }
}
