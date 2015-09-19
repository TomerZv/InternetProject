﻿using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ShauliBlog.Controllers
{
    public class SearchController : Controller
    {
        private BlogDb db = new BlogDb();

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JoinPostComments(string postText, string commentText)
        {
            if (string.IsNullOrWhiteSpace(postText) || string.IsNullOrWhiteSpace(commentText))
            {
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                // Creates all possible combinations of post-comments pairs
                var results = (from post in db.Posts
                               join
                                   comment in db.Comments on
                                   post.Id equals comment.PostId

                               select new
                               {
                                   PostId = post.Id,
                                   PostHeadline = post.Headline,
                                   PostText = post.Content,
                                   CommentHeadline = comment.Headline,
                                   CommentText = comment.Content
                               });

                // Gets only the results of posts AND comments containing the text in headling or content
                results = results.Where(pc => ((pc.CommentHeadline.Contains(commentText)) || (pc.CommentText.Contains(commentText))) &&
                                    ((pc.PostText.Contains(postText)) || (pc.PostText.Contains(postText))));

                // Gets the posts that satisfied the conditions.
                List<Post> posts = db.Posts.Include(post => post.Comments).ToList();
                posts = posts.Where(post => results.Any(result => result.PostId == post.Id)).ToList();

                return View("../Blog/Index", posts); 
            }
        }
    }
}