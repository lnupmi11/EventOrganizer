using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.BLL.Interfaces;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.DAL.Models;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventOrganizer.Controllers
{
    public class EventsController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IEventService _eventService;
        private readonly UserManager<User> _userManager;

        public EventsController(ICategoryService categoryService, IEventService eventService, UserManager<User> userManager)
        {
            _categoryService = categoryService;
            _eventService = eventService;
            _userManager = userManager;
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
        [Authorize(Roles = "Official, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) return RedirectToAction("List");
            var item = _eventService.GetEventById(id);
            var usr = await _userManager.GetUserAsync(User);
            if (await _userManager.IsInRoleAsync(usr, "Admin") || usr.Id == item.UserId)
            {
                _eventService.DeleteItem(item);
                return RedirectToAction("List");
            }
            else
            {
                return Forbid();
            }
        }

        [Authorize(Roles = "Official")]
        public ViewResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Official")]
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
                CategoryId = model.CategoryId,
                UserId = _userManager.GetUserId(User),
                ScheduledAt = model.ScheduledAt,
                CreatedAt = DateTime.Now
            };

            _eventService.CreateItem(event_item);
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Official")]
        public ViewResult Edit(int id)
        {
            var item = _eventService.GetEventById(id);
            if (_userManager.GetUserId(User) == item.UserId)
            {
                ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name");
            }
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "Official")]
        public async Task<IActionResult> EditEvent(Event model)
        {
            if (!ModelState.IsValid) return View(model);
            var oldModel = _eventService.GetEventById(model.Id);
            if (_userManager.GetUserId(User) == oldModel.UserId)
            {
                _eventService.EditItem(model);
            }
            return RedirectToAction("List");
        }

        public ViewResult Show(int id)
        {
            Event item = _eventService.GetEventById(id);
            return View(item);
        }
    }
}