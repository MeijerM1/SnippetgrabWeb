using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Snippetgrab.Models;
using SnippetgrabClasslibrary.Data;
using SnippetgrabClasslibrary.Logic;
using SnippetgrabClasslibrary.Models;

namespace Snippetgrab.Controllers
{
    public class UserController : Controller
    {
        private UserRepository _userRepo = new UserRepository(new UserMsSqlContext());

        // GET: User
        [HttpGet]
        public ActionResult Index()
        {
            if ((int) Session["UserID"] == -1)
            {
                return RedirectToAction("Login", "User");
            }

            return View();
        }


        public ActionResult LogIn()
        {
            Session["UserID"] = -1;
            UserModel um = new UserModel();
            return View(um);
        }

        [HttpPost]
        public ActionResult Home(string email, string password)
        {
            UserModel um = new UserModel();
            if (_userRepo.CheckPassword(email, password))
            {
                User user = _userRepo.GetUserByEmail(email);
                um.ActiveUser = user;
                Session["UserID"] = um.ActiveUser.ID;
                return View(um);
            }
            else
            {
                ViewBag.ErrorMessage = "Incorrect username or password";
                return View("LogIn");
            }

        }

        public ActionResult Detail()
        {
            if ((int) Session["UserID"] == -1)
            {
                return RedirectToAction("Login", "User");
            }

            User userToDisplay = _userRepo.GetUserById((int) Session["userID"]);

            return View(userToDisplay);
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(string Email, string Name, string Password, string ConfirmPassword)
        {

            if (Password != ConfirmPassword)
            {
                ViewBag.ErrorMessage = "PAsswords do not match";
                return View("CreateAccount");
            }
            else if (_userRepo.GetUserByEmail(Email) != null)
            {
                ViewBag.ErrorMessage = "Email already in use";
                return View("CreateAccount");
            }
            else
            {
                DateTime today = DateTime.Today;
                List<int> tags = new List<int>();
                var newUser = new User(Name, today, 0, Email, false, tags);
                try
                {
                    _userRepo.AddUser(newUser, Password);
                }
                catch (Exception e )
                {
                    ViewBag.ErrorMessage = "Something went wrong: " + e.Message;
                    return View("CreateAccount");
                }
                return RedirectToAction("LogIn");
            }
        }
    }
}