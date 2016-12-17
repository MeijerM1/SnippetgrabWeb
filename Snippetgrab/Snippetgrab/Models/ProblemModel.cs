using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SnippetgrabClasslibrary.Data;
using SnippetgrabClasslibrary.Logic;
using SnippetgrabClasslibrary.Models;

namespace Snippetgrab.Models
{
    public class ProblemModel
    {
        ProblemRepository _problemRepo = new ProblemRepository(new ProblemMsSqlContext());
        UserRepository _userRepo = new UserRepository(new UserMsSqlContext());

        public Dictionary<Problem, User> Problems { get; set; }
        public Tuple<Problem, User> ProblemToDisplay { get; set; }

        public ProblemModel()
        {
            Problems = new Dictionary<Problem, User>();
            GetProblems();
        }

        private void GetProblems()
        {
            var problems = new List<Problem>();
            problems = _problemRepo.GetAll();

            foreach (var problem in problems)
            {
                var user = _userRepo.GetUserById(problem.AuthorID);
                Problems.Add(problem, user);
            }
        }

        public void Getproblem(int id)
        {
            var problem = _problemRepo.GetProblemById(id);
            var user = _userRepo.GetUserById(problem.AuthorID);

            ProblemToDisplay = new Tuple<Problem, User>(problem, user);
        }

        public void AddSnippet(Problem problemToAdd)
        {
            _problemRepo.AddProblem(problemToAdd);
        }
    }
}