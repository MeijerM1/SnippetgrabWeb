using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.ContextInterfaces
{
    public interface ICommentContext
    {
        bool AddComment(Comment comment);

        bool RemoveComment(int commentId);

        Comment GetCommentById(int commentId);

        List<Comment> GetAll();

        List<Comment> GetCommentByUser(int userId);

        List<Comment> GetCommentByProblem(int problemId);

        bool ChangePoint(int commentId, int increaseDecrease);

        List<Comment> GetRepliesByComment(int commentId);
    }
}
