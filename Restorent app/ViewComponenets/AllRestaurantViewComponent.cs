using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Restorent_app.Models;

namespace Restorent_app.ViewComponenets
{
    public class AllRestaurantViewComponent : ViewComponent
    {
        private IReposetoryRestaurant reposetoryRestaurant;
        public AllRestaurantViewComponent(IReposetoryRestaurant reposetoryRestaurant)
        {
            this.reposetoryRestaurant = reposetoryRestaurant;
        }

        public IViewComponentResult Invoke()
        {
            List<RestaurantModel> restaurantModelsList = reposetoryRestaurant.getAllRestorent();
            if (restaurantModelsList != null)
            {
                return View(restaurantModelsList);
            }
            else
            {
                return View(null);
            }
        }
    }
}
