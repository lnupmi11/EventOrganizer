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
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [HttpPost]
        public async Task<IActionResult> Delete(Event item)
        {
            if (!ModelState.IsValid) RedirectToAction("List");
            _eventService.DeleteItem(item);
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Official, Admin")]
        public ViewResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            Event event_item = new Event
            {
                Name = model.Name,
                ShortDescription = model.ShortDescription,
                LongDescription = model.LongDescription,
                ImageUrl = model.ImageUrl,
                ImageThumbnailUrl = model.ImageThumbnailUrl,
                IsPreferredEvent = model.IsPreferredEvent,
                Category = model.Category,
                ScheduledAt = model.ScheduledAt,
                CreatedAt = DateTime.Now
            };

            _eventService.CreateItem(event_item);
            return RedirectToAction("List");
        }

        public ViewResult Edit(Event item)
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(Event model)
        {
            if (!ModelState.IsValid) return View(model);

            _eventService.EditItem(model);
            return RedirectToAction("List");
        }


    }
}