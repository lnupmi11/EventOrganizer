using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EventOrganizer.DAL.Models;
using EventOrganizer.ViewModels;

namespace EventOrganizer.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<User> userManager;
        public AdminController(UserManager<User> usrMgr)
        {
            userManager = usrMgr;
        }

        public ViewResult Index() => View(userManager.Users);

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            User user = new User
            {
                UserName = model.Name,
                Email = model.Email
            };

            IdentityResult result = await userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
    }
}
