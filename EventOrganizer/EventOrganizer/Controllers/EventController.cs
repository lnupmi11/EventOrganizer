using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.Data.Interfaces;
using EventOrganizer.ViewModels;
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
            EventListViewModel vm = new EventListViewModel();
            vm.Events = _eventRepository.Events;

            return View(vm);
        }
    }
}