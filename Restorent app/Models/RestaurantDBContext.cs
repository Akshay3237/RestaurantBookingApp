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
            modelBuilder.Entity<BookModel>()
                .HasOne(b => b.User)  // Configure relationship with User
                .WithMany()  // Assuming User has no navigation property back to Book
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete

            modelBuilder.Entity<BookModel>()
                .HasOne(b => b.Table)  // Configure relationship with Table
                .WithMany()  // Assuming Table has no navigation property back to Book
                .HasForeignKey(b => b.TableId)
                .OnDelete(DeleteBehavior.Restrict);
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

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RestaurantModel> Restaurants { get; set; }

        public DbSet<TableModel> Tables { get; set; }

        public DbSet<BookModel> Books { get; set; }

        public DbSet<WaitingModel> Waiters { get; set; }

        public DbSet<NotificationModel> notifications { get; set; }

        public DbSet<FeedbackModel> feedbacks { get; set; }

        
    }
}
