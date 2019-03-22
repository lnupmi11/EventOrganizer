using EventOrganizer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Data.Interfaces
{
    public interface IEventRepository
    {
        IEnumerable<Event> Events { get; }

        IEnumerable<Event> PreferredEvents { get; }

        Event GetEventById(int EventId);
    }
}
