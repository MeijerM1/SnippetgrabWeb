using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetgrabClasslibrary.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int Points { get; set; }
        public int AuthorID { get; set; }
        public int ReplyToID { get; set; }
        public int ProblemID { get; set; }

        public Comment(int id, string text, int points, int authorId, int replyToId, int problemId)
        {
            ID = id;
            Text = text;
            Points = points;
            AuthorID = authorId;
            ReplyToID = replyToId;
            ProblemID = problemId;
        }

        public Comment(string text, int points, int authorId, int replyToId, int problemId)
        {
            Text = text;
            Points = points;
            AuthorID = authorId;
            ReplyToID = replyToId;
            ProblemID = problemId;
        }

        public Comment(int id, string text, int points, int authorId, int problemId)
        {
            ID = id;
            Text = text;
            Points = points;
            AuthorID = authorId;
            ProblemID = problemId;
        }

        public Comment(string text, int points, int authorId, int problemId)
        {
            Text = text;
            Points = points;
            AuthorID = authorId;
            ProblemID = problemId;
            ReplyToID = -1;
        }
    }
}
