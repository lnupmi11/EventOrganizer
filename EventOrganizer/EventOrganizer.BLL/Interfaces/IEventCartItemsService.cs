using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.BLL.Interfaces
{
    public interface IEventCartItemsService
    {
        void AddToCart(Event @event);

        void RemoveFromCart(Event @event);

        IEnumerable<EventCartItem> GetEventsCartItems();

        void ClearCart();
    }
}
