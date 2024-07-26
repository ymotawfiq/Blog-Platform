using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlatform.Data.ModelsConfigurations
{
    public class PostsConfigurations : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(e=>e.Id);
            builder.Property(e=>e.Title).IsRequired();
            builder.Property(e=>e.Content).IsRequired();
            builder.HasIndex(e=>e.Title).IsUnique();
            builder.Property(e=>e.UserId).IsRequired();
            builder.HasOne(e=>e.User).WithMany(e=>e.Posts).HasForeignKey(e=>e.UserId);
            builder.Property(e=>e.CreatedAt).HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(e=>e.UpdatededAt).HasDefaultValueSql("getdate()").IsRequired();
        }
    }
}