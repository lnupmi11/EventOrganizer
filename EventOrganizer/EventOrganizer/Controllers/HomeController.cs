using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.Data.Interfaces;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventOrganizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public HomeController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public ViewResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PreferredEvents = _eventRepository.PreferredEvents
            };
            return View(homeViewModel);
        }
    }
}
