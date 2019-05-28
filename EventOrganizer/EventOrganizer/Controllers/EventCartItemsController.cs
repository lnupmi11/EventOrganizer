﻿using System.Collections.Generic;
using EventOrganizer.BLL.Interfaces;
using EventOrganizer.DAL.Models;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizer.Controllers
{
    public class EventCartItemsController : Controller
    {
        private UserManager<User> _userManager;
        private readonly IEventService _eventService;
        private readonly IEventCartItemsService _eventCartItemsService;

        public EventCartItemsController(UserManager<User> userManager, IEventService eventService, IEventCartItemsService eventCartItemsRepository)
        {
            _userManager = userManager;
            _eventService = eventService;
            _eventCartItemsService = eventCartItemsRepository;
        }

        public ViewResult Index()
        {
            string userId = _userManager.GetUserId(User);
            var items = _eventCartItemsService.GetEventCartItems(userId);
            List<Event> events = new List<Event>();
            foreach(var item in items)
            {
                events.Add(_eventService.GetEventById(item.EventId));
            }
            var eCIVM = new EventCartItemsViewModel
            {
                EventCartItems = events
            };

            return View(eCIVM);
        }

        public RedirectToActionResult AddToEventCart(int eventId)
        {
            string userId = _userManager.GetUserId(User);
            _eventCartItemsService.AddToCart(eventId, userId);
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromEventCart(int eventId)
        {
            string userId = _userManager.GetUserId(User);
            _eventCartItemsService.RemoveFromCart(eventId, userId);
            return RedirectToAction("Index");
        }
    }
}
