using System.Collections.Generic;
using EventOrganizer.BLL.Interfaces;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.DAL.Models;

namespace EventOrganizer.BLL.Services
{
    public class EventsCartItemsService : IEventsCartItemsService
    {
        private readonly IEventsCartItemsRepository _eventsCartItemsRepository;

        public EventsCartItemsService(IEventsCartItemsRepository eventsCartItemsRepository)
        {
            _eventsCartItemsRepository = eventsCartItemsRepository;
        }

        public void AddToCart(Event @event)
        {
            bool exists = _eventsCartItemsRepository.ItemExists(@event);
            if (!exists)
            {
                _eventsCartItemsRepository.AddItem(@event);
            }
        }

        public void RemoveFromCart(Event @event)
        {
            bool exists = _eventsCartItemsRepository.ItemExists(@event);
            if (exists)
            {
                _eventsCartItemsRepository.RemoveItem(@event);
            }
        }

        public IEnumerable<EventsCartItem> GetEventsCartItems()
        {
            return _eventsCartItemsRepository.GetAllItems();
        }

        public void ClearCart()
        {
            _eventsCartItemsRepository.RemoveAllItems();
        }
    }
}
