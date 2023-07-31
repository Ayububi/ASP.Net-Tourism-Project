

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Tour_FP.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Admin_Dashboard> Admin { get; set; }
        public DbSet<CustomerDetail> Customer { get; set; }

        public DbSet<CustomerDashboardViewModel> CustomerDashboard { get; set; }

    }

}
