using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.ViewModels
{
    public class EventsListViewModel
    {
        public IEnumerable<Event> Events { get; set; }

        public string CurrentCategory { get; set; }
    }
}
