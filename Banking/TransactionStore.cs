namespace Banking;
public class TransactionStore
{
    public List<Transaction> Transactions = new List<Transaction>();
    public void AddTransaction(Transaction transaction)
    {
        transaction.id = Transactions.Count() + 1;
        Transactions.Add(transaction);
    }
}