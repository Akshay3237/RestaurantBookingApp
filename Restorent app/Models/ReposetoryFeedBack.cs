using System.Collections.Generic;

namespace Restorent_app.Models
{
    public class ReposetoryFeedBack : IReposetoryFeedBack
    {
        private readonly RestaurantDBContext dbContext;
        public ReposetoryFeedBack(RestaurantDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public FeedbackModel createFeedBack(FeedbackModel feedback)
        {
            throw new System.NotImplementedException();
        }

        public bool deleteFeedBack(FeedbackModel feedbackModel)
        {
            throw new System.NotImplementedException();
        }

        public bool deleteFeedBackByFeedBackid(int feedbackId)
        {
            throw new System.NotImplementedException();
        }

        public bool deleteFeedbackByRestaurantId(int restaurantId)
        {
            throw new System.NotImplementedException();
        }

        public bool deleteFeedBackByUserId(int userId)
        {
            throw new System.NotImplementedException();
        }

        public FeedbackModel getFeedBackByFeedbackid(int feedBackId)
        {
            throw new System.NotImplementedException();
        }

        public List<FeedbackModel> getFeedBackModelsByRestaurantId(int restaurantId)
        {
            throw new System.NotImplementedException();
        }

        public bool updateFeedBack(FeedbackModel feedbackModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
