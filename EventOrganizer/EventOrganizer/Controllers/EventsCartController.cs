using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.Data.Interfaces;
using EventOrganizer.Data.Models;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventOrganizer.Controllers
{
    public class EventsCartController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly EventsCart _eventsCart;

        public EventsCartController(IEventRepository eventRepository, EventsCart eventsCart)
        {
            _eventRepository = eventRepository;
            _eventsCart = eventsCart;
        }

        public ViewResult Index()
        {
            var items = _eventsCart.GetEventsCartItems();
            _eventsCart.EventsCartItems = items;

            var eCVM = new EventsCartViewModel
            {
                EventsCart = _eventsCart
            };
            return View(eCVM);
        }

        public RedirectToActionResult AddToEventsCart(int eventId)
        {
            var selectedEvent = _eventRepository.Events.FirstOrDefault(p => p.EventId == eventId);
            if (selectedEvent != null)
            {
                _eventsCart.AddToCart(selectedEvent);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromEventsCart(int eventId)
        {
            var selectedEvent = _eventRepository.Events.FirstOrDefault(p => p.EventId == eventId);
            if (selectedEvent != null)
            {
                _eventsCart.RemoveFromCart(selectedEvent);
            }
            return RedirectToAction("Index");
        }
    }
}
