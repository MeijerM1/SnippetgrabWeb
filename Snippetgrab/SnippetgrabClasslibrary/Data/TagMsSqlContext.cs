using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using SnippetgrabClasslibrary.ContextInterfaces;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.Data
{
    public class TagMsSqlContext : ITagContext
    {
        private readonly string _sqlCon = WebConfigurationManager.ConnectionStrings["ConnStringDbAzure"].ConnectionString;

        public bool AddTag(Tag tag)
        {
            var QueryString =
                "INSERT INTO [Tag] (Text) VALUES (@text)";

            using (var conn = new SqlConnection(_sqlCon))
            {
                try
                {
                    using (var cmd1 = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd1.Parameters.AddWithValue("text", tag.Text);
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

        public List<Tag> GetAll()
        {
            var getTagQueryString =
                "SELECT t.TagID, t.Text FROM [Tag] as [t]";

            var tags = new List<Tag>();

            try
            {
                using (var conn = new SqlConnection(_sqlCon))
                {
                    using (var cmdGetTag = new SqlCommand(getTagQueryString, conn))
                    {
                        conn.Open();

                        using (var reader = cmdGetTag.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tags.Add(CreateTagFromReader(reader));
                            }
                            return tags;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Tag GetTagById(int tagId)
        {
            var getTagQueryString =
                "SELECT t.TagID, t.Text FROM [Tag] as [t] WHERE t.TagID=@id";

            try
            {
                using (var conn = new SqlConnection(_sqlCon))
                {
                    using (var cmdGetTag = new SqlCommand(getTagQueryString, conn))
                    {
                        conn.Open();
                        cmdGetTag.Parameters.AddWithValue("id", tagId);
                        using (var reader = cmdGetTag.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var t = CreateTagFromReader(reader);
                                return t;
                            }
                        }
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Tag GetTagByString(string text)
        {
            var getTagQueryString =
                "SELECT t.TagID, t.Text FROM [Tag] as [t] WHERE t.Text=@text";

            try
            {
                using (var conn = new SqlConnection(_sqlCon))
                {
                    using (var cmdGetTag = new SqlCommand(getTagQueryString, conn))
                    {
                        conn.Open();
                        cmdGetTag.Parameters.AddWithValue("text", text);
                        using (var reader = cmdGetTag.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var t = CreateTagFromReader(reader);
                                return t;
                            }
                        }
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool RemoveTag(int tagId)
        {
            var QueryString = "DELETE * FROM Tag WHERE TagID=@id";

            try
            {
                using (var conn = new SqlConnection(_sqlCon))
                {
                    using (var cmd = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("id", tagId);
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

        public Tag CreateTagFromReader(SqlDataReader reader)
        {
            return new Tag(
                Convert.ToInt32(reader["TagID"]),
                Convert.ToString(reader["Text"])         
              );
        }
            
    }
}
