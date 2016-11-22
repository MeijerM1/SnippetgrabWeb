using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.ContextInterfaces;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.Logic
{
    public class UserRepository
    {
        private readonly IUserContext _context;

        public UserRepository(IUserContext context)
        {
            _context = context;
        }

        public bool AddUser(User user, string password)
        {
            return _context.AddUser(user, password);
        }

        public bool CheckPassword(string email, string password)
        {
            return _context.CheckPassword(email, password);
        }

        public List<User> GetAll()
        {
            return _context.GetAll();
        }

        public User GetUserByEmail(string email)
        {
            return _context.GetUserByEmail(email);
        }

        public User GetUserById(int id)
        {
            return _context.GetUserById(id);
        }

        public bool RemoveUser(int userId)
        {
            return _context.RemoveUser(userId);
        }

        public bool ResetPassword(int userId, string password)
        {
            return _context.ResetPassword(userId, password);
        }

        public bool SubScribeToTag(int userId, int tagId)
        {
            return _context.SubScribeToTag(userId, tagId);
        }

        public bool UnSubscribeFromTag(int userId, int tagId)
        {
            return _context.UnSubscribeFromTag(userId, tagId);
        }
    }
}
