using Banking.Data;

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
    
    public string GetLastTransaction()
    {
        var lastTransaction = _context.Transactions
            .OrderByDescending(t => t.DateCreated)
            .FirstOrDefault();

        if (lastTransaction == null)
        {
            return "No transactions found.";
        }
        
        return $"{lastTransaction.DateCreated}: {lastTransaction.Operation}: £{lastTransaction.Amount:F2}, New Balance: £{lastTransaction.Balance:F2}";
    }
}