using EventOrganizer.DAL.Models;
using System.Collections.Generic;

namespace EventOrganizer.DAL.Interfaces
{
    public interface IEventsCartItemsRepository
    {
        IEnumerable<EventsCartItem> EventsCartItems { get; }

        void AddItem(Event @event);

        void RemoveItem(Event @event);

        IEnumerable<EventsCartItem> GetAllItems();

        bool ItemExists(Event @event);

        void RemoveAllItems();
    }
}
