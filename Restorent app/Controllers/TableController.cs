using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Restorent_app.Models;

namespace Restorent_app.Controllers
{
    public class TableController : Controller
    {
        IReposetoryTable reposetoryTable;
        IReposetoryRestaurant reposetoryRestaurant;

        public TableController(IReposetoryTable reposetoryTable, IReposetoryRestaurant reposetoryRestaurant)
        {
            this.reposetoryTable = reposetoryTable;
            this.reposetoryRestaurant = reposetoryRestaurant;
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
        public IActionResult Edit(int id)
        {
            if (CheckForAuthenticate())
            {
                if (id <= 0)
                {
                    return NotFound(id);
                }
                else
                {
                    
                    TableModel tableModel = reposetoryTable.getTableModelByTableId(id);
                    tableModel.Restaurant = reposetoryRestaurant.getRestaurantModelById(tableModel.RestaurantId);
                    if (tableModel == null)
                    {
                        return NotFound(); // Return a not found result if the model is null
                    }
                    ViewBag.IsAuthenticate = true;
                    return View(tableModel);
                }
            }
            else
            {

                return RedirectToAction("Login", "Auth");
            }


            
        }
        [HttpPost]
        public IActionResult Edit(TableModel tableModel)
        {
            if (CheckForAuthenticate())
            {
                if (ModelState.IsValid)
                {
                    // Save the tableModel to the database
                    reposetoryTable.UpdateTableById(tableModel);
                    return RedirectToAction("ShowTables","Restaurant", new { id = tableModel.RestaurantId }); // Redirect to the list of tables
                }
                ViewBag.IsAuthenticate = true;
                // If model state is not valid, return the same view with the model to display errors
                return View(tableModel);
            }
            else
            {

                return RedirectToAction("Login", "Auth");
            }


        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (CheckForAuthenticate())
            {
                if (id <= 0)
                {
                    return NotFound(id);
                }

                // Get the table model
                TableModel tableModel = reposetoryTable.getTableModelByTableId(id);
                if (tableModel == null)
                {
                    return NotFound(); // Return a not found result if the model is null
                }

                // Load the associated restaurant
                tableModel.Restaurant = reposetoryRestaurant.getRestaurantModelById(tableModel.RestaurantId);

                ViewBag.IsAuthenticate = true;
                return View(tableModel);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(TableModel table)
        {
            if (CheckForAuthenticate())
            {
                TableModel tableModel = reposetoryTable.getTableModelByTableId(table.Tableid);
                if (tableModel == null)
                {
                    return NotFound(); // Return a not found result if the model is null
                }

                // Delete the table
                reposetoryTable.removeTable(tableModel); // Implement this method in your repository

                // Redirect to a confirmation page or back to the list of tables
                return RedirectToAction("ShowTables","Restaurant", new { Id = tableModel.RestaurantId });
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public IActionResult ShowTableList(int id)
        {
            if (CheckForAuthenticate())
            {
                if (id <= 0)
                {
                    return NotFound(id);
                }
                else
                {
                    // Get the list of tables for the specified restaurant
                    List<TableModel> tableModels = reposetoryTable.getTableModelsByRestaurantId(id);

                    // Check if there are any tables
                    if (tableModels == null || !tableModels.Any())
                    {
                        ViewBag.IsAuthenticate = true;
                        ViewBag.Message = "No tables found for this restaurant.";
                        return View(new List<TableModel>()); // Return an empty list to the view
                    }

                    ViewBag.IsAuthenticate = true;
                    return View(tableModels); // Pass the list of tables to the view
                }
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }


    }
}
