using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ShauliBlog.Controllers
{
    public class BlogController : Controller
    {
        private BlogDb db = new BlogDb();

        // GET: Blog
        public ActionResult Index()
        {
            List<Post> posts = db.Posts.Include(post => post.Comments).ToList();
            posts.Sort();

            return View(posts);
        }
    }
}