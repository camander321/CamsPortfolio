using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    
    public class EFBlogPostRepository : IBlogPostRepository
    {
        private PortfolioDbContext _db;
        public EFBlogPostRepository()
        {
            _db = new PortfolioDbContext();
        }

        public EFBlogPostRepository(PortfolioDbContext db)
        {
            _db = db;
        }

        public IQueryable<BlogPost> BlogPosts => throw new NotImplementedException();

        public BlogPost Edit(BlogPost post)
        {
            _db.Entry(post).State = EntityState.Modified;
            _db.SaveChanges();
            return post;
        }

        public void Remove(BlogPost post)
        {
            _db.BlogPosts.Remove(post);
            _db.SaveChanges();
        }

        public void RemoveAll()
        {
            _db.BlogPosts.RemoveRange(_db.BlogPosts);
            _db.SaveChanges();
        }

        public BlogPost Save(BlogPost post)
        {
            _db.BlogPosts.Add(post);
            _db.SaveChanges();
            return post;
        }
    }
}
