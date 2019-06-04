using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Event> PreferredEvents { get; set; }
    }
}
