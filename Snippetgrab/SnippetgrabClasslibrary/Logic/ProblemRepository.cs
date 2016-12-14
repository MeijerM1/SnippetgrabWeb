using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.ContextInterfaces;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.Logic
{
    public class ProblemRepository
    {
        private readonly IProblemContext _context;

        public ProblemRepository(IProblemContext context)
        {
            _context = context;
        }

        public bool AddProblem(Problem problem)
        {
            return _context.AddProblem(problem);
        }

        public bool ChangeIsSolved(int problemId, bool isSolved)
        {
            return _context.ChangeIsSolved(problemId, isSolved);
        }

        public bool ChangePoint(int problemId, int increaseDecrease)
        {
            return _context.ChangePoint(problemId, increaseDecrease);
        }

        public List<Problem> GetAll()
        {
            return _context.GetAll();
        }

        public Problem GetProblemById(int problemId)
        {
            return _context.GetProblemById(problemId);
        }

        public List<Problem> GetProblemByTag(int tagId)
        {
            return _context.GetProblemByTag(tagId);
        }

        public List<Problem> GetProblemByUser(int userId)
        {
            return _context.GetProblemByUser(userId);
        }

        public bool RemoveProblem(int problemId)
        {
            return _context.RemoveProblem(problemId);
        }

        public List<Problem> GetMostRecent()
        {
            return _context.GetMostRecent();
        }
    }
}
