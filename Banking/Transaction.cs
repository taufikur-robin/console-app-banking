namespace Banking;
public class Transaction
{
    private readonly DateTime dateTime;
    private readonly decimal amount;
    private readonly string operation;
    private readonly decimal balance;
    public int id;

    public Transaction(DateTime dateTime, decimal amount, string operation, decimal balance)
    {
        this.dateTime = dateTime;
        this.amount = amount;
        this.operation = operation;
        this.balance = balance;
    }

    public string GetStatementLine()
    {
        return $"{id}. {dateTime}: {operation}: £{amount:F2}, New Balance: £{balance:F2}";
    }
}