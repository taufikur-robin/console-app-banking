using Banking.Data;
using Banking.Models;

namespace Banking;
public class Account
{
    private readonly BankingContext _context;
    private AccountModel _accountModel;
    private readonly TransactionStore _transactionStore;

    public Account(BankingContext context, TransactionStore transactionStore)
    {
        _context = context;
        _transactionStore = transactionStore;
        
        _accountModel = _context.Accounts.FirstOrDefault() ?? new AccountModel();
        if (_accountModel.Id == 0)
        {
            _context.Accounts.Add(_accountModel);
            _context.SaveChanges();
        }
    }
    
    /*public decimal Total => _accountModel.Balance;*/
    public void DepositFunds(decimal amount) 
    {
        _accountModel.Balance += amount;
        var transaction = new Transaction(DateTime.UtcNow, amount, "deposited", _accountModel.Balance);
        _transactionStore.AddTransaction(transaction, _accountModel.Id);
        _context.SaveChanges();
        Console.WriteLine(transaction.GetStatementLine());
    } 

    public void WithdrawFunds(decimal amount) 
    {
        if (amount > _accountModel.Balance) {
            Console.WriteLine("Insufficient Funds");
        }

        else {
            _accountModel.Balance -= amount;
            var transaction = new Transaction(DateTime.UtcNow, amount, "withdrew", _accountModel.Balance);
            _transactionStore.AddTransaction(transaction, _accountModel.Id);
            _context.SaveChanges();
            Console.WriteLine(transaction.GetStatementLine());
        }
    }

    public string GetStatement()
    {
        var statement = "Transaction History:\n";
        var transactions = _transactionStore.Transactions.Where(t => t.AccountId == _accountModel.Id).ToList();
        foreach (var transaction in transactions)
        {
            statement += $"{transaction.Id}. {transaction.DateCreated}: {transaction.Operation}: £{transaction.Amount:F2}, New Balance: £{_accountModel.Balance:F2}\n";
        }
        statement += $"Current Balance: £{_accountModel.Balance:F2}";
        return statement;
    }
    
    public decimal GetCurrentBalance => _accountModel.Balance;
}