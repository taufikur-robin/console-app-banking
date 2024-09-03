﻿using Banking.Data;

namespace Banking;
public abstract class Program
{
    private const string Withdraw = "withdraw";
    private const string Deposit = "deposit";
    private const string Statement = "statement";
    private const string Exit = "exit";

    private static void Main(string[] args)
    {
        var context = new BankingContext();
        var transactionStore = new TransactionStore(context);
        var accountService = new AccountService(context, transactionStore);
        var account = accountService.GetAccount();
        
        Console.WriteLine($"Current Account Balance: £{accountService.GetBalance:F2}");
        
        while (true) 
        {
            var userInput = GetInputFromUser();
            if (userInput == Deposit || userInput == Withdraw)
            {
                var amount = GetAmountFromUser(userInput);
                if (userInput == Deposit)
                {
                    accountService.DepositFunds(account, amount);
                }
                else
                {
                    accountService.WithdrawFunds(account, amount);
                }
            } 
            else if (userInput == Statement)
            {
                Console.WriteLine(accountService.GetStatement(account));
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