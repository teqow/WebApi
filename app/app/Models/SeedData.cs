using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace app.Models
{
    public static class SeedData
    {
        public static void ProductSeed(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices
                .GetRequiredService<AppDbContext>();
            context.Database.Migrate();

            if(!context.Products.Any())
            {
                context.AddRange(
                    new Product { Name = "Test1", Price = 2M },
                    new Product { Name = "Test2", Price = 3M },
                    new Product { Name = "Test3", Price = 4M }
                        );
                context.SaveChanges();
            }
        }
    }
}
