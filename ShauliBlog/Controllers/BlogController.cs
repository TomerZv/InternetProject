using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShauliBlog.Controllers
{
    public class BlogController : Controller
    {
        private BlogDb db = new BlogDb();

        // GET: Blog
        public ActionResult Index()
        {
            BlogPosts blog = new BlogPosts();
            
            List<Post> posts = db.Posts.ToList();
            posts.Sort();
            blog.Posts = posts;

            blog.Comment = new Comment();

            return View(blog);
        }
    }
}