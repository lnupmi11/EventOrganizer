﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using EventOrganizer.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventOrganizer.Controllers
{
    public class UserPageController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
