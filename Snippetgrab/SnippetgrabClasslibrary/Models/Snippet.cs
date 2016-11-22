﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetgrabClasslibrary.Models
{
    public class Snippet
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int Points { get; set; }
        public bool IsPrivate { get; set; }
        public int AuthorID { get; set; }
        public List<int> Tags { get; set; }

        public Snippet(int id, string code, int points, bool isPrivate, int authorId, List<int> tags)
        {
            Tags= new List<int>();

            ID = id;
            Code = code;
            Points = points;
            IsPrivate = isPrivate;
            AuthorID = authorId;
            Tags = tags;
        }

        public Snippet(string code, int points, bool isPrivate, int authorId, List<int> tags)
        {
            Tags = new List<int>();

            Code = code;
            Points = points;
            IsPrivate = isPrivate;
            AuthorID = authorId;
            Tags = tags;
        }
    }
}
