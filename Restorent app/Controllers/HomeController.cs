using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restorent_app.Models;

namespace Restorent_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RestaurantDBContext _context; // Add DbContext

        public HomeController(ILogger<HomeController> logger, RestaurantDBContext context)
        {
            _logger = logger;
            _context = context; // Initialize DbContext
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Dashboard action
        public IActionResult Dashboard()
        {
            return View(); // This will look for Views/Home/Dashboard.cshtml
        }

        // Action to display the restaurant registration view
        [HttpGet]
        public IActionResult RegisterRestaurant()
        {
            return View(new RestaurantModel()); // Pass an empty model to the view
        }

        // Action to handle restaurant registration form submission
        [HttpPost]
        public IActionResult RegisterRestaurant(RestaurantModel model)
        {
            if (ModelState.IsValid)
            {
                // Save the restaurant to the database
                _context.Restaurants.Add(model); // Add the restaurant model to the context
                _context.SaveChanges(); // Save changes to the database

                // Redirect to a success page or back to the dashboard
                return RedirectToAction("Dashboard"); // Modify as necessary
            }

            // If model state is not valid, return the view with the current model to show validation errors
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
