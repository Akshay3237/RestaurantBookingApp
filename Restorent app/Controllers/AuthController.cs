using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restorent_app.Models;
using Restorent_app.ViewModel;

namespace Restorent_app.Controllers
{
    public class AuthController : Controller
    {
        private readonly IReposetoryUser reposetoryUser;

        public AuthController(IReposetoryUser reposetoryUser)
        {
            this.reposetoryUser = reposetoryUser;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Render the login view with an empty model
            return View(new LogInViewModel());
        }

        [HttpPost]
        public IActionResult Login(LogInViewModel logInViewModel)
        {
            // Check if the model state is valid (based on model validations, if any)
            if (ModelState.IsValid)
            {
                string userName = logInViewModel.userName;
                string password = logInViewModel.password;
                Console.WriteLine(userName);
                // Check if the user exists with the provided username and password
                if (reposetoryUser.findUser(userName, password))
                {
                    // Create a session for the logged-in user
                    HttpContext.Session.SetString("UserName", userName);

                    // Redirect to the dashboard or home page
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    // If login fails, set an error message to display in the view
                    ViewBag.ErrorMessage = "Invalid username or password.";
                }
            }

            // If the model state is not valid or login fails, reload the view with errors
            return View(logInViewModel);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Clear the session on logout
            HttpContext.Session.Clear();

            // Redirect to the login page
            return RedirectToAction("Login", "Auth");
        }
    }
}
