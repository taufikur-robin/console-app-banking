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
    
    public List<TransactionModel> Transactions => _context.Transactions.ToList();
    public void AddTransaction(Transaction transaction, int accountId)
    {
        var transactionModel = new TransactionModel
        {
            AccountId = accountId,
            DateCreated = transaction.DateTime,
            Operation = transaction.Operation,
            Amount = transaction.Amount
        };
        
        _context.Transactions.Add(transactionModel);
        _context.SaveChanges();
    }
}