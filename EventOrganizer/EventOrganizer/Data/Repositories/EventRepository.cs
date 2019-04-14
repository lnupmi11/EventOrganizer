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
        private readonly EventOrganizerDbContext _eventOrganizerDbContext;

        public EventRepository(EventOrganizerDbContext eventOrganizerDbContext)
        {
            _eventOrganizerDbContext = eventOrganizerDbContext;
        }

        public IEnumerable<Event> Events => _eventOrganizerDbContext.Events.Include(c => c.Category);

        public IEnumerable<Event> PreferredEvents => _eventOrganizerDbContext.Events.Where(p => p.IsPreferredEvent).Include(c => c.Category);

        public Event GetEventById(int eventId) => _eventOrganizerDbContext.Events.FirstOrDefault(p => p.EventId == eventId);
    }
}
