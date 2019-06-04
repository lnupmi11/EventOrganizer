using System.Collections.Generic;
using EventOrganizer.BLL.Interfaces;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.DAL.Models;

namespace EventOrganizer.BLL.Services
{
    public class EventCartItemsService : IEventCartItemsService
    {
        private readonly IEventCartItemsRepository _eventCartItemsRepository;

        public EventCartItemsService(IEventCartItemsRepository eventCartItemsRepository)
        {
            _eventCartItemsRepository = eventCartItemsRepository;
        }

        public void AddToCart(int eventId, string userId)
        {
            bool exists = _eventCartItemsRepository.ItemExists(eventId, userId);
            if (!exists)
            {
                _eventCartItemsRepository.AddItem(eventId, userId);
            }
        }

        public void RemoveFromCart(int eventId, string userId)
        {
            bool exists = _eventCartItemsRepository.ItemExists(eventId, userId);
            if (exists)
            {
                _eventCartItemsRepository.RemoveItem(eventId, userId);
            }
        }

        public IEnumerable<EventCartItem> GetEventCartItems(string userId)
        {
            return _eventCartItemsRepository.GetAllItems(userId);
        }

        public void ClearCart(string userId)
        {
            _eventCartItemsRepository.RemoveAllItems(userId);
        }
    }
}
