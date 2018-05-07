using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Models;

namespace Portfolio
{
    public class Startup
    {
        public static string ConnectionString;

        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            string azureConnectionString = Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb");
            ConnectionString = azureConnectionString == null ? 
                Configuration["ConnectionStrings:DefaultConnection"] : 
                ParseAzureConnectionString(azureConnectionString);
        }

        private static string ParseAzureConnectionString(string azureString)
        {
            string server = "server=localhost;";

            int portStart = azureString.IndexOf(':') + 1;
            int portStop = azureString.IndexOf(';', portStart);
            string port = "port=" + azureString.Substring(portStart, portStop - portStart) + ";";

            string database = "database=localdb;";

            string uid = "uid=azure;";

            string pwd = "pwd=" + azureString.Substring(azureString.LastIndexOf('=') + 1) + ';';

            return server+port+database+uid+pwd;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFrameworkMySql().AddDbContext<PortfolioDbContext>(options => options.UseMySql(ConnectionString));

            // This is new
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<PortfolioDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 0;
                options.Password.RequireDigit = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
