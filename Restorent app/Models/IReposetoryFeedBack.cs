using System.Collections.Generic;

namespace Restorent_app.Models
{
    public interface IReposetoryFeedBack
    {
        public FeedbackModel createFeedBack(FeedbackModel feedback);

        public bool updateFeedBack(FeedbackModel feedbackModel);

        public bool deleteFeedBack(FeedbackModel feedbackModel);

        public bool deleteFeedBackByFeedBackid(int feedbackId);
        public FeedbackModel getFeedBackByFeedbackid(int feedBackId);

        public List<FeedbackModel> getFeedBackModelsByRestaurantId(int restaurantId);
    }
}
