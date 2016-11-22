using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetgrabClasslibrary.Models
{
    public class Problem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Points { get; set; }
        public int AuthorID { get; set; }
        public bool IsSolved { get; set; }
        public List<int> Tags { get; set; }

        public Problem(int id, string title, string text, int points, int authorId, List<int> tags, bool isSolved)
        {
            Tags = new List<int>();

            ID = id;
            Title = title;
            Text = text;
            Points = points;
            AuthorID = authorId;
            Tags = tags;
            IsSolved = isSolved;
        }

        public Problem(string title, string text, int points, int authorId, List<int> tags, bool isSolved)
        {
            Tags= new List<int>();

            Title = title;
            Text = text;
            Points = points;
            AuthorID = authorId;
            Tags = tags;
            IsSolved = isSolved;
        }
    }
}
