using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restorent_app.Models;

namespace Restorent_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IReposetoryRestaurant reposetoryRestaurant;
        private bool CheckForAuthenticate()
        {
            // Check if the session contains the UserName and UserId keys
            string userName = HttpContext.Session.GetString("UserName");
            string userId = HttpContext.Session.GetString("UserId");

            // If both session values are not null, the user is authenticated
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userId))
            {
                return true;
            }

            // Otherwise, the user is not authenticated
            return false;
        }

        public HomeController(ILogger<HomeController> logger, IReposetoryRestaurant reposetoryRestaurant)
        {
            _logger = logger;
            this.reposetoryRestaurant = reposetoryRestaurant;
        }

        public IActionResult Index()
        {
            if (CheckForAuthenticate())
            {
                int UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                RestaurantModel isAvailableRestaurant = reposetoryRestaurant.getRestaurantModelByManagerId(UserId);
                if (isAvailableRestaurant != null)
                {
                    ViewBag.IsManager = true;
                }
                else
                {
                    ViewBag.IsManager = false;
                }
                ViewBag.IsAuthenticate = true;
                return View();
            }
            else
            {
                return RedirectToAction("Login","Auth");
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
