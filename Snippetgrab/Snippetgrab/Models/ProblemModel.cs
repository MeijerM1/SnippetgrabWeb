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
        CommentRepository _commentRepo = new CommentRepository(new CommentMsSqlContext());

        public Dictionary<Problem, User> Problems { get; set; }
        public Tuple<Problem, User> ProblemToDisplay { get; set; }
        public Dictionary<Comment, User> Comments { get; set; }

        public ProblemModel()
        {
            Problems = new Dictionary<Problem, User>();
            Comments = new Dictionary<Comment, User>();
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

        private void GetComments(int problemId)
        {
            var comments = new List<Comment>();
            comments = _commentRepo.GetCommentByProblem(problemId);

            foreach (var comment in comments)
            {
                var user = _userRepo.GetUserById(comment.AuthorID);
                Comments.Add(comment, user);
            }
        }

        public void Getproblem(int id)
        {
            var problem = _problemRepo.GetProblemById(id);
            var user = _userRepo.GetUserById(problem.AuthorID);

            ProblemToDisplay = new Tuple<Problem, User>(problem, user);

            GetComments(id);
        }

        public void AddProblem(Problem problemToAdd)
        {
            _problemRepo.AddProblem(problemToAdd);
        }
    }
}