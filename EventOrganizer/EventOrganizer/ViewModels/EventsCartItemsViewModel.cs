using EventOrganizer.DAL.Models;
using System.Collections.Generic;

namespace EventOrganizer.ViewModels
{
    public class EventsCartItemsViewModel
    {
        public IEnumerable<EventsCartItem> EventsCartItems { get; set; }
    }
}
