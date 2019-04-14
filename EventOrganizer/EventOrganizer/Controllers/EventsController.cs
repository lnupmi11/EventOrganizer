using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.Data.Interfaces;
using EventOrganizer.Data.Models;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizer.Controllers
{
    public class EventsController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEventRepository _eventRepository;

        public EventsController(ICategoryRepository categoryRepository, IEventRepository eventRepository)
        {
            _categoryRepository = categoryRepository;
            _eventRepository = eventRepository;
        }

        public ViewResult List(string category)
        {
            string _category = category;
            IEnumerable<Event> events = null;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                events = _eventRepository.Events.OrderBy(p => p.EventId);
                currentCategory = "All";
            }
            else
            {
                if (string.Equals("Bussiness", _category, StringComparison.OrdinalIgnoreCase))
                {
                    events = _eventRepository.Events.Where(p => p.Category.Name.Equals("Bussiness")).OrderBy(p => p.Name);
                }
                else if(string.Equals("Education", _category, StringComparison.OrdinalIgnoreCase))
                {
                    events = _eventRepository.Events.Where(p => p.Category.Name.Equals("Education")).OrderBy(p => p.Name);
                }
                else if (string.Equals("Marketing", _category, StringComparison.OrdinalIgnoreCase))
                {
                    events = _eventRepository.Events.Where(p => p.Category.Name.Equals("Marketing")).OrderBy(p => p.Name);
                }
                else if (string.Equals("Entertainment", _category, StringComparison.OrdinalIgnoreCase))
                {
                    events = _eventRepository.Events.Where(p => p.Category.Name.Equals("Entertainment")).OrderBy(p => p.Name);
                }
                else if (string.Equals("Culture", _category, StringComparison.OrdinalIgnoreCase))
                {
                    events = _eventRepository.Events.Where(p => p.Category.Name.Equals("Culture")).OrderBy(p => p.Name);
                }

                currentCategory = _category;
            }

            EventsListViewModel elvm = new EventsListViewModel();

            elvm.Events = events;
            elvm.CurrentCategory = currentCategory;

            return View(elvm);
        }
    }
}