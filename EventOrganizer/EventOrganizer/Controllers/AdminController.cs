using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EventOrganizer.Data.Models;

namespace EventOrganizer.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<User> userManager;
        public AdminController(UserManager<User> usrMgr)
        {
            userManager = usrMgr;
        }

        public ViewResult Index()
        {
            return View(userManager.Users);
        }

    }
}
