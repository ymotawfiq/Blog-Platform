using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlatform.Data.ModelsConfigurations
{
    public class IdentityUserConfigurations : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.HasKey(e=>e.Id);
            builder.Property(e=>e.Email).IsRequired();
            builder.Property(e=>e.UserName).IsRequired();
            builder.HasIndex(e=>e.UserName).IsUnique();
            builder.HasIndex(e=>e.Email).IsUnique();
        }
    }
}