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
    }
}
