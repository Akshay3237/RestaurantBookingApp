using Microsoft.EntityFrameworkCore;

namespace Restorent_app.Models
{
    public class RestaurantDBContext : DbContext
    {
        public RestaurantDBContext(DbContextOptions<RestaurantDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FeedbackModel>()
                .HasOne(f => f.User)
                .WithMany() // Assuming a User can have many Feedbacks
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

            modelBuilder.Entity<FeedbackModel>()
                .HasOne(f => f.Restaurant)
                .WithMany() // Assuming a Restaurant can have many Feedbacks
                .HasForeignKey(f => f.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade); // Retain cascading delete for Restaurant

                modelBuilder.Entity<NotificationModel>()
                    .HasOne(n => n.User)
                    .WithMany() // Assuming a User can have many Notifications
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete for User

                modelBuilder.Entity<NotificationModel>()
                    .HasOne(n => n.Restaurant)
                    .WithMany() // Assuming a Restaurant can have many Notifications
                    .HasForeignKey(n => n.RestaurantId)
                    .OnDelete(DeleteBehavior.Cascade); // Retain c
        }

        DbSet<UserModel> Users { get; set; }
        DbSet<RestaurantModel> Restaurants { get; set; }

        DbSet<TableModel> Tables { get; set; }

        DbSet<BookModel> Books { get; set; }

        DbSet<WaitingModel> Waiters { get; set; }

        DbSet<NotificationModel> notifications { get; set; }

        DbSet<FeedbackModel> feedbacks { get; set; }

        
    }
}
