using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SnippetgrabClasslibrary.Data;
using SnippetgrabClasslibrary.Logic;
using SnippetgrabClasslibrary.Models;

namespace Snippetgrab.Models
{
    public class SnippetModel
    {
        SnippetRepository _snippetRepo = new SnippetRepository(new SnippetMsSqlContext());
        UserRepository _userRepo = new UserRepository(new UserMsSqlContext());

        public Dictionary<Snippet, User> Snippets { get; set; }
        public Tuple<Snippet, User> SnippetToDisplay { get; set; }

        public SnippetModel()
        {
            Snippets = new Dictionary<Snippet, User>();
            GetSnippets();
        }

        private void GetSnippets()
        {
            var snippets = new List<Snippet>();
            snippets = _snippetRepo.GetAll();

            foreach (var snippet in snippets)
            {
                var user = _userRepo.GetUserById(snippet.AuthorID);
                Snippets.Add(snippet, user);
            }
        }

        public void GetSnippet(int id)
        {            
            var snippet = _snippetRepo.GetSnippetById(id);
            var user = _userRepo.GetUserById(snippet.AuthorID);

            SnippetToDisplay = new Tuple<Snippet, User>(snippet, user);
        }

        public void AddSnippet(Snippet snippetToAdd)
        {
            _snippetRepo.AddSnippet(snippetToAdd);
        }  
    }
}