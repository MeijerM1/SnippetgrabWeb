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
    public class CommentMsSqlContext : ICommentContext
    {
        private const string SqlCon = @"Server = mssql.fhict.local; Database=dbi356615;User Id = dbi356615; Password=Kipgarfield1";

        public bool AddComment(Comment comment)
        {
            var QueryString =
                "INSERT INTO [Comment] (Text, ReplyToID, AuthorID, ProblemID, Points) VALUES (@text, @replyToId, @authorId, @problemId, @points)";

            using (var conn = new SqlConnection(SqlCon))
            {
                try
                {
                    using (var cmd1 = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd1.Parameters.AddWithValue("text", comment.Text);
                        cmd1.Parameters.AddWithValue("replyToId", comment.ReplyToID);
                        cmd1.Parameters.AddWithValue("authorId", comment.AuthorID);
                        cmd1.Parameters.AddWithValue("problemId", comment.ProblemID);
                        cmd1.Parameters.AddWithValue("points", comment.Points);
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

        public bool ChangePoint(int commentId, int increaseDecrease)
        {
            using (var conn = new SqlConnection(SqlCon))
            using (var command = new SqlCommand("dbo.ChangePoint", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ItemID", commentId));
                command.Parameters.Add(new SqlParameter("@IncreaseDecrease", increaseDecrease));
                command.Parameters.Add(new SqlParameter("@TypeToIncrease", "Comment"));
                conn.Open();
                command.ExecuteNonQuery();
                return true;
            }
        }

        public List<Comment> GetAll()
        {
            var getUserQueryString =
                "SELECT c.CommentID, c.Text, c.ReplyToID, c.AuthorID, c.ProblemID, c.Points FROM [Comment] as [c]";

            var comments = new List<Comment>();

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
                                comments.Add(CreateCommentFromReader(reader));
                            }
                            return comments;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Comment GetCommentById(int commentId)
        {
            var getUserQueryString =
                "SELECT c.CommentID, c.Text, c.ReplyToID, c.AuthorID, c.ProblemID, c.Points FROM [Comment] as [c] WHERE c.CommentID=@id";

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmdGetUser = new SqlCommand(getUserQueryString, conn))
                    {
                        conn.Open();
                        cmdGetUser.Parameters.AddWithValue("id", commentId);
                        using (var reader = cmdGetUser.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return CreateCommentFromReader(reader);
                            }
                        }
                    }
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<Comment> GetCommentByProblem(int problemId)
        {
            var getUserQueryString =
                "SELECT c.CommentID, c.Text, c.ReplyToID, c.AuthorID, c.ProblemID, c.Points FROM [Comment] as [c] WHERE c.ProblemID=@problemId";

            var comments = new List<Comment>();

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmdGetUser = new SqlCommand(getUserQueryString, conn))
                    {
                        conn.Open();
                        cmdGetUser.Parameters.AddWithValue("problemId", problemId);
                        using (var reader = cmdGetUser.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comments.Add(CreateCommentFromReader(reader));
                            }
                            return comments;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<Comment> GetCommentByUser(int userId)
        {
            var getUserQueryString =
                "SELECT c.CommentID, c.Text, c.ReplyToID, c.AuthorID, c.ProblemID, c.Points FROM [Comment] as [c] WHERE c.AuthorID=@authorId";

            var comments = new List<Comment>();

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmdGetUser = new SqlCommand(getUserQueryString, conn))
                    {
                        conn.Open();
                        cmdGetUser.Parameters.AddWithValue("authorId", userId);
                        using (var reader = cmdGetUser.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comments.Add(CreateCommentFromReader(reader));
                            }
                            return comments;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<Comment> GetRepliesByComment(int commentId)
        {
            var getUserQueryString =
                "SELECT c.CommentID, c.Text, c.ReplyToID, c.AuthorID, c.ProblemID, c.Points FROM [Comment] as [c] WHERE c.ReplyToID=@commentId";

            var comments = new List<Comment>();

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmdGetUser = new SqlCommand(getUserQueryString, conn))
                    {
                        conn.Open();
                        cmdGetUser.Parameters.AddWithValue("commentId", commentId);
                        using (var reader = cmdGetUser.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comments.Add(CreateCommentFromReader(reader));
                            }
                            return comments;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool RemoveComment(int commentId)
        {
            var QueryString = "DELETE * FROM Comment WHERE CommentID=@id";

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmd = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("id", commentId);
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

        public Comment CreateCommentFromReader(SqlDataReader reader)
        {
            // If the comment is not a reply it will cause a null ref error
            // If so use the constructor without the reply ID
            try
            {
                return new Comment(
                    Convert.ToInt32(reader["CommentID"]),
                    Convert.ToString(reader["Text"]),
                    Convert.ToInt32(reader["ReplyToID"]),
                    Convert.ToInt32(reader["AuhorID"]),
                    Convert.ToInt32(reader["ProblemID"]),
                    Convert.ToInt32(reader["Points"]));
            }
            catch (NullReferenceException)
            {
                return new Comment(
                    Convert.ToInt32(reader["CommentID"]),
                    Convert.ToString(reader["Text"]),
                    Convert.ToInt32(reader["AuhorID"]),
                    Convert.ToInt32(reader["ProblemID"]),
                    Convert.ToInt32(reader["Points"]));
            }
        }            
    }
}
