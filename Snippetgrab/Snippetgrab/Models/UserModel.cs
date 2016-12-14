using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<User> Users { get; set; }
        public User ActiveUser { get; set; }
        public Dictionary<Message, User> MessagesButBetter { get; set; }

        public UserModel()
        {
            Users = new List<User>();
            Users = _userRepo.GetAll();

        }

        public void GetMessages()
        {
            MessagesButBetter = new Dictionary<Message, User>();
            var messages = new List<Message>();
            messages = _messageRepo.GetMostRecent(ActiveUser.ID);

            foreach (var message in messages)
            {
                var user  =_userRepo.GetUserById(message.SenderID);
                MessagesButBetter.Add(message, user);
            }                      
        }
    }
}