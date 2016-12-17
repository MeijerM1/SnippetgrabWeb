using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippetgrab.Models;

namespace Snippetgrab.Controllers
{
    public class ProblemController : Controller
    {
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
    }
}