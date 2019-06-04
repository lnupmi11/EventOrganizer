using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.ViewModels
{
    public class PageInfo
    {
        public int TotalNumberOfEvents { get; set; }
        public int CurrentPageNumber { get; set; }
        public int EventsPerPage { get; set; }
        public int TotalPages {
            get { return (int)Math.Ceiling((double)TotalNumberOfEvents / EventsPerPage); }
        }
    }

    public class EventsListViewModel
    {
        public PageInfo @PageInfo { get; set; }
        public IEnumerable<Event> Events { get; set; }

        public string CurrentCategory { get; set; }

    }
}
