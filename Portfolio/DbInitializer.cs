using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Models;

namespace Portfolio.Data
{
    public class DbInitializer
    {
        public static void Initialize(PortfolioDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.Add(new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" });
            context.Roles.Add(new IdentityRole() { Name = "User", NormalizedName = "USER" });
            
            context.SaveChanges();
        }
    }
}