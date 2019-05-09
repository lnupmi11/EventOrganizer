using System;
using System.Collections.Generic;
using System.Linq;
using EventOrganizer.BLL.Interfaces;
using EventOrganizer.DAL.Models;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventOrganizer.Controllers
{
    public class EventsCartItemsController : Controller
    {
        private readonly IEventsCartItemsService _eventsCartItemsService;

        public EventsCartItemsController(IEventsCartItemsService eventsCartItemsRepository)
        {
            _eventsCartItemsService = eventsCartItemsRepository;
        }

        public ViewResult Index()
        {
            var items = _eventsCartItemsService.GetEventsCartItems();

            var eCIVM = new EventsCartItemsViewModel
            {
                EventsCartItems = items
            };

            return View(eCIVM);
        }

        public RedirectToActionResult AddToEventsCart(Event @event)
        {
            _eventsCartItemsService.AddToCart(@event);
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromEventsCart(Event @event)
        {
            _eventsCartItemsService.RemoveFromCart(@event);
            return RedirectToAction("Index");
        }
    }
}
