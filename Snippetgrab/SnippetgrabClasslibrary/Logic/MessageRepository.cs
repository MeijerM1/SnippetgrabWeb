using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.ContextInterfaces;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.Logic
{
    public class MessageRepository
    {
        private readonly IMessageContext _context;

        public MessageRepository(IMessageContext context)
        {
            _context = context;
        }

        public bool AddMessage(Message message)
        {
            return _context.AddMessage(message);
        }

        public List<Message> GetMessageByUser(int id)
        {
            return _context.GetMessageByUser(id);
        }

        public bool RemoveMessage(int id)
        {
            return _context.RemoveMessage(id);
        }

        public List<Message> GetMostRecent(int userId)
        {
            return _context.GetMostRecent(userId);
        }
    }
}
