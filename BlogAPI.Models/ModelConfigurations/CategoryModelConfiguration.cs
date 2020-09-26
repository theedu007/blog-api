using BlogAPI.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.ModelConfigurations
{
    public class CategoryModelConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "dbo");

            builder.HasKey(ctry => ctry.Id);
            builder.Property(ctry => ctry.Id)
                .ValueGeneratedOnAdd();

            builder.Property(ctry => ctry.Name)
                .IsRequired();
            builder.Property(ctry => ctry.UrlSlug)
                .IsRequired();
            builder.Property(ctry => ctry.Description)
                .IsRequired();
        }
    }
}
