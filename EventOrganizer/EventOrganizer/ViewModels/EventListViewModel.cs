using EventOrganizer.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.ViewModels
{
    public class EventListViewModel
    {
        public IEnumerable<Event> Events { get; set; }
    }
}
