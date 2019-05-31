using EventOrganizer.BLL.Interfaces;
using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.DAL.Models;
using EventOrganizer.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventOrganizer.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly EventRepository _eventRepository;

        public EventService(EventOrganizerDbContext context)
        {
            _eventRepository = new EventRepository(context);
        }

        public IEnumerable<Event> GetAll()
        {
            IEnumerable<Event> events = null;
            events = _eventRepository.Events.OrderBy(p => p.Id);
            return events;
        }

        public IEnumerable<Event> GetEvents(string category)
        {
            IEnumerable<Event> events = null;
            events = _eventRepository.Events.Where(p => p.Category.Name.Equals(category)).OrderBy(p => p.Name);
            return events;
        }

        public IEnumerable<Event> GetEventsByUserId(string userId)
        {
            IEnumerable<Event> events = null;
            events = _eventRepository.Events.Where(e => e.UserId == userId).OrderBy(p => p.CreatedAt);
            return events;
        }

        public IEnumerable<Event> GetPreferredEvents()
        {
            return _eventRepository.PreferredEvents;
        }

        public Event GetEventById(int eventId)
        {
            return _eventRepository.GetEventById(eventId);
        }

        public void CreateItem(Event item)
        {
            if (item != null)
            {
                _eventRepository.Create(item);
            }
        }

        public void EditItem(Event item)
        {
            if(item != null)
            {
                if (_eventRepository.Exists(item))
                {
                    _eventRepository.Edit(item);
                }
            }
        }

        public void DeleteItem(Event item)
        {
            if (item != null)
            {
                if (_eventRepository.Exists(item))
                {
                    _eventRepository.Delete(item);
                }
            }
        }
    }
}
