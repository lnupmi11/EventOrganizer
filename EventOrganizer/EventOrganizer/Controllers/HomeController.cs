using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.BLL.Interfaces;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventOrganizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;
        
        public HomeController(IEventService eventService)
        {
            _eventService = eventService;
        }
        public ViewResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PreferredEvents = _eventService.GetPreferredEvents()
            };
            return View(homeViewModel);
        }
    }
}
