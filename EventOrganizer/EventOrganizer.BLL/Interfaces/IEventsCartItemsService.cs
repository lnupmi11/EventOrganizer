using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.BLL.Interfaces
{
    public interface IEventsCartItemsService
    {
        void AddToCart(Event @event);

        void RemoveFromCart(Event @event);

        IEnumerable<EventsCartItem> GetEventsCartItems();

        void ClearCart();
    }
}
