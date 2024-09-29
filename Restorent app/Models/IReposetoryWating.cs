using Microsoft.VisualBasic;
using System;
namespace Restorent_app.Models
{
    public interface IReposetoryWating
    {
        public bool doWaiting(WaitingModel waiting);

        public bool removeWatingbyWwatingId(int watingId);

        public bool removeWating(WaitingModel waiting);

        public bool isAnyOneWaits(int tableId, DateTime start, DateTime end);

        public WaitingModel firstWaiter(int tableId, DateTime start, DateTime end);
    }
}
