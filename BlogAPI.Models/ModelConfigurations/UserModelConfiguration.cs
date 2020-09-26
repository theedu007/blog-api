using BlogAPI.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPI.Models.ModelConfigurations
{
    public class UserModelConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");

            builder.HasKey(user => user.Id);
            builder.Property(user => user.Id)
                .ValueGeneratedOnAdd();

            builder.Property(user => user.Name)
                .IsRequired();
            builder.Property(user => user.UserName)
                .IsRequired();
            builder.Property(user => user.Password)
                .IsRequired();
        }
    }
}
