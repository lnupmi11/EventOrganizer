using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizer.Controllers
{
    public class EventController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEventRepository _eventRepository;

        public EventController(ICategoryRepository categoryRepository, IEventRepository eventRepository)
        {
            _categoryRepository = categoryRepository;
            _eventRepository = eventRepository;
        }

        public ViewResult List()
        {
            var events = _eventRepository.Events;
            return View(events);
        }
    }
}