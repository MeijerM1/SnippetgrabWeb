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

namespace TSqlDemo
{
    public partial class Form1 : Form
    {
        private const string SqlCon = @"Data Source=192.168.19.152,1433\\MSSQLSERVER; Network Library = DBMSSOCN; Initial Catalog = dbi356615; User ID=dbuser;Password=Wachtwoord1;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private TagRepository tagRepo = new TagRepository(new TagMsSqlContext());
        private UserRepository userRepo = new UserRepository(new UserMsSqlContext());


        public Form1()
        {
            InitializeComponent();
            //LoadTags();
        }

        private void Notify_Click(object sender, EventArgs e)
        {
            int userID = (int)nudUploader.Value;
            userRepo.ResetPassword(userID, "test");
        }

        private void LoadTags()
        {
            lbTags.Items.Clear();
            var tags = tagRepo.GetAll();

            foreach (var tag in tags)
            {
                lbTags.Items.Add(tag.ToString());
            }

            lbUsers.Items.Clear();
            var users = userRepo.GetAll();
            foreach (var user in users)
            {
                lbUsers.Items.Add(user.ToString());
            }

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
                    return (string)command.Parameters["@StringToReturn"].Value;
                }
            }
        }
    }
}
