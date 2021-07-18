using MediatR.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediatR.Infrastructure.Data
{
    public class ApplicationContextSeed
    {
        private static readonly Random random = new Random();

        public static async Task SeedAsync(ApplicationContext context)
        {
            if (!await context.Items.AnyAsync())
            {
                await context.Items.AddRangeAsync(new List<Item>
                {
                    new Item { Title = "Pencil", UniqueNumber = random.NextDouble() },
                    new Item { Title = "Notebook", UniqueNumber = random.NextDouble() },
                    new Item { Title = "Monitor", UniqueNumber = random.NextDouble() },
                    new Item { Title = "Phone", UniqueNumber = random.NextDouble() },
                    new Item { Title = "Keyboard", UniqueNumber = random.NextDouble() },
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
