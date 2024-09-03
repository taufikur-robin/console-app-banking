using Banking.Data;
using Banking.Models;

namespace Banking;
public class TransactionStore
{
    private readonly BankingContext _context;

    public TransactionStore(BankingContext context)
    {
        _context = context;
    }
    
    public List<Transaction> Transactions => _context.Transactions.ToList();
    public void AddTransaction(Transaction transaction, int accountId)
    {
        transaction.AccountId = accountId;
        _context.Transactions.Add(transaction);
        _context.SaveChanges();
    }
}