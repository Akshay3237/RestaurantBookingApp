using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restorent_app.Models;

namespace Restorent_app.Controllers
{
    public class BookingController : Controller
    {
        private IReposetoryBook reposetoryBook;
        private IReposetoryTable reposetoryTable;
        private IReposetoryRestaurant reposetoryRestaurant;
        private IReposetoryUser reposetoryUser;
        private IReposetoryNotification reposetoryNotification;
        public BookingController(IReposetoryBook reposetoryBook,IReposetoryTable reposetoryTable,IReposetoryRestaurant reposetoryRestaurant,IReposetoryUser reposetoryUser,IReposetoryNotification reposetoryNotification)
        {
            this.reposetoryBook = reposetoryBook;
            this.reposetoryTable=reposetoryTable;
            this.reposetoryRestaurant=reposetoryRestaurant;
            this.reposetoryUser = reposetoryUser;
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
        public IActionResult Book(int tableId)
        {
            if (CheckForAuthenticate())
            {
                if (tableId <= 0)
                {
                    return NotFound();
                }
                else
                {
                    TableModel tableModel = reposetoryTable.getTableModelByTableId(tableId);
                    if (tableModel == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        tableModel.Restaurant = reposetoryRestaurant.getRestaurantModelById(tableModel.RestaurantId);
                        BookModel bookModel = new BookModel() { Table = tableModel, TableId = tableId };
                        ViewBag.IsAuthenticate = true;
                        return View(bookModel);
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpPost]
        public IActionResult Book(BookModel bookModel)
        {
            if (CheckForAuthenticate())
            {
                bookModel.Table = reposetoryTable.getTableModelByTableId(bookModel.TableId);
                bookModel.Table.Restaurant = reposetoryRestaurant.getRestaurantModelById(bookModel.Table.RestaurantId);

                if (ModelState.IsValid)
                {
                    int UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                    bookModel.UserId = UserId;
                    bookModel.User = reposetoryUser.getUserByUserId(UserId);
                    // Logic to save the booking to the database
                    if(reposetoryBook.isAvailable(bookModel)) // Make sure this method saves the booking
                    {
                       
                        bool done=reposetoryBook.bookTable(bookModel);
                        if (done)
                        {
                            
                            string message = "You Book Restaurant " + bookModel.Table.Restaurant.RestaurantName + "'s table of capacity " + bookModel.Table.Capacity.ToString()+" Time from "+bookModel.StartTime.ToString("g")+" to "+bookModel.EndTime.ToString("g"); 
                            NotificationModel notificationModel =new NotificationModel() { UserId= UserId,User=bookModel.User,RestaurantId=bookModel.Table.RestaurantId,Restaurant=bookModel.Table.Restaurant,IsUserSide=true,Message=message };
                            reposetoryNotification.createNotification(notificationModel);
                            notificationModel.IsUserSide = false;
                            message= bookModel.User.UserName+" Book Restaurant " + bookModel.Table.Restaurant.RestaurantName + "'s table of capacity " + bookModel.Table.Capacity.ToString() + " Time from " + bookModel.StartTime.ToString("g") + " to " + bookModel.EndTime.ToString("g");
                            notificationModel.Message = message;
                            notificationModel.NotificationId = 0;
                            reposetoryNotification.createNotification(notificationModel);
                            return RedirectToAction("ShowTableList", "Table", new { id = bookModel.Table.RestaurantId }); // Redirect to a confirmation page or similar
                        }
                        else
                        {
                            ViewBag.error = "Please select time for future booking";
                            ViewBag.IsAuthenticate = true;
                            return View(bookModel);
                        }
                        
                    }
                    else
                    {
                        ViewBag.error = "slot is not available";
                        ViewBag.IsAuthenticate = true;
                        return View(bookModel);
                    }
                   
                }
                else
                {
                    ViewBag.IsAuthenticate = true;
                    return View(bookModel); // Return to the view with validation errors
                }
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public IActionResult ShowMyBooking()
        {
            if (CheckForAuthenticate())
            {
                int userId = int.Parse(HttpContext.Session.GetString("UserId"));
                List<BookModel> bookModels = reposetoryBook.getBookingByUserId(userId);

                // Check if there are any bookings
                if (bookModels != null && bookModels.Count > 0)
                {
                    foreach(BookModel bookModel in bookModels)
                    {
                        bookModel.Table = reposetoryTable.getTableModelByTableId(bookModel.TableId);
                        bookModel.Table.Restaurant = reposetoryRestaurant.getRestaurantModelById(bookModel.Table.RestaurantId);
                    }

                    ViewBag.IsAuthenticate = true;
                    return View(bookModels); // Return the list of bookings to the view
                }
                else
                {
                    ViewBag.Message = "You have no bookings.";
                    ViewBag.IsAuthenticate = true;
                    return View(new List<BookModel>()); // Return an empty list to the view
                }
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }

        }

        public IActionResult CancelBooking(int BookId)
        {
            if (CheckForAuthenticate())
            {

                if (BookId <= 0)
                {
                    return NotFound();
                }
                else
                {
                    BookModel bookModel = reposetoryBook.getBookModelByBookId(BookId);
                    if (bookModel != null)
                    {
                        int UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                        if (bookModel.UserId != UserId)
                        {
                            return RedirectToAction("ShowMyBooking", "Booking");
                        }
                        bookModel.Table = reposetoryTable.getTableModelByTableId(bookModel.TableId);
                        bookModel.Table.Restaurant = reposetoryRestaurant.getRestaurantModelById(bookModel.Table.RestaurantId);
                        string message = "You have successfully canceled your booking at \"" + bookModel.Table.Restaurant.RestaurantName + "\" for a table with a capacity of " + bookModel.Table.Capacity.ToString() + "." + "The booking was scheduled from " + bookModel.StartTime.ToString("g") + " to " + bookModel.EndTime.ToString("g") + ".";
                        bookModel.User = reposetoryUser.getUserByUserId(UserId);
                        NotificationModel notificationModel = new NotificationModel() { UserId = UserId, User = bookModel.User, RestaurantId = bookModel.Table.RestaurantId, Restaurant = bookModel.Table.Restaurant, IsUserSide = true, Message = message };
                        reposetoryNotification.createNotification(notificationModel);
                        notificationModel.IsUserSide = false;
                        message = bookModel.User.UserName + " canceled the booking at \"" + bookModel.Table.Restaurant.RestaurantName + "\" for a table with a capacity of " + bookModel.Table.Capacity.ToString() + "." +  "The booking was from " + bookModel.StartTime.ToString("g") + " to " + bookModel.EndTime.ToString("g") + ".";

                        notificationModel.Message = message;
                        notificationModel.NotificationId = 0;
                        reposetoryNotification.createNotification(notificationModel);



                        reposetoryBook.cancelBook(bookModel);
                        return RedirectToAction("ShowMyBooking", "Booking");
                    }
                    else
                    {
                        return RedirectToAction("ShowMyBooking","Booking");
                    }
                }
            
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public IActionResult ShowBookings(int TableId)
        {
            if (CheckForAuthenticate())
            {
                List<BookModel> bookModels = reposetoryBook.getBookingByTableId(TableId);
                if (bookModels != null && bookModels.Count > 0)
                {
                    foreach (BookModel bookModel in bookModels)
                    {
                        bookModel.Table = reposetoryTable.getTableModelByTableId(bookModel.TableId);
                        bookModel.Table.Restaurant = reposetoryRestaurant.getRestaurantModelById(bookModel.Table.RestaurantId);
                        bookModel.User = reposetoryUser.getUserByUserId(bookModel.UserId);
                    }
                    ViewBag.IsAuthenticate = true;
                    return View(bookModels);
                }
                else
                {
                    ViewBag.IsAuthenticate = true;
                    ViewBag.ErrorMessage = "No bookings found for this table.";
                    return View(new List<BookModel>());
                }
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        

        public IActionResult CancelBookingByManager(int BookId)
        {
            if (CheckForAuthenticate())
            {

                if (BookId <= 0)
                {
                    return NotFound();
                }
                else
                {
                    BookModel bookModel = reposetoryBook.getBookModelByBookId(BookId);
                    if (bookModel != null)
                    {
                        bookModel.Table = reposetoryTable.getTableModelByTableId(bookModel.TableId);
                        RestaurantModel restaurantModel = reposetoryRestaurant.getRestaurantModelById(bookModel.Table.RestaurantId);

                        int UserId = int.Parse(HttpContext.Session.GetString("UserId"));
                        if (restaurantModel.ManagerId != UserId)
                        {

                            return RedirectToAction("ShowMyBooking", "Booking");
                        }
                        bookModel.Table = reposetoryTable.getTableModelByTableId(bookModel.TableId);
                        bookModel.Table.Restaurant = reposetoryRestaurant.getRestaurantModelById(bookModel.Table.RestaurantId);
                        string message = "Your booking at \"" + bookModel.Table.Restaurant.RestaurantName + "\" for a table with a capacity of " + bookModel.Table.Capacity.ToString() + " has been canceled by the restaurant. The booking was scheduled from " + bookModel.StartTime.ToString("g") + " to " + bookModel.EndTime.ToString("g") + ".";

                        bookModel.User = reposetoryUser.getUserByUserId(bookModel.UserId);
                        NotificationModel notificationModel = new NotificationModel() { UserId = UserId, User = bookModel.User, RestaurantId = bookModel.Table.RestaurantId, Restaurant = bookModel.Table.Restaurant, IsUserSide = true, Message = message };
                        reposetoryNotification.createNotification(notificationModel);
                        notificationModel.IsUserSide = false;
                        
                        message = "You have successfully canceled the booking for \"" + bookModel.User.UserName + "\" at your restaurant \"" + bookModel.Table.Restaurant.RestaurantName + "\" for a table with a capacity of " + bookModel.Table.Capacity.ToString() + ". The booking was scheduled from " + bookModel.StartTime.ToString("g") + " to " + bookModel.EndTime.ToString("g") + ".";
                        notificationModel.Message = message;
                        notificationModel.NotificationId = 0;
                        reposetoryNotification.createNotification(notificationModel);



                        reposetoryBook.cancelBook(bookModel);
                        return RedirectToAction("ShowMyBooking", "Booking");
                    }
                    else
                    {
                        return RedirectToAction("ShowMyBooking", "Booking");
                    }
                }

            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }
    }
}
