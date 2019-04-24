using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.BLL.Interfaces;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.DAL.Models;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizer.Controllers
{
    public class EventsController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IEventService _eventService;

        public EventsController(ICategoryService categoryService, IEventService eventService)
        {
            _categoryService = categoryService;
            _eventService = eventService;
        }

        public ViewResult List(string category)
        {
            IEnumerable<Event> events = null;
            string currentCategory = null;
            currentCategory = _categoryService.GetCategoryName(category);

            if (string.Equals("All", currentCategory))
            {
                events = _eventService.GetAll();
            } else
            {
                events = _eventService.GetEvents(currentCategory);
            }

            EventsListViewModel elvm = new EventsListViewModel();

            elvm.Events = events;
            elvm.CurrentCategory = currentCategory;

            return View(elvm);
        }
    }
}