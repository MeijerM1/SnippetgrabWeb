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
        TagRepository _tagRepo = new TagRepository(new TagMsSqlContext());

        [HttpGet]
        public ActionResult Index()
        {
            SnippetModel sm = new SnippetModel();
            return View(sm);
        }

        [HttpGet]
        public ActionResult Add()
        {
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
            SnippetModel sm = new SnippetModel();
            sm.GetSnippet(id);
            return View(sm);
        }            
    }
}