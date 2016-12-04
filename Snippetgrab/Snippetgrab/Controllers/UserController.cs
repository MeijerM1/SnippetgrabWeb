using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Snippetgrab.Models;
using SnippetgrabClasslibrary.Data;
using SnippetgrabClasslibrary.Logic;

namespace Snippetgrab.Controllers
{
    public class UserController : Controller
    {
        private UserRepository _userRepo = new UserRepository(new UserMsSqlContext());

        // GET: User
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Home()
        {
            UserModel um = new UserModel();
            return View(um);
        }

        /*[HttpPost]
        public ActionResult Home(string email, string password)
        {
            UserModel um = new UserModel();
            if (_userRepo.CheckPassword(email, password))
            {
                return View(um);
            }
            else
            {
                return Index();   
            }

        }*/
    }
}