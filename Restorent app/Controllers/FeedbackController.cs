using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restorent_app.Models;

namespace Restorent_app.Controllers
{
    public class FeedbackController : Controller
    {
        private IReposetoryFeedBack reposetoryFeedBack;
        private IReposetoryUser feedbackUser;
        private IReposetoryRestaurant reposetoryRestaurant;
        private IReposetoryNotification reposetoryNotification;

        public FeedbackController(IReposetoryFeedBack reposetoryFeedBack, IReposetoryUser feedbackUser, IReposetoryRestaurant reposetoryRestaurant, IReposetoryNotification reposetoryNotification)
        {
            this.reposetoryFeedBack = reposetoryFeedBack;
            this.feedbackUser = feedbackUser;
            this.reposetoryRestaurant = reposetoryRestaurant;
            this.reposetoryNotification = reposetoryNotification;
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
        [HttpGet]
        public IActionResult GiveFeedback(int id)
        {
            if (CheckForAuthenticate())
            {
                FeedbackModel feedbackModel = new FeedbackModel
                {
                    RestaurantId = id,
                    UserId = int.Parse(HttpContext.Session.GetString("UserId"))
                };
                feedbackModel.Restaurant= reposetoryRestaurant.getRestaurantModelById(id);
              
                ViewBag.IsAuthenticate = true;
                return View(feedbackModel);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpPost]
        public IActionResult GiveFeedback(FeedbackModel feedbackModel)
        {
            feedbackModel.User = feedbackUser.getUserByUserId(feedbackModel.UserId);
            feedbackModel.Restaurant = reposetoryRestaurant.getRestaurantModelById(feedbackModel.RestaurantId);
            if (ModelState.IsValid)
            {
                // Assuming reposetoryFeedback is your feedback repository
                feedbackModel.User = feedbackUser.getUserByUserId(feedbackModel.UserId);
                feedbackModel.Restaurant = reposetoryRestaurant.getRestaurantModelById(feedbackModel.RestaurantId);
                reposetoryFeedBack.createFeedBack(feedbackModel);
                RestaurantModel restaurantModel = reposetoryRestaurant.getRestaurantModelById(feedbackModel.RestaurantId);
                int Ret = feedbackModel.RateNo;
                restaurantModel.Rating = (restaurantModel.Rating + feedbackModel.RateNo) / 2;
                reposetoryRestaurant.updateRestaurantModel(feedbackModel.RestaurantId,restaurantModel);
                return RedirectToAction("Details", "Restaurant",new {id=feedbackModel.RestaurantId });
            }
            ViewBag.IsAuthenticate = true;
            return View(feedbackModel);
        }
        public IActionResult ShowFeedback(int id)
        {
            if (CheckForAuthenticate())
            {

                List<FeedbackModel> feedbackModels = reposetoryFeedBack.getFeedBackModelsByRestaurantId(id);
                foreach(FeedbackModel feedbackModel in feedbackModels)
                {
                    feedbackModel.User = feedbackUser.getUserByUserId(feedbackModel.UserId);
                    feedbackModel.Restaurant = reposetoryRestaurant.getRestaurantModelById(feedbackModel.RestaurantId);
                }
                ViewBag.IsAuthenticate = true;
                return View(feedbackModels);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }
    }
}
