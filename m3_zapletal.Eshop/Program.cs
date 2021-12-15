using m3_zapletal.Eshop.Areas.Admin.Models.Database;
using m3_zapletal.Eshop.Models.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m3_zapletal.Eshop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                if (scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<EshopDbContext>();

                    DatabaseInit dbInit = new DatabaseInit();
                    dbInit.Initialize(dbContext);

                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                    Task task = dbInit.EnsureRoleCreated(roleManager);
                    task.Wait();
                    task.Dispose();

                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                    task = dbInit.EnsureAdminCreated(userManager);
                    task.Wait();
                    task.Dispose();

                    task = dbInit.EnsureManagerCreated(userManager);
                    task.Wait();
                    task.Dispose();
                }
            }



            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(loggingBuilder =>
                 {
                     loggingBuilder.ClearProviders();
                     loggingBuilder.AddConsole();
                     loggingBuilder.AddDebug();
                     loggingBuilder.AddFile("logs/eshop-log-{Date}.txt");

                 });
    }
}
