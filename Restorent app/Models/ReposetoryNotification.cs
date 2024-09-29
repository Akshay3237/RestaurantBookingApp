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
            throw new System.NotImplementedException();
        }

        public bool Deletenotification(NotificationModel notificationModel)
        {
            throw new System.NotImplementedException();
        }

        public bool DeletenotificationByNotificationId(int notificationId)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteNotificationByRestaurantId(int reasturantId)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteNotificationByuserId(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
