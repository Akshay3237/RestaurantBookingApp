namespace Restorent_app.Models
{
    public interface IReposetoryNotification
    {
        //user book table to restaurant,user Cancel Table to restaurant,book from wating list to user
        public NotificationModel createNotification(NotificationModel notification);

        public bool DeletenotificationByNotificationId(int notificationId);

        public bool Deletenotification(NotificationModel notificationModel);

        //delete those notification where userId=notification.userId AND isUserSide=true
        public bool DeleteNotificationByuserId(int userId);

        //delete those notification where restaurantId=notification.restaurantId AND isUserSide=false
        public bool DeleteNotificationByRestaurantId(int reasturantId);
    }
}
