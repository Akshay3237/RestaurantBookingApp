using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restorent_app.Models;

namespace Restorent_app.ViewComponenets
{
    public class MyRestaurantViewComponent : ViewComponent
    {
        private IReposetoryRestaurant reposetoryRestaurant;
        public MyRestaurantViewComponent(IReposetoryRestaurant reposetoryRestaurant)
        {
            this.reposetoryRestaurant=reposetoryRestaurant;
        }

      
        public IViewComponentResult Invoke()
        {
            var sm = HttpContext.Session.GetString("UserId");
            if(sm!=null)
            { 
                int ManagerId = int.Parse(sm);
                RestaurantModel restaurantModel = reposetoryRestaurant.getRestaurantModelByManagerId(ManagerId);
                return View(restaurantModel);
            }
            else
            {
                return View(null);
            }

            
        }
    }
}
