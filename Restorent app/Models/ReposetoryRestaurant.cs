using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Restorent_app.Models
{
    public class RepositoryRestaurant : IReposetoryRestaurant
    {
        private readonly RestaurantDBContext dbContext;

        public RepositoryRestaurant(RestaurantDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public RestaurantModel createRestorent(RestaurantModel restaurantModel)
        {
            dbContext.Restaurants.Add(restaurantModel);
            dbContext.SaveChanges();
            return restaurantModel;
        }


        public bool deleteRestorantModel(RestaurantModel restaurantModel)
        {
            try
            {
                dbContext.Restaurants.Remove(restaurantModel);
                dbContext.SaveChanges(); // Commit the delete to the database
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in deleteRestaurantModel: {e.Message}");
                return false;
            }
        }

        public bool deleteRestorantModelById(int id)
        {
            try
            {
                var restaurantModel = dbContext.Restaurants.Find(id);
                if (restaurantModel != null)
                {
                    dbContext.Restaurants.Remove(restaurantModel);
                    dbContext.SaveChanges(); // Commit the delete to the database
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in deleteRestaurantModelById: {e.Message}");
                return false;
            }
        }

        

       

        public List<RestaurantModel> getAllRestorent()
        {
            return dbContext.Restaurants.AsNoTracking().ToList(); 
        }

    
        public RestaurantModel getRestaurantModelById(int id)
        {
            return dbContext.Restaurants.Find(id);
        }

        public RestaurantModel getRestaurantModelByManagerId(int managerId)
        {
            try
            {
                string query = "SELECT * FROM [Restaurants] WHERE ManagerId = @managerId";
                SqlParameter sqlParameter = new SqlParameter("@managerId", managerId);
                return dbContext.Restaurants.FromSqlRaw(query, sqlParameter).AsNoTracking().FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in getRestaurantModelByManagerId: {e.Message}");
                return null;
            }
        }

        public RestaurantModel updateRestaurantModel(int id, RestaurantModel updatedRestaurant)
        {
            try
            {
                var existingRestaurant = dbContext.Restaurants.Find(id);
                if (existingRestaurant == null)
                {
                    return null; // Restaurant not found
                }

                // Update properties
                existingRestaurant.RestaurantName = updatedRestaurant.RestaurantName;
                existingRestaurant.RestaurantUniqueName = updatedRestaurant.RestaurantUniqueName;
                existingRestaurant.Address = updatedRestaurant.Address;
                existingRestaurant.ContactNumber = updatedRestaurant.ContactNumber;
                existingRestaurant.Type = updatedRestaurant.Type;
                existingRestaurant.Rating = updatedRestaurant.Rating;
                existingRestaurant.ManagerId = updatedRestaurant.ManagerId;

                dbContext.SaveChanges(); // Commit changes to the database

                return existingRestaurant;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in updateRestaurantModel: {e.Message}");
                return null;
            }
        }
    }
}
