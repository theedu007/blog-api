using BlogAPI.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.ModelConfigurations
{
    public class TagModelConfiguration : IEntityTypeConfiguration<Tags>
    {
        public void Configure(EntityTypeBuilder<Tags> builder)
        {
            builder.ToTable("Tags", "dbo");

            builder.HasKey(tag => tag.Id);
            builder.Property(tag => tag.Id)
                .ValueGeneratedOnAdd();

            builder.Property(tag => tag.Name)
                .IsRequired();
            builder.Property(tag => tag.UrlSlug)
                .IsRequired();
            builder.Property(tag => tag.Description)
                .IsRequired();
        }
    }
}
