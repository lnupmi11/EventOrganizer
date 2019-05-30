using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.DAL.Models;
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

        public IEnumerable<EventCartItem> EventCartItems => _eventOrganizerDbContext.EventCartItems;

        public void AddItem(int eventId, string userId)
        {
            var eventCartItem = _eventOrganizerDbContext.EventCartItems.SingleOrDefault
                (s => s.EventId == eventId && s.UserId ==  userId);

            if (eventCartItem == null)
            {
                eventCartItem = new EventCartItem { EventId = eventId, UserId = userId };
                _eventOrganizerDbContext.EventCartItems.Add(eventCartItem);
            }

            _eventOrganizerDbContext.SaveChanges();
        }

        public void RemoveItem(int eventId, string userId)
        {
            var eventCartItem = _eventOrganizerDbContext.EventCartItems.SingleOrDefault
                (s => s.EventId == eventId && s.UserId == userId);

            if (eventCartItem != null)
            {
                _eventOrganizerDbContext.EventCartItems.Remove(eventCartItem);
            }

            _eventOrganizerDbContext.SaveChanges();
        }

        public bool ItemExists(int eventId, string userId)
        {
            var eventCartItem = _eventOrganizerDbContext.EventCartItems.SingleOrDefault
                (s => s.EventId == eventId && s.UserId == userId);
            return eventCartItem != null;
        }

        public IEnumerable<EventCartItem> GetAllItems(string userId)
        {
            var cartItems = _eventOrganizerDbContext.EventCartItems.Where(s => s.UserId == userId).ToList();
            return cartItems;
        }

        public void RemoveAllItems(string userId)
        {
            var cartItems = _eventOrganizerDbContext.EventCartItems.Where(s => s.UserId == userId).ToList();
            _eventOrganizerDbContext.EventCartItems.RemoveRange(cartItems);

            _eventOrganizerDbContext.SaveChanges();
        }
    }
}
