using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.ContextInterfaces
{
    public interface ISnippetContext
    {
        bool AddSnippet(Snippet snippet);

        bool RemoveSnippet(int id);

        Snippet GetSnippetById(int snippetId);

        List<Snippet> GetSnippetByUser(int userId);

        List<Snippet> GetAll();

        List<Snippet> GetSnippetByTag(int tagId);

        bool ChangePrivateModifier(int snippetId, bool isPrivate);

        bool ChangePoint(int snippetId, int increaseDecrease);

        List<Snippet> GetMostRecent();
    }
}
