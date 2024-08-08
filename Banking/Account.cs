namespace Banking;
public class Account
{
    public static decimal Total = 0;
    private readonly TransactionStore transactionStore;

    public Account(TransactionStore transactionStore)
    {
        this.transactionStore = transactionStore;
    }
    public void DepositFunds(decimal amount) 
    {
        Total += amount;
        var transaction = new Transaction(DateTime.UtcNow, amount, "deposited", Total);
        transactionStore.AddTransaction(transaction);
        Console.WriteLine(transaction.GetStatementLine());
    } 

    public void WithdrawFunds(decimal amount) 
    {
        if (amount > Total) {
            Console.WriteLine("Insufficient Funds");
        }

        else {
            Total -= amount;
            var transaction = new Transaction(DateTime.UtcNow, amount, "withdrew", Total);
            transactionStore.AddTransaction(transaction);
            Console.WriteLine(transaction.GetStatementLine());
        }
    }

    public string GetStatement()
    {
        var statement = "Transaction History:\n";
        foreach (var transaction in transactionStore.Transactions)
        {
            statement += transaction.GetStatementLine() + "\n";
        }
        statement += $"Current Balance: £{Total:F2}";
        return statement;
    }

   


}