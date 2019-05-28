using EventOrganizer.DAL.Models;
using System.Collections.Generic;

namespace EventOrganizer.DAL.Interfaces
{
    public interface IEventCartItemsRepository
    {
        IEnumerable<EventCartItem> EventsCartItems { get; }

        void AddItem(Event @event);

        void RemoveItem(Event @event);

        IEnumerable<EventCartItem> GetAllItems();

        bool ItemExists(Event @event);

        void RemoveAllItems();
    }
}
