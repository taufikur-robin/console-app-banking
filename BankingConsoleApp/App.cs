using System.Text;
using System.Text.Json;

namespace BankingConsoleApp;

public class App
{
    private const string Withdraw = "withdraw";
    private const string Deposit = "deposit";
    private const string Statement = "statement";
    private const string Exit = "exit";
    
    private readonly HttpClient _httpClient;

    public App(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("BankingApi");
    }

    public void Run()
    {
        Task.Run(async () => await RunAsync()).Wait();
    }

    private async Task RunAsync()
    {
        await DisplayBalanceAsync();

        while (true)
        {
            var userInput = GetInputFromUser();
            
            if (userInput == Deposit || userInput == Withdraw)
            {
                var amount = GetAmountFromUser(userInput);

                if (userInput == Deposit)
                {
                    await DepositFundsAsync(amount);
                }
                else
                {
                    await WithdrawFundsAsync(amount);
                }
                await DisplayBalanceAsync();
            }
            else if (userInput == Statement)
            {
                await DisplayStatementAsync();
            }

            if (userInput == Exit)
            {
                break;
            }
        }
    }
    
    private async Task DisplayBalanceAsync()
    {
        var response = await _httpClient.GetAsync("/api/account/balance");
        if (response.IsSuccessStatusCode)
        {
            var balanceString = await response.Content.ReadAsStringAsync();
            var balance = decimal.Parse(balanceString);
            Console.WriteLine($"Current Account Balance: £{balance:F2}");
        }
        else
        {
            Console.WriteLine("Failed to retrieve balance");
        }
    }
    
    private async Task DepositFundsAsync(decimal amount)
    {
        var content = new StringContent(JsonSerializer.Serialize(amount), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/api/account/deposit", content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Deposit successful.");
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error: {errorMessage}");
        }
    }
    
    private async Task WithdrawFundsAsync(decimal amount)
    {
        var content = new StringContent(JsonSerializer.Serialize(amount), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/api/account/withdraw", content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Withdrawal successful.");
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error: {errorMessage}");
        }
    }
    
    private async Task DisplayStatementAsync()
    {
        var response = await _httpClient.GetAsync("/api/account/statement");
        if (response.IsSuccessStatusCode)
        {
            var statement = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Transaction History:");
            Console.WriteLine(statement);
        }
        else
        {
            Console.WriteLine("Failed to retrieve statement");
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