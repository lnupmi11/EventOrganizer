using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.BLL.Interfaces
{
    public interface IEventService
    {
        IEnumerable<Event> GetAll();

        IEnumerable<Event> GetEventsByUserId(string userId);

        IEnumerable<Event> GetEvents(string category);

        IEnumerable<Event> GetPreferredEvents();

        Event GetEventById(int id);

        void CreateItem(Event item);

        void EditItem(Event item);

        void DeleteItem(Event item);
    }
}
