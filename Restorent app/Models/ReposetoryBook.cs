using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Restorent_app.Models
{
    public class ReposetoryBook : IReposetoryBook
    {
        private readonly RestaurantDBContext dbContext;

      

        public List<KeyValuePair<DateTime, DateTime>> availibility(int tableId, DateTime Date)
        {
            string query = "SELECT * FROM Books where TableId=@tableId ORDER BY StartTime ASC";
            SqlParameter sqlParameter = new SqlParameter("tableId", tableId);
            List<BookModel> bookingList = dbContext.Books.FromSqlRaw<BookModel>(query, sqlParameter).AsNoTracking().ToList();
            List<KeyValuePair<DateTime, DateTime>> timingList = new List<KeyValuePair<DateTime, DateTime>>();
            DateTime startDateTime = DateAndTime.Now;
            DateTime endDateTime;
            foreach (BookModel bookModel in bookingList)
            {
                endDateTime = bookModel.StartTime;
                timingList.Add(new KeyValuePair<DateTime, DateTime>(startDateTime,endDateTime));
                startDateTime = bookModel.EndTime;
            }
            endDateTime = DateAndTime.DateAdd(DateInterval.Day, 7, startDateTime);
            timingList.Add(new KeyValuePair<DateTime, DateTime> ( startDateTime, endDateTime ));
            return timingList;
           
        }

        public bool bookTable(BookModel book)
        {
            if (book.StartTime.CompareTo(book.EndTime)<0 && book.EndTime.CompareTo(DateAndTime.Now)>0 &&isAvailable(book))
            {
                try
                {
                    dbContext.Books.Add(book);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception e)
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

        public bool cancelBook(BookModel book)
        {
            BookModel bookModel = dbContext.Books.Find(book.BookId);
            if (bookModel != null)
            {
                try
                {
                    dbContext.Books.Remove(bookModel);
                    dbContext.SaveChanges();
                    return true;
                }catch(Exception e)
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
        //start time < new endtime AND endtime > new starttime
        public bool isAvailable(BookModel book)
        {
            if (book.StartTime.CompareTo(book.EndTime)<0)
            {
                string query = "SELECT * FROM [books] WHERE StartTime < @endtime AND EndTime > @startTime";

                DateTime startTime = book.StartTime.AddMinutes(-5);
                DateTime endTime = book.EndTime.AddMinutes(5);

                // Create SqlParameters with correct names
                SqlParameter sqlParameterforStartTime = new SqlParameter("@startTime", startTime);
                SqlParameter sqlParameterforEndTime = new SqlParameter("@endtime", endTime);

                // Fetch list of bookings that conflict with the new booking time
                List<BookModel> listOfBooking = dbContext.Books
                    .FromSqlRaw(query, sqlParameterforEndTime, sqlParameterforStartTime)
                    .AsNoTracking()
                    .ToList();

                // Check if any bookings conflict
                return listOfBooking.Count == 0;  // returns true if there are no conflicts

            }
            else
            {


                return false;
            }

           
        }
    }
}
