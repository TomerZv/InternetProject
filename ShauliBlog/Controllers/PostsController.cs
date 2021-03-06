﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShauliBlog.Models;
using System.IO;
using System.Globalization;
using System.Web.Routing;

namespace ShauliBlog.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private BlogDb db = new BlogDb();

        // GET: /Posts/
        public ActionResult Index()
        {
            List<Post> posts = db.Posts.ToList();
            posts.Sort();

            return View(posts);
        }

        public ActionResult Get()
        {
            return Json(db.Posts.ToList());
        }

        // GET: /Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Post post = db.Posts.Include(currPost => currPost.Comments).Where(currPost => currPost.Id == id).FirstOrDefault();

            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: /Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Headline,Author,Website,Timestamp,Content,Category")] Post post, HttpPostedFileBase picture, HttpPostedFileBase video)
        {
            if (ModelState.IsValid)
            {
                post.Timestamp = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();

                if (picture != null && picture.ContentLength > 0)
                {
                    string fileName = post.Id + ".png";
                    picture.SaveAs(Server.MapPath("~/Uploads/" + fileName));
                }

                if (video != null && video.ContentLength > 0)
                {
                    string fileName = post.Id + ".mp4";
                    video.SaveAs(Server.MapPath("~/Uploads/" + fileName));
                }
                
 
                return RedirectToAction("Index");
            }


            return View(post);
        }

        // GET: /Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: /Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Headline,Author,Website,Timestamp,Content,Category")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.Entry(post).Property("Timestamp").IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: /Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: /Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search(int? id, string author, string text, string postedAfter, string postedBefore)
        {
            List<Post> results = db.Posts.Include(post => post.Comments).ToList();
            DateTime date;

            // Checks whether to filter by id
            if (id.HasValue)
            {
                results = results.Where(post => post.Id == id.Value).ToList();
            }

            // Checks whether to filter by author
            if (!string.IsNullOrWhiteSpace(author))
            {
                results = results.Where(post => post.Author.Contains(author)).ToList();
            }

            // Checks whether to filter by some sort of text
            if (!string.IsNullOrWhiteSpace(text))
            {
                results = results.Where(post => post.Headline.Contains(text) || post.Content.Contains(text)).ToList();
            }

            // Checks whether to get posts posted after a certain date
            if (postedAfter != null && DateTime.TryParseExact(postedAfter, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                results = results.Where(post => post.Timestamp > date).ToList();
            }

            // Checks whether to get posts posted before a certain date
            if (postedBefore != null && DateTime.TryParseExact(postedBefore, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                results = results.Where(post => post.Timestamp < date).ToList();
            }

            results.Sort();

            return View("../Blog/Index", results);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
