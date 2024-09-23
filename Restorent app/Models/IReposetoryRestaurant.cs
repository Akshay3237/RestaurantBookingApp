using System.Collections.Generic;

namespace Restorent_app.Models
{
    public interface IReposetoryRestaurant
    {
        public RestaurantModel createRestorent(RestaurantModel restaurantModel);

        public List<RestaurantModel> getAllRestorent();

        public RestaurantModel getRestaurantModelById(int id);

        public RestaurantModel getRestaurantModelByManagerId(int id);

        public RestaurantModel updateRestaurantModel(int id, RestaurantModel restaurant);

        public bool deleteRestorantModelById(int id);
        
        public bool deleteRestorantModel(RestaurantModel restaurantModel);
    }
}
