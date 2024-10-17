using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restorent_app.Models;

namespace Restorent_app.Controllers
{
    public class NotificationController : Controller
    {
        IReposetoryNotification reposetoryNotification;
        IReposetoryUser reposetoryUser;
        IReposetoryRestaurant reposetoryRestaurant;
        public NotificationController(IReposetoryNotification reposetoryNotification, IReposetoryUser reposetoryUser, IReposetoryRestaurant reposetoryRestaurant)
        {
            this.reposetoryNotification = reposetoryNotification;
            this.reposetoryUser = reposetoryUser;
            this.reposetoryRestaurant = reposetoryRestaurant;
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
       

        public IActionResult ShowNotificationForClient()
        {
            if (CheckForAuthenticate())
            {
                int UserId = int.Parse(HttpContext.Session.GetString("UserId"));

                // Fetch notifications for the authenticated user
                List<NotificationModel> notificationModels = reposetoryNotification.GetAllNotifications()
                    .Where(notification => notification.UserId == UserId && notification.IsUserSide)
                    .ToList();
                foreach(NotificationModel notificationModel in notificationModels)
                {
                    notificationModel.User = reposetoryUser.getUserByUserId(notificationModel.UserId);
                    notificationModel.Restaurant = reposetoryRestaurant.getRestaurantModelById(notificationModel.RestaurantId);
                }
                ViewBag.IsAuthenticate = true;
                return View(notificationModels);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }
        public IActionResult DeleteNotification(int id)
        {
            if (CheckForAuthenticate())
            {
                reposetoryNotification.DeletenotificationByNotificationId(id);
                return RedirectToAction("ShowNotificationForClient");
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        // Method for deleting all notifications for the user
        public IActionResult DeleteAllNotifications()
        {
            if (CheckForAuthenticate())
            {
                int userId = int.Parse(HttpContext.Session.GetString("UserId"));
                reposetoryNotification.DeleteNotificationByuserId(userId);
                return RedirectToAction("ShowNotificationForClient");
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

      

        public IActionResult ShowNotificationForRestaurant(int RestaurantId)
        {
            if (CheckForAuthenticate())
            {
               
                // Fetch notifications for the restaurant
                List<NotificationModel> notificationModels = reposetoryNotification.GetAllNotifications()
                    .Where(notification => notification.RestaurantId == RestaurantId && !notification.IsUserSide)
                    .ToList();
                foreach (NotificationModel notificationModel in notificationModels)
                {
                    notificationModel.User = reposetoryUser.getUserByUserId(notificationModel.UserId);
                    notificationModel.Restaurant = reposetoryRestaurant.getRestaurantModelById(notificationModel.RestaurantId);
                }
                ViewBag.RestaurantId = RestaurantId;
                ViewBag.IsAuthenticate = true;
                return View(notificationModels);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public IActionResult DeleteAllRestaurantNotifications(int RestaurantId)
        {
            if (CheckForAuthenticate())
            {

                reposetoryNotification.DeleteNotificationByRestaurantId(RestaurantId);
                return RedirectToAction("ShowNotificationForRestaurant",new {RestaurantId=RestaurantId});
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public IActionResult DeleteNotificationForRestaurant(int id)
        {
            if (CheckForAuthenticate())
            {
                NotificationModel notificationModel = reposetoryNotification.GetAllNotifications().Where((notification) => notification.NotificationId == id).ToList()[0];
                if (notificationModel != null)
                {
                    int RestaurantId = notificationModel.RestaurantId;
                    reposetoryNotification.DeletenotificationByNotificationId(id);
                    return RedirectToAction("ShowNotificationForRestaurant",new {RestaurantId=RestaurantId});
                }
                return RedirectToAction("Logout", "Auth");
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

    }
}
