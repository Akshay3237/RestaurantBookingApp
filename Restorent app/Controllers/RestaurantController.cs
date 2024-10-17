using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restorent_app.Models;

namespace Restorent_app.Controllers
{
    public class RestaurantController : Controller
    {
        private  IReposetoryRestaurant reposetoryRestaurant;
        private IReposetoryTable reposetoryTable;

        public RestaurantController(IReposetoryRestaurant reposetoryRestaurant, IReposetoryTable reposetoryTable)
        {
            this.reposetoryRestaurant = reposetoryRestaurant;
            this.reposetoryTable = reposetoryTable;
        }

        public IActionResult Index()
        {
            return View();
        }

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
        [HttpGet]
        public IActionResult AddRestaurant()
        {
            // Logic for displaying or handling the add restaurant form
            if (!CheckForAuthenticate())
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.IsAuthenticate = true;
            return View(new RestaurantModel());
        }

        [HttpPost]
        public IActionResult AddRestaurant(RestaurantModel restaurantModel)
        {
            // Check if the user is authenticated
            if (!CheckForAuthenticate())
            {
                // Redirect to Login page if the user is not authenticated
                return RedirectToAction("Login", "Auth");
            }
            int ManagerId = int.Parse(HttpContext.Session.GetString("UserId"));
            RestaurantModel AlreadyAvailablerestaurant=reposetoryRestaurant.getRestaurantModelByManagerId(ManagerId);
            if (AlreadyAvailablerestaurant != null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Check if the submitted form is valid
            if (ModelState.IsValid)
            {
                // Add the ManagerId to the model from the session (assuming the user is logged in and their ID is stored in session)
                restaurantModel.ManagerId = ManagerId;

                // Save restaurant details to the database (Assuming you have a repository for restaurant handling)
                reposetoryRestaurant.createRestorent(restaurantModel); // Implement the create method in your repository.

                // Redirect to some page, like a dashboard or a list of restaurants
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IsAuthenticate = true;
            // If model is not valid, return the form with validation errors
            return View(restaurantModel);
        }
        [HttpGet]
        public IActionResult ShowAllRestaurants()
        {
            if (CheckForAuthenticate())
            {


                ViewBag.IsAuthenticate = true;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public IActionResult SearchSuggestions(string searchTerm)
        {
            List<RestaurantModel> restaurants = reposetoryRestaurant.getAllRestorent();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Filter restaurants by search term
                restaurants = restaurants.Where(r => r.RestaurantName.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();

            }
            else
            {
                restaurants=null;
            }

            return PartialView("_RestaurantSuggestionsPartial", restaurants);
        }

        public IActionResult Details(int id)
        {
            if (CheckForAuthenticate())
           {
                ViewBag.IsAuthenticate = true;
                var restaurant = reposetoryRestaurant.getRestaurantModelById(id);
                if (restaurant == null)
                {
                    ViewBag.RestaurantId = id;
                    return View("NotFound");
                }
                    return View(restaurant);
                }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public IActionResult ShowTables(int id)
        {
            int restaurantId = id;
            if (CheckForAuthenticate())
            {
                ViewBag.IsAuthenticate = true;
                if (restaurantId == null)
                {
                    // Handle the case when restaurantId is not provided
                    return NotFound(); // Redirect to a NotFound view or an error page
                }
                List<TableModel> tables = reposetoryTable.getTableModelsByRestaurantId(restaurantId);
                ViewData["RestaurantId"] = restaurantId;
                return View(tables);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpGet]
        public IActionResult AddTable(int id)
        {
            if (id <= 0)
            {
                return NotFound(id);
            }

            if (CheckForAuthenticate())
            {
                RestaurantModel restaurant = reposetoryRestaurant.getRestaurantModelById(id);

                // Check if the restaurant exists
                if (restaurant == null)
                {
                    return NotFound(); // Handle restaurant not found case
                }
                ViewBag.IsAuthenticate = true;
                return View(new TableModel() { RestaurantId = id, Restaurant = restaurant });
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpPost]
        public IActionResult AddTable(TableModel tableModel)
        {
            if (ModelState.IsValid)
            {
                // Save the tableModel to the database
                reposetoryTable.addTable(tableModel);
                return RedirectToAction("ShowTables", new { id = tableModel.RestaurantId }); // Redirect to the list of tables
            }
            ViewBag.IsAuthenticate = true;
            // If model state is not valid, return the same view with the model to display errors
            return View(tableModel);
        }

    }
}
