using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventOrganizer.DAL.Repositories
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

        public Event GetEventById(int eventId) => _eventOrganizerDbContext.Events.FirstOrDefault(p => p.Id == eventId);

        public void Create(Event item)
        {
            _eventOrganizerDbContext.Events.Add(item);
            _eventOrganizerDbContext.SaveChanges();
        }

        public bool Exists(Event item)
        {
            var events = _eventOrganizerDbContext.Events.Where((it) => (it == item)).Count();
            if (events != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Delete(Event item)
        {
            _eventOrganizerDbContext.Events.Remove(item);
            _eventOrganizerDbContext.SaveChanges();
        }
    }
}
