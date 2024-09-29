using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                dbContext.feedbacks.Add(feedback);
                dbContext.SaveChanges();
                return feedback;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public bool deleteFeedBack(FeedbackModel feedbackModel)
        {
            return deleteFeedBackByFeedBackid(feedbackModel.FeedbackId);
        }

        public bool deleteFeedBackByFeedBackid(int feedbackId)
        {
            FeedbackModel feedbackModel=dbContext.feedbacks.Find(feedbackId);
            if (feedbackModel != null)
            {
                try
                {
                    dbContext.feedbacks.Remove(feedbackModel);
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
                return false;
            }
        }

        public bool deleteFeedbackByRestaurantId(int restaurantId)
        {
            try
            {
                string query = "DELETE FROM [feedbacks] WHERE  RestaurantId=@restaurantId";
                SqlParameter sqlParameter = new SqlParameter("@restaurantId", restaurantId);
                dbContext.Database.ExecuteSqlRaw(query, sqlParameter);
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool deleteFeedBackByUserId(int userId)
        {
            try
            {
                string query = "DELETE FROM [feedbacks] WHERE  UserId=@userId";
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

        public FeedbackModel getFeedBackByFeedbackid(int feedBackId)
        {
            return dbContext.feedbacks.Find(feedBackId);
        }

        public List<FeedbackModel> getFeedBackModelsByRestaurantId(int restaurantId)
        {
            try
            {
                string query = "SELECT * FROM [feedbacks] WHERE  RestaurantId=@restaurantId";
                SqlParameter sqlParameter = new SqlParameter("@restaurantId", restaurantId);
                return dbContext.feedbacks.FromSqlRaw<FeedbackModel>(query, sqlParameter).AsNoTracking<FeedbackModel>().ToList<FeedbackModel>();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public bool updateFeedBack(FeedbackModel feedbackModel)
        {
            FeedbackModel oldFeedBack = dbContext.feedbacks.Find(feedbackModel.FeedbackId);
            if (oldFeedBack != null)
            {
                try
                {
                    oldFeedBack.Message = feedbackModel.Message;
                    oldFeedBack.RateNo = feedbackModel.RateNo;
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
                return false;
            }
        }
    }
}
