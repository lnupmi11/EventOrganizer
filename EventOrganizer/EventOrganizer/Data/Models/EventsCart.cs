using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Data.Models
{
    public class EventsCart
    {
        private readonly EventOrganizerDbContext _eventOrganizerDbContext;
        private EventsCart(EventOrganizerDbContext eventOrganizerDbContext)
        {
            _eventOrganizerDbContext = eventOrganizerDbContext;
        }

        public string EventsCartId { get; set; }
        public List<EventsCartItem> EventsCartItems { get; set; }

        public static EventsCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<EventOrganizerDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new EventsCart(context) { EventsCartId = cartId };
        }

        public void AddToCart(Event @event, int amount)
        {
            var eventsCartItem =
                _eventOrganizerDbContext.EventsCartItems.SingleOrDefault(
                    s => s.Event.EventId == @event.EventId && s.EventsCartId == EventsCartId);

            if (eventsCartItem == null)
            {
                eventsCartItem = new EventsCartItem
                {
                    EventsCartId = EventsCartId,
                    Event = @event
                };
                _eventOrganizerDbContext.EventsCartItems.Add(eventsCartItem);
            }

            _eventOrganizerDbContext.SaveChanges();
        }

        public void RemoveFromCart(Event @event)
        {
            var eventsCartItem =
                _eventOrganizerDbContext.EventsCartItems.SingleOrDefault(
                    s => s.Event.EventId == @event.EventId && s.EventsCartId == EventsCartId);

            if (eventsCartItem != null)
            {
                _eventOrganizerDbContext.EventsCartItems.Remove(eventsCartItem);
            }

            _eventOrganizerDbContext.SaveChanges();
        }

        public List<EventsCartItem> GetEventsCartItems()
        {
            return EventsCartItems ??
                (EventsCartItems =
                    _eventOrganizerDbContext.EventsCartItems.Where(c => c.EventsCartId == EventsCartId)
                        .Include(s => s.Event)
                        .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _eventOrganizerDbContext
                .EventsCartItems
                .Where(cart => cart.EventsCartId == EventsCartId);

            _eventOrganizerDbContext.EventsCartItems.RemoveRange(cartItems);
            _eventOrganizerDbContext.SaveChanges();
        }
    }
}
