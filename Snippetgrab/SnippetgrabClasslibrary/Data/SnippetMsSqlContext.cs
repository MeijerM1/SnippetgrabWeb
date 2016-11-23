using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.ContextInterfaces;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.Data
{
    public class SnippetMsSqlContext : ISnippetContext
    {
        private const string SqlCon = @"Data Source = (LocalDB)\MSSQLLocalDB;" +
                      @"AttachDbFilename=|DataDirectory|\Snippetgrab.mdf;" +
                      "Integrated Security = True;" +
                      "Connect Timeout = 30";

        public bool ChangePoint(int snippetId, int increaseDecrease)
        {
            using (var conn = new SqlConnection(SqlCon))
            {
                using (var command = new SqlCommand("dbo.ChangePoint", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ItemID", snippetId);
                    command.Parameters.Add(new SqlParameter("@IncreaseDecrease", increaseDecrease));
                    command.Parameters.Add(new SqlParameter("@TypeToIncrease", "Snippet"));
                    conn.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool AddSnippet(Snippet snippet)
        {
            var QueryString =
                "INSERT INTO [Snippet] (Code, Points, IsPrivate, AuthorID) VALUES (@code, @points, @isPrivate, @authorId)";

            using (var conn = new SqlConnection(SqlCon))
            {
                try
                {
                    using (var cmd1 = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd1.Parameters.AddWithValue("code", snippet.Code);
                        cmd1.Parameters.AddWithValue("points", snippet.Points);
                        cmd1.Parameters.AddWithValue("isPrivate", snippet.IsPrivate);
                        cmd1.Parameters.AddWithValue("authorId", snippet.AuthorID);
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

        public bool ChangePrivateModifier(int snippetId, bool isPrivate)
        {
            var QueryString = "UPDATE Snippet SET IsPrivate=@isPrivate WHERE SnippetID=id";

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmd = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("id", snippetId);
                        cmd.Parameters.AddWithValue("isPrivate", Convert.ToInt32(isPrivate));

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

        public List<Snippet> GetAll()
        {
            var getUserQueryString =
                "SELECT s.SnippetID, s.Code, s.Points, s.IsPrivate, s.AuthorID FROM [Snippet] as [s]";

            var snippets = new List<Snippet>();

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmdGetUser = new SqlCommand(getUserQueryString, conn))
                    {
                        conn.Open();
                        using (var reader = cmdGetUser.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                snippets.Add(CreateSnippetFromReader(reader));
                            }
                            return snippets;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Snippet GetSnippetById(int snippetId)
        {
            var getUserQueryString =
                "SELECT s.SnippetID, s.Code, s.Points, s.IsPrivate, s.AuthorID FROM [Snippet] as [s] WHERE s.SnippetID=@id";

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmdGetUser = new SqlCommand(getUserQueryString, conn))
                    {
                        conn.Open();
                        cmdGetUser.Parameters.AddWithValue("id", snippetId);
                        using (var reader = cmdGetUser.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return CreateSnippetFromReader(reader);
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

        public List<Snippet> GetSnippetByTag(int tagId)
        {
            var getUserQueryString =
                "SELECT s.SnippetID, s.Code, s.Points, s.IsPrivate, s.AuthorID FROM [Snippet] as [s] JOIN [Tag_Snippet] as [ts] ON ts.SnippetID = s.SnippetID WHERE ts.TagID=@id";

            var snippets = new List<Snippet>();

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmdGetUser = new SqlCommand(getUserQueryString, conn))
                    {
                        conn.Open();
                        cmdGetUser.Parameters.AddWithValue("id", tagId);
                        using (var reader = cmdGetUser.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                snippets.Add(CreateSnippetFromReader(reader));
                            }
                            return snippets;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<Snippet> GetSnippetByUser(int userId)
        {
            var getUserQueryString =
                "SELECT s.SnippetID, s.Code, s.Points, s.IsPrivate, s.AuthorID FROM [Snippet] as [s] WHERE s.AuthorID=@id";

            var snippets = new List<Snippet>();

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmdGetUser = new SqlCommand(getUserQueryString, conn))
                    {
                        conn.Open();
                        cmdGetUser.Parameters.AddWithValue("id", userId);
                        using (var reader = cmdGetUser.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                snippets.Add(CreateSnippetFromReader(reader));
                            }
                            return snippets;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool RemoveSnippet(int id)
        {
            var QueryString = "DELETE * FROM Snippet WHERE SnippetID=@id";

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

        public List<Int32> GetTagsBySnippet(int snippetId)
        {
            var getTagQueryString =
                "SELECT ts.TagID FROM [Tag_Snippet] as [ts] WHERE ts.SnippetID = @id";

            var tags = new List<int>();
            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {

                    using (var cmdTag = new SqlCommand(getTagQueryString, conn))
                    {
                        conn.Open();
                        cmdTag.Parameters.AddWithValue("id", snippetId);
                        using (var reader = cmdTag.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tags.Add(Convert.ToInt32(reader["TagID"]));
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

        public Snippet CreateSnippetFromReader(SqlDataReader reader)
        {
            var tags = GetTagsBySnippet(Convert.ToInt32(reader["SnippetID"]));

            return new Snippet(
                Convert.ToInt32(reader["SnippetID"]),
                Convert.ToString(reader["Code"]),
                Convert.ToInt32(reader["Points"]),
                Convert.ToBoolean(reader["IsPrivate"]),
                Convert.ToInt32(reader["AuthorID"]),
                tags);                
        }            
    }
}
