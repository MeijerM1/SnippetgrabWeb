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
        public List<Tag> Tags { get; set; }

        public Problem()
        {            
            Tags = new List<Tag>();
        }

        public Problem(int id, string title, string text, int points, int authorId, List<Tag> tags, bool isSolved)
        {
            Tags = new List<Tag>();

            ID = id;
            Title = title;
            Text = text;
            Points = points;
            AuthorID = authorId;
            Tags = tags;
            IsSolved = isSolved;
        }

        public Problem(string title, string text, int points, int authorId, List<Tag> tags, bool isSolved)
        {
            Tags= new List<Tag>();

            Title = title;
            Text = text;
            Points = points;
            AuthorID = authorId;
            Tags = tags;
            IsSolved = isSolved;
        }
    }
}
