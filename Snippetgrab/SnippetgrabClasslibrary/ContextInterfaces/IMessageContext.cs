using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.ContextInterfaces
{
    public interface IMessageContext
    {
        bool AddMessage(Message message);

        bool RemoveMessage(int id);

        List<Message> GetMessageByUser(int id);

        List<Message> GetMostRecent(int userId);
    }
}
