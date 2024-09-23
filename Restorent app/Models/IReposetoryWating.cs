using Microsoft.VisualBasic;

namespace Restorent_app.Models
{
    public interface IReposetoryWating
    {
        public bool doWaiting(WaitingModel waiting);

        public bool removeWatingbyWwatingId(int watingId);

        public bool removeWating(WaitingModel waiting);

        public bool isAnyOneWaits(int tableId, DateAndTime start, DateAndTime end);

        public WaitingModel firstWaiter(int tableId, DateAndTime start, DateAndTime end);
    }
}
