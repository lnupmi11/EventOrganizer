using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using EventOrganizer.ViewModels;
using EventOrganizer.BLL.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventOrganizer.Controllers
{
    public class UserPageController : Controller
    {
        private readonly IEventCartItemsService _eventsCartItemsService;

        public UserPageController(IEventCartItemsService eventsCartItemsRepository)
        {
            _eventsCartItemsService = eventsCartItemsRepository;
        }

        public ViewResult Index()
        {
            var items = _eventsCartItemsService.GetEventsCartItems();

            var eCIVM = new EventCartItemsViewModel
            {
                EventCartItems = items
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
