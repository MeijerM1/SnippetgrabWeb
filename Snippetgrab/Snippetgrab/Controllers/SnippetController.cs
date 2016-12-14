using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippetgrab.Models;
using SnippetgrabClasslibrary.Models;

namespace Snippetgrab.Controllers
{
    public class SnippetController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            SnippetModel sm = new SnippetModel();
            return View(sm);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Snippet newSnippet)
        {
            SnippetModel sm = new SnippetModel();


            if (ModelState.IsValid)
            {
                newSnippet.AuthorID = (int)Session["UserID"];
                newSnippet.Points = 1;
                sm.AddSnippet(newSnippet);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            SnippetModel sm = new SnippetModel();
            sm.GetSnippet(id);
            return View(sm);
        }            
    }
}