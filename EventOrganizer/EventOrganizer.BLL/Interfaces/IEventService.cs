using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.BLL.Interfaces
{
    public interface IEventService
    {
        IEnumerable<Event> GetAll();

        IEnumerable<Event> GetEvents(string category);

        IEnumerable<Event> GetPreferredEvents();

        Event GetEventById(int id);
    }
}
