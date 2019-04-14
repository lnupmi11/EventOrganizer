using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.Data.Interfaces;
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

        public ViewResult List()
        {
            EventsListViewModel vm = new EventsListViewModel();
            vm.Events = _eventRepository.Events;
            vm.CurrentCategory = "EC";

            return View(vm);
        }
    }
}