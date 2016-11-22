using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.ContextInterfaces;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.Logic
{
    public class CommentRepository
    {
        private readonly ICommentContext _context;

        public CommentRepository(ICommentContext context)
        {
            _context = context;
        }

        public bool AddComment(Comment comment)
        {
            return _context.AddComment(comment);
        }

        public bool ChangePoint(int commentId, int increaseDecrease)
        {
            return _context.ChangePoint(commentId, increaseDecrease);
        }

        public List<Comment> GetAll()
        {
            return _context.GetAll();
        }

        public Comment GetCommentById(int commentId)
        {
            return _context.GetCommentById(commentId);
        }

        public List<Comment> GetCommentByProblem(int problemId)
        {
            return _context.GetCommentByProblem(problemId);
        }

        public List<Comment> GetCommentByUser(int userId)
        {
            return _context.GetCommentByUser(userId);
        }

        public List<Comment> GetRepliesByComment(int commentId)
        {
            return _context.GetRepliesByComment(commentId);
        }

        public bool RemoveComment(int commentId)
        {
            return _context.RemoveComment(commentId);
        }
    }
}
