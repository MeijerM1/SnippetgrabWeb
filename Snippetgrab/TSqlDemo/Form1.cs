using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnippetgrabClasslibrary.Data;
using SnippetgrabClasslibrary.Logic;
using SnippetgrabClasslibrary.Models;
using Message = SnippetgrabClasslibrary.Models.Message;

namespace TSqlDemo
{
    public partial class Form1 : Form
    {
        private const string SqlCon =
            @"Data Source=192.168.19.152,1433\\MSSQLSERVER; Network Library = DBMSSOCN; Initial Catalog = dbi356615; User ID=dbuser;Password=Wachtwoord1;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private TagRepository tagRepo = new TagRepository(new TagMsSqlContext());
        private UserRepository userRepo = new UserRepository(new UserMsSqlContext());
        private MessageRepository messageRepository = new MessageRepository(new MessageMsSqlContext());


        public Form1()
        {
            InitializeComponent();
            //LoadTags();
        }

        private void NotifyCommand(int uploaderID, int tagID, string link)
        {
            using (var conn = new SqlConnection(SqlCon))
            {
                using (var command = new SqlCommand("dbo.Notify", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UploaderID", uploaderID);
                    command.Parameters.Add(new SqlParameter("@ParaTagID", tagID));
                    command.Parameters.Add(new SqlParameter("@Link", link));
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private string RemoveWords(string stringToClean)
        {
            using (var conn = new SqlConnection(SqlCon))
            {
                using (var command = new SqlCommand("dbo.CheckWordsToRemove", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StringToCheck", stringToClean);
                    command.Parameters.AddWithValue("@CurrentWord", "");
                    SqlParameter retval = command.Parameters.Add("@StringToReturn", SqlDbType.NVarChar);
                    retval.Direction = ParameterDirection.Output;
                    retval.Size = 50;
                    command.ExecuteNonQuery();
                    return (string) command.Parameters["@StringToReturn"].Value;
                }
            }
        }

        private void btnGetOffenders_Click(object sender, EventArgs e)
        {
            lbUser.Items.Clear();
            lbMessage.Items.Clear();

            List<User> users = new List<User>();
            users = GetOffenders();

            foreach (var user in users)
            {
                lbUser.Items.Add(user);
            }
        }

        private List<User> GetOffenders()
        {
            List<User> results = new List<User>();

            using (var conn = new SqlConnection(SqlCon))
            {
                using (var command = new SqlCommand("dbo.CheckUserBannedWord", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                          results.Add(userRepo.GetUserById(Convert.ToInt32(reader["SenderID"]))); 
                        }
                        return results;
                    }
                }
            }
        }

        private List<Message> GetLastMessages(int UserID, int amountOfMessages)
        {
            List<Message> results = new List<Message>();

            using (var conn = new SqlConnection(SqlCon))
            {
                using (var command = new SqlCommand("dbo.GetLastMessages", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@AmountOfMessages", amountOfMessages);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(messageRepository.GetMessageByID(Convert.ToInt32(reader["MessageID"])));
                        }
                        return results;
                    }
                }
            }
        }

        private void lbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbMessage.Items.Clear();

            var user = (SnippetgrabClasslibrary.Models.User) lbUser.SelectedItem;
            if (user.ID > -1)
            {
                List<Message> messages = GetLastMessages(user.ID, 3);
                foreach (var message in messages)
                {
                    lbMessage.Items.Add(message);
                }
            }
        }

        private void ApproveMessage(int messageId)
        {
            using (var conn = new SqlConnection(SqlCon))
            {
                using (var command = new SqlCommand("dbo.ApproveMessage", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MessageID", messageId);
                    command.ExecuteNonQuery();
                }
            }

            lbUser.Items.Clear();

            List<User> users = new List<User>();
            users = GetOffenders();

            foreach (var user in users)
            {
                lbUser.Items.Add(user);
            }

            lbMessage.Items.Clear();

            if (lbUser.SelectedIndex < 0) return;

            var sUser = (User) lbUser.SelectedItem;
            var messages = GetLastMessages(sUser.ID, 3);
            foreach (var message in messages)
            {
                lbMessage.Items.Add(message);
            }
        }

        private void btnMarkApproved_Click(object sender, EventArgs e)
        {
            if (lbMessage.SelectedIndex < 0)
            {
                return;
            }

            var Message = (Message) lbMessage.SelectedItem;
            ApproveMessage(Message.ID);
        }

        private void btnSendWarningMessage_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                return;
            }

            using (var conn = new SqlConnection(SqlCon))
            {
                using (var command = new SqlCommand("dbo.SendOffenderMessage", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@WarningMessage", textBox1.Text);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
