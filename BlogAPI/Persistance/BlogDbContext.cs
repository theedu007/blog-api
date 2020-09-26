using BlogAPI.Models.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Persistance
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryModelConfiguration());
            modelBuilder.ApplyConfiguration(new PostModelConfiguration());
            modelBuilder.ApplyConfiguration(new PostTagModelConfiguration());
            modelBuilder.ApplyConfiguration(new TagModelConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
