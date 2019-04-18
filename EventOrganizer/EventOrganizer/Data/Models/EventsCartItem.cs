using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Data.Models
{
    public class EventsCartItem
    {
        public int EventsCartItemId { get; set; }
        public Event Event { get; set; }
        public string EventsCartId { get; set; }
    }
}
