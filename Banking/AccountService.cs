using Banking.Data;

namespace Banking;

public class AccountService
{
    private readonly BankingContext _context;
    private readonly TransactionStore _transactionStore;

    public AccountService(BankingContext context, TransactionStore transactionStore)
    {
        _context = context;
        _transactionStore = transactionStore;
    }
    
    public Account GetAccount()
    {
        var account = _context.Accounts.FirstOrDefault() ?? new Account();
        if (account.Id == 0)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }
        return account;
    }
    
    public void DepositFunds(Account account, decimal amount) 
    {
        account.Balance += amount;
        var transaction = new Transaction
        {
            DateCreated = DateTime.Now,
            Amount = amount,
            Operation = "Deposited",
            AccountId = account.Id,
            Balance = account.Balance
        };
        _transactionStore.AddTransaction(transaction, account.Id);
        _context.SaveChanges();
    } 

    public void WithdrawFunds(Account account, decimal amount) 
    {
        if (!HasSufficientFunds(account, amount))
        {
            throw new InvalidOperationException("Insufficient funds for the requested withdrawal.");
        }
        
        account.Balance -= amount;
        var transaction = new Transaction
        {
            DateCreated = DateTime.Now,
            Amount = amount,
            Operation = "Withdrew",
            AccountId = account.Id,
            Balance = account.Balance
        };
        _transactionStore.AddTransaction(transaction, account.Id);
        _context.SaveChanges();
    }

    public string GetStatement(Account account)
    {
        var statement = "Transaction History:\n";
        var transactions = _transactionStore.Transactions.Where(t => t.AccountId == account.Id).ToList();
        foreach (var transaction in transactions)
        {
            statement += $"{transaction.Id}. {transaction.DateCreated}: {transaction.Operation}: £{transaction.Amount:F2}\n";
        }
        statement += $"Current Balance: £{account.Balance:F2}";
        return statement;
    }

    public decimal GetBalance(Account account)
    {
        return account.Balance;
    }

    private bool HasSufficientFunds(Account account, decimal amount)
    {
        return account.Balance >= amount;
    }
}