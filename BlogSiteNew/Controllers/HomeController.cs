using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogSiteNew.Models;
using Newtonsoft.Json;
using BlogSiteNew.Data;

namespace BlogSiteNew.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = "Data Source=.\\sqlexpress;Initial Catalog=BlogSite;Integrated Security=True";

        public IActionResult Index(int page)
        {
            //page 1 == skip 0
            //page 2 == skip 3
            //page 3 == skip 6
            //page 4 == skip 9
            if (page <= 0)
            {
                page = 1;
            }
            int pageCount = 3;

            BlogDb db = new BlogDb(_connectionString);
            HomePageViewModel vm = new HomePageViewModel();
            int total = db.GetPostsCount();
            if (page > 1)
            {
                vm.NextPage = page - 1;
            }
            int from = (page - 1) * pageCount;
            int to = from + pageCount;
            if (to < total)
            {
                vm.PreviousPage = page + 1;
            }
            vm.Posts = db.GetPosts(from, pageCount);
            
            return View(vm);
        }

        public ActionResult ViewBlog(int id)
        {
            if (id == 0)
            {
                return Redirect("/"); //if no id was sent in, redirect to home page
            }
            BlogDb db = new BlogDb(_connectionString);
            ViewBlogViewModel vm = new ViewBlogViewModel();
            vm.Post = db.GetPost(id);
            vm.Comments = db.GetComments(id);
            if (Request.Cookies["commenter-name"] != null)
            {
                vm.CommenterName = Request.Cookies["commenter-name"];
            }
            return View(vm);
        }

        public ActionResult AddComment(Comment comment)
        {
            BlogDb db = new BlogDb(_connectionString);
            comment.DateCreated = DateTime.Now;
            db.AddComment(comment);
            Response.Cookies.Append("commenter-name", comment.Name);
            return Redirect($"/home/viewblog?id={comment.PostId}");
        }

        public ActionResult MostRecent()
        {
            BlogDb db = new BlogDb(_connectionString);
            int id = db.GetMostRecentId();
            return Redirect($"/home/viewblog?id={id}");
        }
    }
}
