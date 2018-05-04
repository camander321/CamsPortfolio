using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

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
    }
}