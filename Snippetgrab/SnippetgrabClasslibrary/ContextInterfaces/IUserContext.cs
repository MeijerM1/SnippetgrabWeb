using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.ContextInterfaces
{
    public interface IUserContext
    {
        bool CheckPassword(string email, string password);

        bool AddUser(User user, string password);

        bool RemoveUser(int userId);

        User GetUserById(int id);

        List<User> GetAll();

        User GetUserByEmail(string email);

        bool ResetPassword(int userId, string password);

        bool SubScribeToTag(int userId, int tagId);

        bool UnSubscribeFromTag(int userId, int tagId);
    }
}
