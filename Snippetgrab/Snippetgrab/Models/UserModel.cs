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

        public List<User> Users { get; set; }
        public User ActiveUser { get; set; }


        public UserModel()
        {
            Users = new List<User>();
            Users = _userRepo.GetAll();
        }
    }
}