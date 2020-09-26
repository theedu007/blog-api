using BlogAPI.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.ModelConfigurations
{
    public class PostModelConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts", "dbo");

            builder.HasKey(post => post.Id);
            builder.Property(post => post.Id)
                .ValueGeneratedOnAdd();

            builder.Property(post => post.Title)
                .IsRequired();
            builder.Property(post => post.ShortDescription)
                .IsRequired();
            builder.Property(post => post.PostContent)
                .IsRequired();
            builder.Property(post => post.UrlSlug)
                .IsRequired();
            builder.Property(post => post.Published)
                .IsRequired();
            builder.Property(post => post.PostedOn)
                .IsRequired();
            builder.Property(Post => Post.UpdatedAt)
                .IsRequired(false);

            builder.HasOne(post => post.User)
                .WithMany(User => User.Posts)
                .IsRequired();
            builder.HasOne(post => post.Category)
                .WithMany(category => category.Posts)
                .IsRequired();
        }
    }
}
