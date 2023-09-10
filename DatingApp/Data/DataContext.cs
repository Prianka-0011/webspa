using DatingApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext( DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<AppUser> Users { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<UserLike> Likes { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserLike>().HasKey(k => new { k.SourceUserId, k.LikeUserId });
            builder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(l => l.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserLike>()
              .HasOne(s => s.LikedUser)
              .WithMany(l => l.LikedByUsers)
              .HasForeignKey(l => l.LikeUserId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Message>()
               .HasOne(s => s.Recepient)
               .WithMany(l => l.MessageReceive)
               .HasForeignKey(l => l.RecepientId)
               .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Message>()
              .HasOne(s => s.Sender)
              .WithMany(l => l.MessageSent)
              .HasForeignKey(l => l.SenderId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
