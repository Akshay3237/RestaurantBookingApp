using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Restorent_app.Models
{
    public class ReposetoryTable : IReposetoryTable
    {
        private readonly RestaurantDBContext dbContext;
        public ReposetoryTable(RestaurantDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public TableModel addTable(TableModel table)
        {
            try
            {
                dbContext.Tables.Add(table);
                dbContext.SaveChanges();
                return table;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            throw new System.NotImplementedException();
        }

        public TableModel getTableModelByTableId(int id)
        {
            return dbContext.Tables.Find(id);
        }

        public List<TableModel> getTableModels()
        {
            return dbContext.Tables.AsNoTracking().ToList();
        }

        public List<TableModel> getTableModelsByRestaurantId(int id)
        {
            string query = "SELECT * FROM [Tables] where RestaurantId=@restaurantId";
            SqlParameter sqlParameter = new SqlParameter("RestaurantId", id);
            return dbContext.Tables.FromSqlRaw(query, sqlParameter).AsNoTracking().ToList();
           
        }

        public TableModel removeTable(TableModel table)
        {
            try
            {
                dbContext.Tables.Remove(table);
                
                dbContext.SaveChanges();
                return table;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public bool UpdateTableById(TableModel table)
        {
            var existingTable = dbContext.Tables.Find(table.Tableid);

            // Step 2: Check if the table exists
            if (existingTable != null)
            {
                // Step 3: Update the properties of the existing table with new values from the passed `table` object
                existingTable.Capacity = table.Capacity;
                existingTable.NumberOfTable = table.NumberOfTable;
                existingTable.RestaurantId = table.RestaurantId;

                // Step 4: Save the changes to the database
                dbContext.SaveChanges();

                // Step 5: Return true to indicate that the update was successful
                return true;
            }

            // If the table with the given ID doesn't exist, return false
            return false;
        }
    }
}
