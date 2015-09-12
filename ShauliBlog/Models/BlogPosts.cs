using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    public class BlogPosts
    {
        public IEnumerable<Post> Posts { get; set; }
        public Comment Comment { get; set; }
    }
}