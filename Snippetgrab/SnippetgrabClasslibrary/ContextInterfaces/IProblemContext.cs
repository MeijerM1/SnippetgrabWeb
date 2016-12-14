using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.ContextInterfaces
{
    public interface IProblemContext
    {
        bool AddProblem(Problem problem);

        bool RemoveProblem(int problemId);

        Problem GetProblemById(int problemId);

        List<Problem> GetAll();

        List<Problem> GetProblemByTag(int tagId);

        List<Problem> GetProblemByUser(int userId);

        bool ChangeIsSolved(int problemId, bool isSolved);

        bool ChangePoint(int problemId, int increaseDecrease);

        List<Problem> GetMostRecent();
    }
}
