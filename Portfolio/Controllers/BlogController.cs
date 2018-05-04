using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class BlogController : Controller
    {
        private IBlogPostRepository _blogRepo;
        public BlogController(IBlogPostRepository blogRepo = null)
        {
            _blogRepo = blogRepo ?? new EFBlogPostRepository();
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
                ["total"] = _blogRepo.BlogPosts.Count(),
                ["data"] = _blogRepo.BlogPosts.OrderByDescending(b => b.Time).Skip((pageNum - 1) * perPage).Take(perPage).ToList()
            };
            return Json(model);
        }
    }
}