using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SnippetgrabClasslibrary.ContextInterfaces;
using SnippetgrabClasslibrary.Models;

namespace SnippetgrabClasslibrary.Data
{
    public class UserMsSqlContext : IUserContext
    {
        private const string SqlCon = @"Data Source = (LocalDB)\MSSQLLocalDB;" +
                                      @"AttachDbFilename=|DataDirectory|\Snippetgrab.mdf;" +
                                      "Integrated Security = True;" +
                                      "Connect Timeout = 30";

        public bool CheckPassword(string email, string password)
        {
            using (var conn = new SqlConnection(SqlCon))
            {
                const string query = "SELECT Salt, HashedPassword FROM [User] WHERE Email = @email";
                using (var command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("email", email);

                    conn.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var passwordFromDb = Convert.ToString(reader["HashedPassword"]);
                            var hashedPassword = GenerateSha256Hash(password, Convert.ToString(reader["Salt"]));

                            return (hashedPassword == passwordFromDb);
                        }
                    }
                }
            }
            return false;
        }

        public bool AddUser(User user, string password)
        {
            var QueryString =
                "INSERT INTO [User] (Name, JoinDate, Reputation, Email, IsAdmin, Salt, HashedPassword) VALUES (@name, @joinDate, @reputation, @email, @isAdmin, @salt, @password)";

            using (var conn = new SqlConnection(SqlCon))
            {
                var salt = CreateSalt();
                var hashedPassword = GenerateSha256Hash(password, salt);
                try
                {

                    using (var cmd1 = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd1.Parameters.AddWithValue("name", user.Name);
                        cmd1.Parameters.AddWithValue("joinDate", user.JoinDate);
                        cmd1.Parameters.AddWithValue("reputation", user.Reputation);
                        cmd1.Parameters.AddWithValue("email", user.Email);
                        cmd1.Parameters.AddWithValue("isAdmin", Convert.ToInt32(user.IsAdmin));
                        cmd1.Parameters.AddWithValue("salt", salt);
                        cmd1.Parameters.AddWithValue("password", hashedPassword);
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

        public List<User> GetAll()
        {
            var getUserQueryString =
                "SELECT u.UserID, u.Name, u.JoinDate, u.Reputation, u.Email, u.IsAdmin FROM [User] as [u]";

            var users = new List<User>();
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
                                users.Add(CreateUserFromReader(reader));
                            }
                            return users;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public User GetUserByEmail(string email)
        {
            var getUserQueryString =
                "SELECT u.UserID, u.Name, u.JoinDate, u.Reputation, u.Email, u.IsAdmin FROM [User] as [u] WHERE u.Email = @email";

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmdGetUser = new SqlCommand(getUserQueryString, conn))
                    {
                        conn.Open();
                        cmdGetUser.Parameters.AddWithValue("email", email);

                        using (var reader = cmdGetUser.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var u = CreateUserFromReader(reader);
                                return u;
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

        public User GetUserById(int id)
        {
            var getUserQueryString =
                "SELECT u.UserID, u.Name, u.JoinDate, u.Reputation, u.Email, u.IsAdmin FROM [User] as [u] WHERE u.UserID = @id";

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
                                var u = CreateUserFromReader(reader);
                                return u;
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

        public bool RemoveUser(int userId)
        {
            var QueryString = "DELETE * FROM User WHERE UserID=@id";

            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmd = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("id", userId);
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

        public bool ResetPassword(int userId, string password)
        {
            var QueryString = "UPDATE User SET Salt=@salt, HashedPassword=@password WHERE UserID=id";

            var salt = CreateSalt();
            var hashedPassword = GenerateSha256Hash(password, salt);
            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {
                    using (var cmd = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("id", userId);
                        cmd.Parameters.AddWithValue("Salt", salt);
                        cmd.Parameters.AddWithValue("password", hashedPassword);
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

        public bool SubScribeToTag(int userId, int tagId)
        {
            var QueryString =
                "INSERT INTO [Tag_User] (TagID, UserID) VALUES (@tagId, @userId)";

            using (var conn = new SqlConnection(SqlCon))
            {
                try
                {

                    using (var cmd1 = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd1.Parameters.AddWithValue("tagId", tagId);
                        cmd1.Parameters.AddWithValue("userId", userId);
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

        public bool UnSubscribeFromTag(int userId, int tagId)
        {
            var QueryString =
                "DELETE * FROM [Tag_User] WHERE TagID=@tagId, UserID=@userId";

            using (var conn = new SqlConnection(SqlCon))
            {
                try
                {

                    using (var cmd1 = new SqlCommand(QueryString, conn))
                    {
                        conn.Open();
                        cmd1.Parameters.AddWithValue("tagId", tagId);
                        cmd1.Parameters.AddWithValue("userId", userId);
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

        public List<Int32> GetTagsByUser(int userId)
        {
            var getTagQueryString =
                "SELECT tu.TagID FROM [Tag_User] as [tu] WHERE tu.UserID = @id";

            var tags = new List<int>();
            try
            {
                using (var conn = new SqlConnection(SqlCon))
                {

                    using (var cmdTag = new SqlCommand(getTagQueryString, conn))
                    {
                        conn.Open();
                        cmdTag.Parameters.AddWithValue("id", userId);
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

        public User CreateUserFromReader(SqlDataReader reader)
        {
            var tags = GetTagsByUser(Convert.ToInt32(reader["UserID"]));

            if (tags == null)
            {
                tags = new List<int>();
            }

            return new User(
                Convert.ToInt32(reader["UserID"]),
                Convert.ToString(reader["Name"]),
                Convert.ToDateTime(reader["JoinDate"]),
                Convert.ToInt32(reader["Reputation"]),
                Convert.ToString(reader["Email"]),
                Convert.ToBoolean(reader["IsAdmin"]),
                tags
                );
        }

        public string CreateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[16];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string GenerateSha256Hash(string password, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            var sha256HashedString =
                new SHA256Managed();
            var hash = sha256HashedString.ComputeHash(bytes);

            return Encoding.UTF8.GetString(hash);
        }
    }
}
