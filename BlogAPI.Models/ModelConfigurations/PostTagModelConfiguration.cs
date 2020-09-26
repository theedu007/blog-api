using BlogAPI.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.ModelConfigurations
{
    public class PostTagModelConfiguration : IEntityTypeConfiguration<PostsTags>
    {
        public void Configure(EntityTypeBuilder<PostsTags> builder)
        {
            builder.ToTable("PostTags", "dbo");

            builder.HasKey(postTag => new { postTag.PostId, postTag.TagId });

            builder.HasOne(posTag => posTag.Post)
                .WithMany(post => post.Tags)
                .HasForeignKey(postTag => postTag.PostId)
                .IsRequired();
            builder.HasOne(postTag => postTag.Tag)
                .WithMany(tag => tag.Posts)
                .HasForeignKey(postTag => postTag.TagId)
                .IsRequired();
           
        }
    }
}
