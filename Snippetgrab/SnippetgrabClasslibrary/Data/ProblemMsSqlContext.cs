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
    public class ProblemMsSqlContext : IProblemContext
    {
        private const string SqlCon = @"Data Source=192.168.19.152,1433\\MSSQLSERVER; Network Library = DBMSSOCN; Initial Catalog = dbi356615; User ID=dbuser;Password=Wachtwoord1;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool ChangePoint(int problemId, int increaseDecrease)
        {
            using (var conn = new SqlConnection(SqlCon))
            using (var command = new SqlCommand("dbo.ChangePoint", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ItemID", problemId));
                command.Parameters.Add(new SqlParameter("@IncreaseDecrease", increaseDecrease));
                command.Parameters.Add(new SqlParameter("@TypeToIncrease", "Problem"));
                conn.Open();
                command.ExecuteNonQuery();
                return true;
            }
        }

        public bool AddProblem(Problem problem)
        {
            var QueryString =
                "INSERT INTO [problem] (Text, Points, AuthorID, IsSolved, Title) VALUES (@text, @points, @authorId, @isSolved, @title)";

            using (var conn = new SqlConnection(SqlCon))
            {
                try
                {
                    using (var cmd1 = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd1.Parameters.AddWithValue("text", problem.Text);
                        cmd1.Parameters.AddWithValue("points", problem.Points);
                        cmd1.Parameters.AddWithValue("authorId", problem.AuthorID);
                        cmd1.Parameters.AddWithValue("isSolved", Convert.ToInt32(problem.IsSolved));
                        cmd1.Parameters.AddWithValue("title", problem.Title);
                        cmd1.ExecuteNonQuery();
                    }

                    AddtagForProblem(problem.Tags);
                    return true;
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception.Message);
                    return false;
                }
            }
        }

        public bool ChangeIsSolved(int problemId, bool isSolved)
        {
            var QueryString = "UPDATE problem SET IsSolved=@isSolved WHERE ProblemID=id";

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmd = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("id", problemId);
                        cmd.Parameters.AddWithValue("isPrivate", Convert.ToInt32(isSolved));

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

        public List<Problem> GetAll()
        {
            var getUserQueryString =
                "SELECT p.problemID, p.Text, p.Points, p.AuthorID, p.IsSolved, p.Title FROM [Problem] as [p]";

            var problems = new List<Problem>();
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
                                problems.Add(CreateProblemFromReader(reader));
                            }
                        }
                    }
                    return problems;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public Problem GetProblemById(int problemId)
        {
            var getUserQueryString =
                "SELECT p.problemID, p.Text, p.Points, p.AuthorID, p.IsSolved, p.Title FROM [Problem] as [p] WHERE p.ProblemID=@id";


            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmdGetUser = new SqlCommand(getUserQueryString, conn))
                    {
                        conn.Open();
                        cmdGetUser.Parameters.AddWithValue("id", problemId);
                        using (var reader = cmdGetUser.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return CreateProblemFromReader(reader);
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

        public List<Problem> GetProblemByTag(int tagId)
        {
            var getUserQueryString =
                "SELECT p.problemID, p.Text, p.Points, p.AuthorID, p.IsSolved, p.Title FROM [Problem] as [p] JOIN [Tag_problem] as [tp] ON tp.ProblemID = p.ProblemID WHERE tp.TagID=@id";

            var problems = new List<Problem>();

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
                                problems.Add(CreateProblemFromReader(reader));
                            }
                            return problems;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<Problem> GetProblemByUser(int userId)
        {
            var getUserQueryString =
                "SELECT p.problemID, p.Text, p.Points, p.AuthorID, p.IsSolved, p.Title FROM [Problem] as [p] WHERE p.AuthorID=@id";

            var problems = new List<Problem>();
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
                                problems.Add(CreateProblemFromReader(reader));
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

        public bool RemoveProblem(int problemId)
        {
            var QueryString = "DELETE * FROM Problem WHERE problemID=@id";

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmd = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("id", problemId);
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

        public List<Problem> GetMostRecent()
        {
            var getUserQueryString =
                "select TOP 10 * from Problem";

            var results = new List<Problem>();

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
                                results.Add(CreateProblemFromReader(reader));
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

        public List<Tag> GetTagsByProblem(int problemId)
        {
            var getTagQueryString =
                "SELECT t.TagID, t.Text FROM [Tag_Problem] as [ts] JOIN Tag as [t] ON t.TagID = ts.TagID  WHERE ts.ProblemID = @id";

            var tags = new List<Tag>();
            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {

                    using (var cmdTag = new SqlCommand(getTagQueryString, conn))
                    {
                        conn.Open();
                        cmdTag.Parameters.AddWithValue("id", problemId);
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

        public bool AddtagForProblem(List<Tag> tags)
        {
            var getLastAddedQuery = "SELECT IDENT_CURRENT('Problem') AS 'ID'";

            var QueryString =
                "INSERT INTO [Tag_Problem] (TagID, ProblemID) VALUES (@TagID, @ProblemID)";


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
                        cmd2.Parameters.AddWithValue("ProblemID", id);
                        cmd2.ExecuteNonQuery();

                        conn.Close();
                    }
                }
            }
            return true;
        }

        public Problem CreateProblemFromReader(SqlDataReader reader)
        {
            var tags = new List<Tag>();
            tags = GetTagsByProblem(Convert.ToInt32(reader["problemID"]));

            return new Problem(
                Convert.ToInt32(reader["problemID"]),
                Convert.ToString(reader["Title"]),
                Convert.ToString(reader["Text"]),
                Convert.ToInt32(reader["Points"]),
                Convert.ToInt32(reader["AuthorID"]),
                tags,
                Convert.ToBoolean(reader["IsSolved"]));
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
