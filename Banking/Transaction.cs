namespace Banking;
public class Transaction
{
    public DateTime DateTime { get; }
    public decimal Amount { get; }
    public string Operation { get; }
    public decimal Balance { get; }

    public Transaction(DateTime dateTime, decimal amount, string operation, decimal balance)
    {
        DateTime = dateTime;
        Amount = amount;
        Operation = operation;
        Balance = balance;
    }

    public string GetStatementLine()
    {
        return $"{DateTime}: {Operation}: £{Amount:F2}, New Balance: £{Balance:F2}";
    }
}