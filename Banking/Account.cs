namespace Banking;
public class Account
{
    public static decimal Total = 0;
    public static List<string> TransactionHistory = new List<string>();

    static void Main(string[] args)
	{
        while (true) 
        {
            var userInput = GetInputFromUser();
            if (userInput == "deposit" || userInput == "withdraw")
            {
                decimal amount = GetAmountFromUser(userInput);
                if (userInput == "deposit")
                {
                    Deposit(amount);
                }
                else
                {
                    Withdraw(amount);
                }
            } 
            else if (userInput == "printstatement")
            {
                Console.WriteLine(GetStatement());
            }
            if (userInput == "exit")
            {
            break;
            }
        }
    }

    public static void Deposit(decimal amount) 
    {
        Total += amount;
        var transaction = $"Deposited: £{amount:F2}, New Balance: £{Total:F2}";
        TransactionHistory.Add(transaction);
        Console.WriteLine(transaction);
    } 

    public static void Withdraw(decimal amount) 
    {
        if (amount > Total) {
            Console.WriteLine("Insufficient Funds");
        }

        else {
            Total -= amount;
            var transaction = $"Withdrew: £{amount:F2}, New Balance: £{Total:F2}";
            TransactionHistory.Add(transaction);
            Console.WriteLine(transaction);
        }
    }

    public static string GetStatement()
    {
        var statement = "Transaction History:\n";
        foreach (var transaction in TransactionHistory)
        {
            statement += transaction + "\n";
        }
        statement += $"Current Balance: £{Total:F2}";
        return statement;
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
        Console.WriteLine("What would you like to do?\n'Deposit' | 'Withdraw' | 'PrintStatement' | 'exit'");
        var result = Console.ReadLine() ?? "";
        return result.ToLower();
    }
}