using Microsoft.VisualBasic;

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
            throw new System.NotImplementedException();
        }

        public WaitingModel firstWaiter(int tableId, DateAndTime start, DateAndTime end)
        {
            throw new System.NotImplementedException();
        }

        public bool isAnyOneWaits(int tableId, DateAndTime start, DateAndTime end)
        {
            throw new System.NotImplementedException();
        }

        public bool removeWating(WaitingModel waiting)
        {
            throw new System.NotImplementedException();
        }

        public bool removeWatingbyWwatingId(int watingId)
        {
            throw new System.NotImplementedException();
        }
    }
}
