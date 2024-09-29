using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Restorent_app.Models
{
    public class ReposetoryUser : IReposetoryUser
    {
        private readonly RestaurantDBContext dbContext;

        public ReposetoryUser(RestaurantDBContext dbContext)
        {
            dbContext = dbContext;
        }
        
        public UserModel createUser(UserModel user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user;
        }

        public void deleteUser(int id)
        {
            UserModel user=dbContext.Users.Find(id);
            if (user != null)
            {
                dbContext.Users.Remove(user);
            }
        }

        public bool findUser(string username, string password)
        {
            string query = "SELECT COUNT(1) FROM Users WHERE Username = @username AND Password = @password";

            // Parameters for the query
            var usernameParam = new SqlParameter("@username", username);
            var passwordParam = new SqlParameter("@password", password);

            // Execute the query and return true if the user exists
            int result = dbContext.Database.ExecuteSqlRaw(query, usernameParam, passwordParam);
            return result > 0;
        }

        public UserModel updateUser(int id, UserModel updatedUser)
        {
           
                // Find the user by id
                UserModel user = dbContext.Users.Find(id);

                if (user == null)
                {
                    return null; // Or handle as needed (e.g., throw exception or return a message)
                }

                // Update the user's fields with the new data
                user.UserName = updatedUser.UserName;
                user.Password = updatedUser.Password;
                user.Email = updatedUser.Email;
                user.PhoneNo = updatedUser.PhoneNo;
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Surname = updatedUser.Surname;
                // Add other fields that need to be updated

                // Save changes to the database
                dbContext.SaveChanges();

                return user;
            }

        }
    }
}
