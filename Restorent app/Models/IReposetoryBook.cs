using System.Collections.Generic;
using Microsoft.VisualBasic;
using System;
namespace Restorent_app.Models
{
    public interface IReposetoryBook
    {
        //write logic for step follow
        //1)check isAvailablity of booking and if it is available then create one book entry in db
        //if bookTable gives true it means table booked but if it false means table does not book
        public bool bookTable(BookModel book);
        //check table availibility
        //1)do search tableid in bookmodel,and capacity of that table is 
        //2)if all number of tables are booked then check time slots of that all table and match with and if it not satisfy then return false.
        public bool isAvailable(BookModel book);
      
        public bool cancelBook(BookModel book);

        //by tableId user can find availibilty of table at any date.
        public List<KeyValuePair<DateTime, DateTime>> availibility(int tableId,DateTime Date);
    }
}
