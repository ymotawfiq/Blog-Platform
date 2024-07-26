using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlatform.Data.ModelsConfigurations
{
    public class FollowConfigurations : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.HasKey(e=>e.Id);
            builder.Property(e=>e.User1Id).IsRequired();
            builder.Property(e=>e.User2Id).IsRequired();
            builder.HasIndex(e=>new{e.User1Id, e.User2Id}).IsUnique();
            builder.HasOne(e=>e.User1).WithMany(e=>e.Follows1).HasForeignKey(e=>e.User1Id)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e=>e.User2).WithMany(e=>e.Follows2).HasForeignKey(e=>e.User2Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}