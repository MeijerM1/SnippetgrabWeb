using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.ContextInterfaces
{
    public interface ITagContext
    {
        bool AddTag(Tag tag);

        bool RemoveTag(int tagId);

        Tag GetTagById(int tagId);

        Tag GetTagByString(string text);

        List<Tag> GetAll();
    }
}
