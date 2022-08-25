using System;
using postApi.Models;
using Microsoft.EntityFrameworkCore;

namespace postApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
    }
}
