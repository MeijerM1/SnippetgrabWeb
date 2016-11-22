using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.ContextInterfaces;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.Logic
{
    public class SnippetRepository
    {
        private readonly ISnippetContext _context;

        public SnippetRepository(ISnippetContext context)
        {
            _context = context;
        }

        public bool AddSnippet(Snippet snippet)
        {
            return _context.AddSnippet(snippet);
        }

        public bool ChangePoint(int snippetId, int increaseDecrease)
        {
            return _context.ChangePoint(snippetId, increaseDecrease);
        }

        public bool ChangePrivateModifier(int snippetId, bool isPrivate)
        {
            return _context.ChangePrivateModifier(snippetId, isPrivate);
        }

        public List<Snippet> GetAll()
        {
            return _context.GetAll();
        }

        public Snippet GetSnippetById(int snippetId)
        {
            return _context.GetSnippetById(snippetId);
        }

        public List<Snippet> GetSnippetByTag(int tagId)
        {
            return _context.GetSnippetByTag(tagId);
        }

        public List<Snippet> GetSnippetByUser(int userId)
        {
            return _context.GetSnippetByUser(userId);
        }

        public bool RemoveSnippet(int id)
        {
            return _context.RemoveSnippet(id);
        }
    }
}
