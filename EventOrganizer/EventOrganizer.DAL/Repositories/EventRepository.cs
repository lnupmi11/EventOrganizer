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

        public virtual IEnumerable<Event> Events => _eventOrganizerDbContext.Events.Include(c => c.Category).Include(c => c.Comments).Include(l=>l.Likes);

        public virtual IEnumerable<Event> PreferredEvents => Events.Where(p => p.IsPreferredEvent);

        public virtual Event GetEventById(int eventId) => Events.FirstOrDefault(p => p.Id == eventId);

        public virtual void Create(Event item)
        {
            _eventOrganizerDbContext.Events.Add(item);
            _eventOrganizerDbContext.SaveChanges();
        }

        public virtual bool Exists(Event item)
        {
            var events = _eventOrganizerDbContext.Events.Where((it) => (it.Id == item.Id)).Count();
            if (events != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void Edit(Event item)
        {
            var ev = _eventOrganizerDbContext.Events.Where((it) => (it.Id == item.Id)).First();
            _eventOrganizerDbContext.Events.Remove(ev);
            _eventOrganizerDbContext.Events.Add(item);
            _eventOrganizerDbContext.SaveChanges();
        }

        public virtual void Delete(Event item)
        {
            _eventOrganizerDbContext.Events.Remove(item);
            _eventOrganizerDbContext.SaveChanges();
        }

    }
}
