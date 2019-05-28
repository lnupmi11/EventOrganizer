using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EventOrganizer.DAL.Repositories
{
    public class EventCartItemsRepository : IEventCartItemsRepository
    {
        private readonly EventOrganizerDbContext _eventOrganizerDbContext;

        public EventCartItemsRepository(EventOrganizerDbContext eventOrganizerDbContext)
        {
            _eventOrganizerDbContext = eventOrganizerDbContext;
        }

        public IEnumerable<EventCartItem> EventsCartItems => _eventOrganizerDbContext.EventCartItems;

        public void AddItem(Event @event)
        {
            var eventsCartItem = _eventOrganizerDbContext.EventCartItems.SingleOrDefault(s => s.Event.Id == @event.Id);

            if (eventsCartItem == null)
            {
                eventsCartItem = new EventCartItem { Event = @event };
                _eventOrganizerDbContext.EventCartItems.Add(eventsCartItem);
            }

            _eventOrganizerDbContext.SaveChanges();
        }

        public void RemoveItem(Event @event)
        {
            var eventsCartItem = _eventOrganizerDbContext.EventCartItems.SingleOrDefault(s => s.Event.Id == @event.Id);

            if (eventsCartItem != null)
            {
                _eventOrganizerDbContext.EventCartItems.Remove(eventsCartItem);
            }

            _eventOrganizerDbContext.SaveChanges();
        }

        public bool ItemExists(Event @event)
        {
            return _eventOrganizerDbContext.EventCartItems.SingleOrDefault(s => s.Event.Id == @event.Id) != null;
        }

        public IEnumerable<EventCartItem> GetAllItems()
        {
            return _eventOrganizerDbContext.EventCartItems.Include(s => s.Event).ToList();
        }

        public void RemoveAllItems()
        {
            var cartItems = _eventOrganizerDbContext.EventCartItems;
            _eventOrganizerDbContext.EventCartItems.RemoveRange(cartItems);

            _eventOrganizerDbContext.SaveChanges();
        }
    }
}
