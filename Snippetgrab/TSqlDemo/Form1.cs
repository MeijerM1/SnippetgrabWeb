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

namespace TSqlDemo
{
    public partial class Form1 : Form
    {
        private const string SqlCon = @"Server = mssql.fhict.local; Database=dbi356615;User Id = dbi356615; Password=Kipgarfield1";

        private TagRepository tagRepo = new TagRepository(new TagMsSqlContext());
        private UserRepository userRepo = new UserRepository(new UserMsSqlContext());


        public Form1()
        {
            InitializeComponent();
            LoadTags();
        }

        private void Notify_Click(object sender, EventArgs e)
        {
            NotifyCommand(Convert.ToInt32(nudUploader.Text), lbTags.SelectedIndex, tbLink.Text);
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
    }
}
