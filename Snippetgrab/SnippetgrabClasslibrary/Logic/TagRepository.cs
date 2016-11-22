using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.ContextInterfaces;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.Logic
{
    public class TagRepository
    {
        private readonly ITagContext _context;

        public TagRepository(ITagContext context)
        {
            _context = context;
        }

        public bool AddTag(Tag tag)
        {
            return _context.AddTag(tag);
        }

        public List<Tag> GetAll()
        {
            return _context.GetAll();
        }

        public Tag GetTagById(int tagId)
        {
            return _context.GetTagById(tagId);
        }

        public Tag GetTagByString(string text)
        {
            return _context.GetTagByString(text);
        }

        public bool RemoveTag(int tagId)
        {
            return _context.RemoveTag(tagId);
        }
    }
}
