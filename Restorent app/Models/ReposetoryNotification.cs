using System;
using System.Collections.Generic;
using System.Linq;
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
                List<NotificationModel> notificationModels = GetAllNotifications().Where((notification) => notification.RestaurantId == reasturantId && (!notification.IsUserSide)).ToList();

                foreach (NotificationModel notification in notificationModels)
                {
                    DeletenotificationByNotificationId(notification.NotificationId);
                }
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
                List<NotificationModel> notificationModels = GetAllNotifications().Where((notification) => notification.UserId == userId && notification.IsUserSide).ToList();

                foreach(NotificationModel notification in notificationModels)
                {
                    DeletenotificationByNotificationId(notification.NotificationId);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public List<NotificationModel> GetAllNotifications()
        {
            return dbContext.notifications.ToList();
        }
    }
}
