using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class PortfolioDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<BlogPost> BlogPosts { get; set; }

        public PortfolioDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(Startup.ConnectionString);
        }

        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
