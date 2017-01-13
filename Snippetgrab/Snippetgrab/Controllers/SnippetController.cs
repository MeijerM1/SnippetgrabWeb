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
    
    public class SnippetController : Controller
    {
        private readonly TagRepository _tagRepo = new TagRepository(new TagMsSqlContext());
        private readonly SnippetRepository _snippetRepo = new SnippetRepository(new SnippetMsSqlContext());

        [HttpGet]
        public ActionResult Index()
        {
            if ((int)Session["UserID"] == -1)
            {
                return RedirectToAction("Login", "User");
            }

            SnippetModel sm = new SnippetModel();
            return View(sm);
        }

        [HttpGet]
        public ActionResult Add()
        {
            if ((int)Session["UserID"] == -1)
            {
                return RedirectToAction("Login", "User");
            }

            Snippet s = new Snippet();
            s.Tags = _tagRepo.GetAll();
            return View(s);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Snippet newSnippet, FormCollection form)
        {
            SnippetModel sm = new SnippetModel();

            if (ModelState.IsValid)
            {
                newSnippet.AuthorID = (int)Session["UserID"];
                newSnippet.Points = 1;
                var AllStrings = form["checkboxTag"].Split(',');
                foreach (string item in AllStrings)
                {
                    int value = int.Parse(item);
                    var tag = _tagRepo.GetTagById(value);
                    newSnippet.Tags.Add(tag);
                }
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
            if ((int)Session["UserID"] == -1)
            {
                return RedirectToAction("Login", "User");
            }

            SnippetModel sm = new SnippetModel();
            sm.GetSnippet(id);
            return View(sm);
        }

        [HttpPost]
        public ActionResult ChangePoint(int snippetId, string point)
        {
            switch (point)
            {
                case "+":
                    _snippetRepo.ChangePoint(snippetId, 1);
                    break;
                case "-":
                    _snippetRepo.ChangePoint(snippetId, 0);
                    break;
                default:
                    return RedirectToAction("Detail", new { id = snippetId });
                    break;
            }

            return RedirectToAction("Detail", new { id = snippetId });
        }
    }
}