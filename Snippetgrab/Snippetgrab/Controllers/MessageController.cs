using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippetgrab.Models;
using SnippetgrabClasslibrary.Data;
using SnippetgrabClasslibrary.Logic;
using SnippetgrabClasslibrary.Models;

namespace Snippetgrab.Controllers
{
    public class MessageController : Controller
    {
        UserRepository _userRepo = new UserRepository(new UserMsSqlContext());
        MessageRepository _messageRepo = new MessageRepository(new MessageMsSqlContext());

        // GET: Message
        public ActionResult Index()
        {
            if ((int) Session["UserID"] == -1)
            {
                return RedirectToAction("Login", "User");
            }

            var userId = (int) Session["UserId"];
            UserModel um = new UserModel();
            um.SetActiveUser(userId);
            um.GetMessages();
            return View(um);
        }

        public ActionResult Detail(int MessageId)
        {
            if ((int)Session["UserID"] == -1)
            {
                return RedirectToAction("Login", "User");
            }

            UserModel um = new UserModel();
            um.GetMessage(MessageId);
            return View(um);
        }

        public ActionResult Add()
        {
            if ((int)Session["UserID"] == -1)
            {
                return RedirectToAction("Login", "User");
            }

            UserModel um = new UserModel();        
            um.GetAllsUser();    
            return View(um);
        }

        [HttpPost]
        public ActionResult Add(string MessageText, string Receipent)
        {
            var userId = (int) Session["UserId"];
            var receipent = _userRepo.GetUserByEmail(Receipent);
            var newMessage = new Message(MessageText, userId, receipent.ID);
            _messageRepo.AddMessage(newMessage);
            return RedirectToAction("Index");
        }

    }
}