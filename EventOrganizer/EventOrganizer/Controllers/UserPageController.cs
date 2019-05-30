using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using EventOrganizer.ViewModels;
using EventOrganizer.BLL.Interfaces;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventOrganizer.Controllers
{
    public class UserPageController : Controller
    {
        private readonly IEventService _eventService;
        private readonly UserManager<User> _userManager;

        public UserPageController(IEventService eventService, UserManager<User> userManager)
        {
            _eventService = eventService;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.UserEvents = _eventService.GetEventsByUserId(_userManager.GetUserId(User));
            return View();
        }
    }
}
