using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetgrabClasslibrary.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }
        public int Reputation { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public List<int> Tags { get; set; }

        public User(int id, string name, DateTime joinDate, int reputation, string email, bool isAdmin, List<int> tags)
        {
            Tags = new List<int>();

            ID = id;
            Name = name;
            JoinDate = joinDate;
            Reputation = reputation;
            Email = email;
            IsAdmin = isAdmin;
            Tags = tags;
        }

        public User(string name, DateTime joinDate, int reputation, string email, bool isAdmin, List<int> tags)
        {
            Tags = new List<int>();

            Name = name;
            JoinDate = joinDate;
            Reputation = reputation;
            Email = email;
            IsAdmin = isAdmin;
            Tags = tags;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
