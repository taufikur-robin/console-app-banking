using Microsoft.EntityFrameworkCore;
namespace Banking.Data;

public class BankingContext : DbContext
{
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    
    public BankingContext(DbContextOptions<BankingContext> options): base(options) { }
    
}