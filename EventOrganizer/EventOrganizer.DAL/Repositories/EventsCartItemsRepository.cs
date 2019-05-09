using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EventOrganizer.DAL.Repositories
{
    public class EventsCartItemsRepository : IEventsCartItemsRepository
    {
        private readonly EventOrganizerDbContext _eventOrganizerDbContext;

        public EventsCartItemsRepository(EventOrganizerDbContext eventOrganizerDbContext)
        {
            _eventOrganizerDbContext = eventOrganizerDbContext;
        }

        public IEnumerable<EventsCartItem> EventsCartItems => _eventOrganizerDbContext.EventsCartItems;

        //public static EventsCart GetCart(IServiceProvider services)
        //{
        //    ISession session = services.GetRequiredService<IHttpContextAccessor>()?
        //        .HttpContext.Session;

        //    var context = services.GetService<EventOrganizerDbContext>();
        //    string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
        //    session.SetString("CartId", cartId);

        //    return new EventsCart(context) { EventsCartId = cartId };
        //}

        public void AddItem(Event @event)
        {
            var eventsCartItem = _eventOrganizerDbContext.EventsCartItems.SingleOrDefault(s => s.Event.Id == @event.Id);

            if (eventsCartItem == null)
            {
                eventsCartItem = new EventsCartItem { Event = @event };
                _eventOrganizerDbContext.EventsCartItems.Add(eventsCartItem);
            }

            _eventOrganizerDbContext.SaveChanges();
        }

        public void RemoveItem(Event @event)
        {
            var eventsCartItem = _eventOrganizerDbContext.EventsCartItems.SingleOrDefault(s => s.Event.Id == @event.Id);

            if (eventsCartItem != null)
            {
                _eventOrganizerDbContext.EventsCartItems.Remove(eventsCartItem);
            }

            _eventOrganizerDbContext.SaveChanges();
        }

        public bool ItemExists(Event @event)
        {
            return _eventOrganizerDbContext.EventsCartItems.SingleOrDefault(s => s.Event.Id == @event.Id) != null;
        }

        public IEnumerable<EventsCartItem> GetAllItems()
        {
            return _eventOrganizerDbContext.EventsCartItems.Include(s => s.Event).ToList();
        }

        public void RemoveAllItems()
        {
            var cartItems = _eventOrganizerDbContext.EventsCartItems;
            _eventOrganizerDbContext.EventsCartItems.RemoveRange(cartItems);

            _eventOrganizerDbContext.SaveChanges();
        }
    }
}
