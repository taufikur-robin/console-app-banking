namespace Banking;
public class Program
{
    public const string Withdraw = "withdraw";
    public const string Deposit = "withdraw";
    public const string Statement = "statement";
    public const string Exit = "exit";
    static void Main(string[] args)
	{
        var transactionStore = new TransactionStore();
        var account = new Account(transactionStore);
        while (true) 
        {
            var userInput = GetInputFromUser();
            if (userInput == Deposit || userInput == Withdraw)
            {
                decimal amount = GetAmountFromUser(userInput);
                if (userInput == Deposit)
                {
                    account.DepositFunds(amount);
                }
                else
                {
                    account.WithdrawFunds(amount);
                }
            } 
            else if (userInput == Statement)
            {
                Console.WriteLine(account.GetStatement());
            }
            if (userInput == Exit)
            {
            break;
            }
        }
    }

    public static decimal GetAmountFromUser(string userInput)
    {
        decimal number;
        while (true) {
            Console.WriteLine($"Enter the amount you want to {userInput}");
            string input = Console.ReadLine() ?? "";
            if (input != null && decimal.TryParse(input, out number))
            {
                break;
            }
            else 
            {
                Console.WriteLine("Invalid input, please enter a valid number");
            }
        }
        return number;
    }
    
     public static string GetInputFromUser()
    {
        Console.WriteLine($"What would you like to do?\n{Deposit} | {Withdraw} | {Statement} | {Exit}");
        var result = Console.ReadLine() ?? "";
        return result.ToLower();
    } 
}