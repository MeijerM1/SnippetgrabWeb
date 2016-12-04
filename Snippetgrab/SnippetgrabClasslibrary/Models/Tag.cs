using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetgrabClasslibrary.Models
{
    public class Tag
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public Tag(int id, string text)
        {
            ID = id;
            Text = text;
        }

        public Tag(string text)
        {
            Text = text;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "ID: " + ID + " Text: " + Text;
        }
    }
}
