using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.BLL.Interfaces
{
    public interface IEventCartItemsService
    {
        void AddToCart(int eventId, string userId);

        void RemoveFromCart(int eventId, string userId);

        IEnumerable<EventCartItem> GetEventCartItems(string userId);

        void ClearCart(string userId);
    }
}
