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

        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult PopularPosts()
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

        public ActionResult GetPopularComments()
        {
            // Creates all possible combinations of post-comments pairs
            var results = (from post in db.Posts
                           join
                               comment in db.Comments on
                               post.Id equals comment.PostId into postComments

                           select new
                           {
                               Post = post.Headline + " by " + post.Author,
                               CommentsCount = postComments.Count()
                           });

            // Gets the top 5 posts.
            results = results.Where(post => post.CommentsCount != 0).OrderByDescending(post => post.CommentsCount).Take(5);

            return Json(results.ToList());
        }
    }
}