using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using Portfolio.ViewModels;

namespace Portfolio.Controllers
{
    public class BlogController : Controller
    {
        private PortfolioDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public BlogController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, PortfolioDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPosts(int pageNum = 1, int perPage = 10)
        {
            Dictionary<string, Object> model = new Dictionary<string, object>
            {
                ["page"] = pageNum,
                ["shown"] = perPage,
                ["total"] = _db.BlogPosts.Count(),
                ["data"] = _db.BlogPosts.OrderByDescending(b => b.Time).Skip((pageNum - 1) * perPage).Take(perPage).ToList()
            };
            return Json(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(BlogPostViewModel model)
        {
            BlogPost post = new BlogPost
            {
                Title = model.Title,
                Content = model.Content,
                Time = DateTime.Now
            };
            _db.BlogPosts.Add(post);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            BlogPost post = _db.BlogPosts.FirstOrDefault(b => b.BlogPostKey == id);
            return View(post);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _db.BlogPosts.Remove(_db.BlogPosts.FirstOrDefault(b => b.BlogPostKey == id));
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(_db.BlogPosts.FirstOrDefault(b => b.BlogPostKey == id));
        }

        [HttpPost]
        public IActionResult Edit(BlogPost post)
        {
            _db.Entry(post).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = post.BlogPostKey });
        }

        public IActionResult GetComments(int id)
        {
            return Json(_db.Comments.Include(c => c.User).Where(c => c.BlogPostId == id).OrderByDescending(c => c.Time).Take(25));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteComment(int id)
        {
            _db.Comments.Remove(_db.Comments.FirstOrDefault(c => c.CommentKey == id));
            _db.SaveChanges();
            return Json(null);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public IActionResult AddComment(int id, string commentText)
        {
            Comment comment = new Comment {
                Content = commentText,
                UserId = _userManager.GetUserId(User),
                BlogPostId = id,
                Time = DateTime.Now
            };
            _db.Comments.Add(comment);
            _db.SaveChanges();
            return Json(null);
        }
    }
}