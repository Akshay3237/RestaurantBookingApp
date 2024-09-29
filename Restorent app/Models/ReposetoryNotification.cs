using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace Restorent_app.Models
{
    public class ReposetoryNotification : IReposetoryNotification
    {
        private readonly RestaurantDBContext dbContext;
        public ReposetoryNotification(RestaurantDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public NotificationModel createNotification(NotificationModel notification)
        {
            try
            {
                dbContext.notifications.Add(notification);
                dbContext.SaveChanges();
                return notification;
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public bool Deletenotification(NotificationModel notificationModel)
        {

            return DeletenotificationByNotificationId(notificationModel.NotificationId);
            
        }

        public bool DeletenotificationByNotificationId(int notificationId)
        {
            NotificationModel notificationModel=dbContext.notifications.Find(notificationId);
            if (notificationModel != null) {
                try
                {

                    dbContext.notifications.Remove(notificationModel);
                    dbContext.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Something went wrong in notification deletion");
                return false;
            }

        }

        public bool DeleteNotificationByRestaurantId(int reasturantId)
        {
             try
            {
                string query = "DELETE FROM [notifications] where RestaurantId=@restaurantId AND IsUserSide=FALSE";
                SqlParameter sqlParameter = new SqlParameter("@restaurantId", reasturantId);
                dbContext.Database.ExecuteSqlRaw(query,sqlParameter);
                
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            
        }

        public bool DeleteNotificationByuserId(int userId)
        {
            try
            {
                string query = "DELETE FROM [notifications] where UserId=@userId AND IsUserSide=True";
                SqlParameter sqlParameter = new SqlParameter("@userId", userId);
                dbContext.Database.ExecuteSqlRaw(query, sqlParameter);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
    }
}
