using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using SnippetgrabClasslibrary.Data;
using SnippetgrabClasslibrary.Logic;
using SnippetgrabClasslibrary.Models;

namespace Snippetgrab.Models
{
    public class UserModel
    {
        UserRepository _userRepo = new UserRepository(new UserMsSqlContext());
        MessageRepository _messageRepo = new MessageRepository(new MessageMsSqlContext());
        ProblemRepository _problemrepo = new ProblemRepository(new ProblemMsSqlContext());
        SnippetRepository _snippetRepo = new SnippetRepository(new SnippetMsSqlContext());

        public List<User> Users { get; set; }
        public User ActiveUser { get; set; }
        public Dictionary<Message, User> MessagesButBetter { get; set; }
        public Tuple<Message, User> MessageToDisplay { get; set; }
        public Dictionary<Problem, User> Problems { get; set; }
        public Dictionary<Snippet, User> Snippets { get; set; }

        public UserModel()
        {
            Users = new List<User>();
            Users = _userRepo.GetAll();
            GetRecentProblems();
            GetRecentSnippets();
        }

        public void GetMessages()
        {
            MessagesButBetter = new Dictionary<Message, User>();
            var messages = new List<Message>();
            messages = _messageRepo.GetMessageByUser(ActiveUser.ID);

            foreach (var message in messages)
            {
                var user  =_userRepo.GetUserById(message.SenderID);
                MessagesButBetter.Add(message, user);
            }                      
        }

        public void GetMessage(int messageId)
        {
            var message = _messageRepo.GetMessageByID(messageId);
            var user = _userRepo.GetUserById(message.SenderID);
            MessageToDisplay = new Tuple<Message,User>(message, user);
        }

        public void SetActiveUser(int id)
        {
            ActiveUser = _userRepo.GetUserById(id);
        }

        public void GetRecentProblems()
        {
            Problems = new Dictionary<Problem, User>();
            List<Problem> problem = new List<Problem>();
            problem = _problemrepo.GetMostRecent();
            foreach (var problem1 in problem)
            {
                var user = _userRepo.GetUserById(problem1.AuthorID);
                Problems.Add(problem1, user);
            }
        }

        public void GetRecentSnippets()
        {
            Snippets = new Dictionary<Snippet, User>();
            List<Snippet> snippet = new List<Snippet>();
            snippet = _snippetRepo.GetMostRecent();
            foreach (var snippet1 in snippet)
            {
                var user = _userRepo.GetUserById(snippet1.AuthorID);
                Snippets.Add(snippet1, user);
            }
        }
    }
}