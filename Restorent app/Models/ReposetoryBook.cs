using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Restorent_app.Models
{
    public class ReposetoryBook : IReposetoryBook
    {
        private readonly RestaurantDBContext dbContext;

        public List<KeyValuePair<DateAndTime, DateAndTime>> availibility(int tableId, DateAndTime Date)
        {
            throw new System.NotImplementedException();
        }

        public bool bookTable(BookModel book)
        {
            throw new System.NotImplementedException();
        }

        public bool cancelBook(BookModel book)
        {
            throw new System.NotImplementedException();
        }

        public bool isAvailable(BookModel book)
        {
            throw new System.NotImplementedException();
        }
    }
}
