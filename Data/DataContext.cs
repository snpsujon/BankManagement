using BankManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BankManagement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<UsersTbl> usersTbls { get; set; }
        public DbSet<UsersBankAccTbl> usersBankAccTbls { get; set; }
        public DbSet<BankAccTypeTbl> bankAccTypeTbls { get; set; }
        public DbSet<StatusTbl> statusTbls { get; set; }
        public DbSet<TransTbl> transTbls { get; set; }
        public DbSet<TransTypeTbl> transTypeTbls { get; set; }
        public DbSet<UserTypeTbl> userTypeTbls { get; set; }

    }
}
