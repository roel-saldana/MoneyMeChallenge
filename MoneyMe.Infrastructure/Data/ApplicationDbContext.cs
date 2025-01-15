using Microsoft.EntityFrameworkCore;
using MoneyMe.Core.Entities;

namespace MoneyMe.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<LoanApplication> LoanApplications{ get; set; }
        public DbSet<LoanPaymentDetail> LoanPaymentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LoanApplication>().Property(x => x.AmountRequired).HasColumnType("decimal(18,2)");

        }
    }
}