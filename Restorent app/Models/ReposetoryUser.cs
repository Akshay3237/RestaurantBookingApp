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
            this.dbContext = dbContext;
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
            dbContext.SaveChanges();
        }

        public bool findUser(string username, string password)
        {
            var user = dbContext.Users.SingleOrDefault(u => u.UserName == username);

            if (user != null)
            {
                // Verify the password (assuming a method for verifying hashed passwords)
                return password==user.Password;
            }

            return false;

            //return false;
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

        public bool userExist(string username, string email)
        {
            return dbContext.Users.Any(u => u.UserName == username || u.Email == email);

        }
    }
}

