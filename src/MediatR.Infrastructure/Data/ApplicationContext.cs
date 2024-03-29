﻿using MediatR.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
