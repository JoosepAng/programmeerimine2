
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null;

        public DbSet<Invoice> Invoices { get; set; } = null!;

        public DbSet<InvoiceLine> InvoiceLines { get; set; } = null!;

        public DbSet<Car> Cars { get; set; } = null!;

        public DbSet<CarType> CarTypes { get; set; } = null!;

        public DbSet<Booking> Bookings { get; set; } = null!;
    }
}
