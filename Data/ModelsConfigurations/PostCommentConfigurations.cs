using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlatform.Data.ModelsConfigurations
{
    public class PostCommentConfigurations : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.HasKey(e=>e.Id);
            builder.Property(e=>e.Comment).IsRequired();
            builder.Property(e=>e.UserId).IsRequired();
            builder.HasOne(e=>e.User).WithMany(e=>e.PostComments).HasForeignKey(e=>e.UserId);
            builder.HasOne(e=>e.Post).WithMany(e=>e.PostComments).HasForeignKey(e=>e.PostId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(e=>e.CreatedAt).HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(e=>e.UpdatededAt).HasDefaultValueSql("getdate()").IsRequired();
        }
    }
}