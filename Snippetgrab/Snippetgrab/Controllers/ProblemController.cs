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
        private readonly TagRepository _tagRepo = new TagRepository(new TagMsSqlContext());
        private readonly CommentRepository _commentRepo = new CommentRepository(new CommentMsSqlContext());
        private readonly ProblemRepository _problemRepository = new ProblemRepository(new ProblemMsSqlContext());

        // GET: Problem
        public ActionResult Index()
        {
            if ((int)Session["UserID"] == -1)
            {
                return RedirectToAction("Login", "User");
            }

            ProblemModel pm = new ProblemModel();
            return View(pm);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            if ((int)Session["UserID"] == -1)
            {
                return RedirectToAction("Login", "User");
            }

            ProblemModel pm = new ProblemModel();
            pm.Getproblem(id);
            return View(pm);
        }

        public ActionResult Add()
        {
            if ((int)Session["UserID"] == -1)
            {
                return RedirectToAction("Login", "User");
            }

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

        [HttpPost]
        public ActionResult PostComment(int problemId, string commentText)
        {
            var userId = (int)Session["UserId"];
            Comment newComment = new Comment(commentText, 0, userId, problemId);
            _commentRepo.AddComment(newComment);
            ProblemModel pm = new ProblemModel();
            pm.Getproblem(problemId);
            return RedirectToAction("Detail", new { id = problemId });
        }

        [HttpPost]
        public ActionResult ChangePointProblem(int problemId, string point)
        {
            switch (point)
            {
                case "+":
                    _problemRepository.ChangePoint(problemId, 1);
                    break;
                case "-":
                    _problemRepository.ChangePoint(problemId, 0);
                    break;
                default:
                    return RedirectToAction("Detail", new { id = problemId });
            }
            
            return RedirectToAction("Detail", new { id = problemId });
        }

        public ActionResult ChangePointComment(int problemId, string point, int commentId)
        {
            switch (point)
            {
                case "+":
                    _commentRepo.ChangePoint(commentId, 1);
                    break;
                case "-":
                    _commentRepo.ChangePoint(commentId, 0);
                    break;
                default:
                    return RedirectToAction("Detail", new { id = problemId });
            }

            return RedirectToAction("Detail", new { id = problemId });
        }
    }
}