using Microsoft.AspNetCore.Mvc;
using Restorent_app.Models;
using System.Collections.Generic;
using System.Linq;

namespace Restorent_app.ViewComponents
{
    public class SearchRestaurantViewComponent : ViewComponent
    {
        private readonly IReposetoryRestaurant _restaurantRepository;

        public SearchRestaurantViewComponent(IReposetoryRestaurant restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public IViewComponentResult Invoke(string searchTerm)
        {
            

            return View(new List<RestaurantModel>());
        }
    }
}
