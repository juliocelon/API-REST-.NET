using System;
using System.Linq;
using api.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using api.Models;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            using (var context = new ApplicationDBContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Add some users
                context.User.Add(new User
                {
                    Login = "user1",
                    Password = "pwd",
                    Role = "admin",
                    USD_Balance = 10000
                });

                context.User.Add(new User
                {
                    Login = "user2",
                    Password = "pwd",
                    Role = "user",
                    USD_Balance = 20000
                });

                context.User.Add(new User
                {
                    Login = "user3",
                    Password = "pwd",
                    Role = "user",
                    USD_Balance = 30000
                });

                context.SaveChanges();
            }

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
