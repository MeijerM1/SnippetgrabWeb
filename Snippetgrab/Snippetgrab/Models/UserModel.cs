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

        public List<Message> Messages = new List<Message>();


        public UserModel()
        {
            Users = new List<User>();
            Users = _userRepo.GetAll();

        }

        public void GetMessages()
        {
            Messages = new List<Message>();
            Messages = _messageRepo.GetMessageByUser(ActiveUser.ID);
        }
    }
}