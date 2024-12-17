using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Categories.WebAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace Categories.WebAPI.Context
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}