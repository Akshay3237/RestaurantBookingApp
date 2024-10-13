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

        // Login GET action
        [HttpGet]
        public IActionResult Login()
        {
            // Render the login view with an empty model
            return View(new LogInViewModel());
        }

        // Login POST action
        [HttpPost]
        public IActionResult Login(LogInViewModel logInViewModel)
        {
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

            // Reload the view with errors if login fails
            return View(logInViewModel);
        }

        // Register GET action
        [HttpGet]
        public IActionResult Register()
        {
            // Return an empty UserModel to the registration form
            return View(new UserModel());
        }

        // Register POST action
        [HttpPost]
        public IActionResult Register(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists (based on email, for example)
                if (reposetoryUser.UserExists(user.Email))
                {
                    // Set an error message if the user already exists
                    ViewBag.ErrorMessage = "A user with this email already exists.";
                    return View(user);
                }

                // Save the new user using the repository
                reposetoryUser.createUser(user);

                // Optionally, create a session or redirect to the login page
                HttpContext.Session.SetString("UserName", user.UserName);

                // Redirect to login or home after successful registration
                return RedirectToAction("Login", "Auth");
            }

            // If the model state is not valid, reload the view with validation errors
            return View(user);
        }

        // Logout action
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
