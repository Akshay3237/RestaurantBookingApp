using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Linq;

namespace Restorent_app.Models
{
    public class ReposetoryWating : IReposetoryWating
    {
        private readonly RestaurantDBContext dbContext;
        public ReposetoryWating(RestaurantDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool doWaiting(WaitingModel waiting)
        {
            try
            {
                waiting.PriorityTime = DateAndTime.Now;
                dbContext.Waiters.Add(waiting);
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public WaitingModel firstWaiter(int tableId, DateTime start, DateTime end)
        {
            if (isAnyOneWaits(tableId, start, end))
            {
                string query = "SELECT TOP 1 * FROM [Waiters] WHERE StartTime < @endtime AND EndTime > @startTime ORDER BY PriorityTime ASC ";
                DateTime startTime = start.AddMinutes(-5);
                DateTime endTime = start.AddMinutes(5);

                // Create SqlParameters with correct names
                SqlParameter sqlParameterforStartTime = new SqlParameter("@startTime", startTime);
                SqlParameter sqlParameterforEndTime = new SqlParameter("@endtime", endTime);

                // Fetch list of bookings that conflict with the new booking time
                WaitingModel firstWaiter = dbContext.Waiters.FromSqlRaw<WaitingModel>(query, sqlParameterforStartTime, sqlParameterforEndTime).AsNoTracking<WaitingModel>().FirstOrDefault<WaitingModel>();    
                                    
                return firstWaiter;
            }
            else
            {
                return null;
            }
        }

        public bool isAnyOneWaits(int tableId, DateTime start, DateTime end)
        {
            try
            {
                string query = "SELECT * FROM [Waiters] WHERE StartTime < @endtime AND EndTime > @startTime ORDER BY PriorityTime ASC ";
                DateTime startTime = start.AddMinutes(-5);
                DateTime endTime = start.AddMinutes(5);

                // Create SqlParameters with correct names
                SqlParameter sqlParameterforStartTime = new SqlParameter("@startTime", startTime);
                SqlParameter sqlParameterforEndTime = new SqlParameter("@endtime", endTime);

                // Fetch list of bookings that conflict with the new booking time
                List<WaitingModel> waiterList = dbContext.Waiters.FromSqlRaw<WaitingModel>(query, sqlParameterforStartTime, sqlParameterforEndTime).AsNoTracking<WaitingModel>().ToList<WaitingModel>();

                return waiterList.Count>0;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        
        }

        public bool removeWating(WaitingModel waiting)
        {
            return removeWatingbyWwatingId(waiting.WaitingId);
        }

        public bool removeWatingbyWwatingId(int watingId)
        {
            WaitingModel waitingModel = dbContext.Waiters.Find(watingId);
            if (waitingModel != null)
            {
                try
                {
                    dbContext.Waiters.Remove(waitingModel);
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
