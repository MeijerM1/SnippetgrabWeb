using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.ContextInterfaces;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.Data
{
    public class SnippetMsSqlContext : ISnippetContext
    {
        private const string SqlCon = @"Data Source=192.168.19.152,1433\\MSSQLSERVER; Network Library = DBMSSOCN; Initial Catalog = dbi356615; User ID=dbuser;Password=Wachtwoord1;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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

                    AddtagForSnippet(snippet.Tags);
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
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
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
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

        public List<Snippet> GetMostRecent()
        {
            var getUserQueryString =
                "select * from Snippet limit 10";

            var results = new List<Snippet>();

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
                                results.Add(CreateSnippetFromReader(reader));
                            }
                            return results;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public List<Tag> GetTagsBySnippet(int snippetId)
        {
            var getTagQueryString =
                "SELECT t.TagID, t.Text FROM [Tag_Snippet] as [ts] JOIN Tag as [t] ON t.TagID = ts.TagID  WHERE ts.SnippetID = @id";

            var tags = new List<Tag>();
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
                                tags.Add(CreateTagFromReader(reader));
                            }
                            return tags;
                        }
                    }
                }
            }
            catch (Exception e)
            {
               Debug.WriteLine(e.Message);
                return null;
            }
        }

        public bool AddtagForSnippet(List<Tag> tags)
        {
            var getLastAddedQuery = "SELECT IDENT_CURRENT('Snippet') AS 'ID'";

            var QueryString =
                "INSERT INTO [Tag_Snippet] (TagID, SnippetID) VALUES (@TagID, @SnippetID)";


            int id = -1;

            using (var conn = new SqlConnection(SqlCon))
            {
                

                using (var cmd1 = new SqlCommand(getLastAddedQuery, conn))
                {
                    conn.Open();
                    using (var reader = cmd1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader["ID"]);
                        }
                        conn.Close();
                    }
                }

            }
            using (var conn = new SqlConnection(SqlCon))
            {
                foreach (var tag in tags)
                {
                    using (var cmd2 = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();

                        cmd2.Parameters.AddWithValue("TagID", tag.ID);
                        cmd2.Parameters.AddWithValue("SnippetID", id);
                        cmd2.ExecuteNonQuery();

                        conn.Close();

                    }
                }
            }
            return true;
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

        public Tag CreateTagFromReader(SqlDataReader reader)
        {
            return new Tag(
                Convert.ToInt32(reader["TagID"]),
                Convert.ToString(reader["Text"])
              );
        }
    }
}
