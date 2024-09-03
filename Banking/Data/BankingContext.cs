using Banking.Models;
using Microsoft.EntityFrameworkCore;
namespace Banking.Data;

public class BankingContext : DbContext
{
    public DbSet<AccountModel> Accounts { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost,1435;Database=BankingAppData;User ID=localTestUser;Password=testUser;TrustServerCertificate=True;");
    }
}