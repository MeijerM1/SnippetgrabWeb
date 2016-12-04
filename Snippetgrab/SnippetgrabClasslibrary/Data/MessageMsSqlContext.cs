using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.ContextInterfaces;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.Data
{
    public class MessageMsSqlContext : IMessageContext
    {
        private const string SqlCon = @"Server = mssql.fhict.local; Database=dbi356615;User Id = dbi356615; Password=Kipgarfield1";

        public bool AddMessage(Message message)
        {
            var QueryString =
                "INSERT INTO [Message] (Text, SenderID, ReceipentID) VALUES (@text, @senderId, @receipentId)";

            using (var conn = new SqlConnection(SqlCon))
            {
                try
                {
                    using (var cmd1 = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd1.Parameters.AddWithValue("text", message.MessageText);
                        cmd1.Parameters.AddWithValue("senderId", message.SenderID);
                        cmd1.Parameters.AddWithValue("receipentId", message.ReceipentID);
                        cmd1.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<Message> GetMessageByUser(int id)
        {
            var getUserQueryString =
                "SELECT m.MessageID, m.Text, m.senderID, m.ReceipentID FROM [Message] as [m] WHERE m.ReceipentID = @id";

            var messages = new List<Message>();

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmdGetUser = new SqlCommand(getUserQueryString, conn))
                    {
                        conn.Open();
                        cmdGetUser.Parameters.AddWithValue("id", id);

                        using (var reader = cmdGetUser.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                messages.Add(CreateMessageFromReader(reader));                               
                            }
                            return messages;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool RemoveMessage(int id)
        {
            var QueryString = "DELETE * FROM Message WHERE MessageID=@id";

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmd = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Message CreateMessageFromReader(SqlDataReader reader)
        {
            return new Message(
                Convert.ToInt32(reader["MessageID"]),
                Convert.ToString(reader["Text"]),
                Convert.ToInt32(reader["SenderID"]),
                Convert.ToInt32(reader["ReceipentID"])
                );
        }
    }
}
