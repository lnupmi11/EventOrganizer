using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.DAL.Interfaces
{
    public interface IEventRepository
    {
        IEnumerable<Event> Events { get; }

        IEnumerable<Event> PreferredEvents { get; }

        Event GetEventById(int EventId);
    }
}
