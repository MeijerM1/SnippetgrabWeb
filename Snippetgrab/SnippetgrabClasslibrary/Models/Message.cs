using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetgrabClasslibrary.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string MessageText { get; set; }
        public int SenderID { get; set; }
        public int ReceipentID { get; set; }

        public Message(int id, string messageText, int senderId, int receipentId)
        {
            ID = id;
            MessageText = messageText;
            SenderID = senderId;
            ReceipentID = receipentId;
        }

        public Message(string messageText, int senderId, int receipentId)
        {
            MessageText = messageText;
            SenderID = senderId;
            ReceipentID = receipentId;
        }

        public override string ToString()
        {
            return MessageText;
        }
    }
}
