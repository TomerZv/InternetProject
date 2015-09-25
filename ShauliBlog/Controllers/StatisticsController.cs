using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShauliBlog.Controllers
{
    public class StatisticsController : Controller
    {
        private BlogDb db = new BlogDb();

        // GET: Statistics
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PublicityDivisionByCategory()
        {
            var posts = db.Posts.ToList();
            // Gets the posts by category
            
            var postsByCategory =
              from post in posts
              group post by post.Category into g
              select new { Category = g.Key.ToString(), Posts = g.ToList().Count() };

            return Json(postsByCategory.ToList());
        }
    }
}