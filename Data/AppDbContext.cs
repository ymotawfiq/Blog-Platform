

using BlogPlatform.Data.Models;
using BlogPlatform.Data.ModelsConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
            SeedRoles(builder);
            ApplyConfiguration(builder);
        }
        
        private void ApplyConfiguration(ModelBuilder builder){
            builder
                .ApplyConfiguration(new UserConfigurations())
                .ApplyConfiguration(new PostsConfigurations())
                .ApplyConfiguration(new PostCommentConfigurations())
                .ApplyConfiguration(new FollowConfigurations());
        }

        private void SeedRoles(ModelBuilder builder){
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole{
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    Name = "ADMIN",
                    NormalizedName = "ADMIN",
                    Id = Guid.NewGuid().ToString()
                },
                new IdentityRole{
                    ConcurrencyStamp=Guid.NewGuid().ToString(),
                    Name = "USER",
                    NormalizedName = "USER",
                    Id = Guid.NewGuid().ToString()
                }
            );
        }

        public DbSet<Post> Post {get; set;}
        public DbSet<PostComment> PostComments {get; set;}
        public DbSet<Follow> Follows {get; set;}
    }
}