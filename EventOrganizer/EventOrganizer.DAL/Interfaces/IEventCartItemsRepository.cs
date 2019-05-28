using EventOrganizer.DAL.Models;
using System.Collections.Generic;

namespace EventOrganizer.DAL.Interfaces
{
    public interface IEventCartItemsRepository
    {
        IEnumerable<EventCartItem> EventCartItems { get; }

        void AddItem(int eventId, string userId);

        void RemoveItem(int eventId, string userId);

        bool ItemExists(int eventId, string userId);

        IEnumerable<EventCartItem> GetAllItems(string userId);

        void RemoveAllItems(string userId);
    }
}
