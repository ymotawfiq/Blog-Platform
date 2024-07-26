
using BlogPlatform.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlatform.Data.ModelsConfigurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e=>e.Id);
            builder.Property(e=>e.Email).IsRequired();
            builder.Property(e=>e.UserName).IsRequired();
            builder.HasIndex(e=>e.UserName).IsUnique();
            builder.HasIndex(e=>e.Email).IsUnique();
        }
    }
}