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
    public class ProblemController : Controller
    {
        TagRepository _tagRepo = new TagRepository(new TagMsSqlContext());

        // GET: Problem
        public ActionResult Index()
        {
            ProblemModel pm = new ProblemModel();
            return View(pm);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            ProblemModel pm = new ProblemModel();
            pm.Getproblem(id);
            return View(pm);
        }

        public ActionResult Add()
        {
            Problem p = new Problem();
            p.Tags = _tagRepo.GetAll();
            return View(p);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Problem newProblem, FormCollection form)
        {
            ProblemModel pm = new ProblemModel();

            if (ModelState.IsValid)
            {
                newProblem.AuthorID = (int)Session["UserID"];
                newProblem.Points = 1;
                newProblem.IsSolved = false;
                var AllStrings = form["checkboxTag"].Split(',');
                foreach (string item in AllStrings)
                {
                    int value = int.Parse(item);
                    var tag = _tagRepo.GetTagById(value);
                    newProblem.Tags.Add(tag);
                }
                pm.AddProblem(newProblem);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}