using Banking;
namespace BankingConsoleApp;

public class App
{
    private const string Withdraw = "withdraw";
    private const string Deposit = "deposit";
    private const string Statement = "statement";
    private const string Exit = "exit";
    
    private readonly TransactionStore _transactionStore;
    private readonly AccountService _accountService;

    public App(TransactionStore transactionStore, AccountService accountService)
    {
        _transactionStore = transactionStore;
        _accountService = accountService;
    }

    public void Run()
    {
        var account = _accountService.GetAccount();
        Console.WriteLine($"Current Account Balance: £{_accountService.GetBalance(account):F2}");

        while (true)
        {
            var userInput = GetInputFromUser();
            if (userInput == Deposit || userInput == Withdraw)
            {
                var amount = GetAmountFromUser(userInput);
                var balance = _accountService.GetBalance(account);

                if (userInput == Deposit)
                {
                    _accountService.DepositFunds(account, amount);
                    Console.WriteLine(_transactionStore.GetLastTransaction());
                }
                else if (amount > balance)
                {
                    Console.WriteLine("Insufficient funds");
                }
                else
                {
                    _accountService.WithdrawFunds(account, amount);
                    Console.WriteLine(_transactionStore.GetLastTransaction());
                }
            }
            else if (userInput == Statement)
            {
                Console.WriteLine(_accountService.GetStatement(account));
            }

            if (userInput == Exit)
            {
                break;
            }
        }
    }
    
    private static decimal GetAmountFromUser(string userInput)
    {
        decimal number;
        while (true) {
            Console.WriteLine($"Enter the amount you want to {userInput}");
            var input = Console.ReadLine() ?? "";
            if (decimal.TryParse(input, out number))
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

    private static string GetInputFromUser()
    {
        Console.WriteLine($"What would you like to do?\n{Deposit} | {Withdraw} | {Statement} | {Exit}");
        var result = Console.ReadLine() ?? "";
        return result.ToLower();
    }
}