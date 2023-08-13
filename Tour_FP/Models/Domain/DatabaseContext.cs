

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Tour_FP.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = true; // Enable lazy loading
        }
        public DbSet<Admin_Dashboard> Admin { get; set; }
        public DbSet<CustomerDetail> Customer { get; set; }

        public DbSet<CustomerDashboardViewModel> CustomerDashboard { get; set; }

        public DbSet<ReviewTable> ReviewTable { get; set; }

        public DbSet<CommunityPostTable> PostTable { get; set; }

        public DbSet<CommentTable> commentTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CommunityPostTable>()
                .HasMany(c => c.Comments)
                .WithOne(e => e.CommunityPost)
                .HasForeignKey(e => e.PostId);
        }

    }

}
