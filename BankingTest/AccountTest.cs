/*using NUnit.Framework;
using Banking;
using System;
using System.IO;
using System.Collections.Generic;

namespace BankingTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        Account.Total = 0;
        Account.TransactionHistory = new List<string>();
    }

    [Test]
    public void Deposit_SHOULD_add_to_total_and_display_transaction()
    {
        // arrange
        var output = new StringWriter();
        Console.SetOut(output);

        // act
        Account.Deposit(100);

        // assert
        var expectedOutput = "Deposited: £100.00, New Balance: £100.00\n";
        Assert.That(expectedOutput, Is.EqualTo(output.ToString()));
    }

    [Test]
    public void Withdraw_WHERE_insufficient_funds_SHOULD_display_insufficent_funds_message()
    {
        // arrange
        var output = new StringWriter();
        Console.SetOut(output);
        Account.Deposit(100);

        // act
        Account.WithdrawFunds(150);

        // assert
        var expectedOutput = "Deposited: £100.00, New Balance: £100.00\nInsufficient Funds\n";
        Assert.That(expectedOutput, Is.EqualTo(output.ToString()));
    }

    [Test]
    public void Withdraw_WHERE_sufficient_funds_SHOULD_display_transaction()
    {
        // arrange
        var output = new StringWriter();
        Console.SetOut(output);
        Account.Deposit(100);

        // act
        Account.WithdrawFunds(50);

        // assert
        var expectedOutput = "Deposited: £100.00, New Balance: £100.00\nWithdrew: £50.00, New Balance: £50.00\n";
        Assert.That(expectedOutput, Is.EqualTo(output.ToString()));
    }

    [Test]
    public void GetStatement_SHOULD_return_correct_statement()
    {
        // arrange
        Account.Deposit(100m);
        Account.WithdrawFunds(50m);

        var expectedStatement = "Transaction History:\n" +
                                "Deposited: £100.00, New Balance: £100.00\n" +
                                "Withdrew: £50.00, New Balance: £50.00\n" +
                                "Current Balance: £50.00";

        // act
        var statement = Account.GetStatement();

        // assert
        Assert.That(expectedStatement, Is.EqualTo(statement));
    }

    [TestCase("100", 100)]
    [TestCase("200.50", 200.50)]
    [TestCase("0", 0)]
    public void GetAmountFromUser_WHERE_input_is_valid_SHOULD_return_valid_decimal (string simulatedInput, decimal expected)
    {
        // arrange
        var input = new StringReader(simulatedInput);
        Console.SetIn(input);

        // act
        var actual = Account.GetAmountFromUser("test");

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("Deposit", "deposit")]
    [TestCase("Withdraw", "withdraw")]
    [TestCase("PrintStatement", "printstatement")]
    public void GetInputFromUser_SHOULD_return_either_deposit_withdraw_or_print_statement (string simulatedInput, string expected)
    {
        // arrange
        var input = new StringReader(simulatedInput);
        Console.SetIn(input);

        // act
        string actual = Account.GetInputFromUser();

        // assert
        Assert.That(actual, Is.EqualTo(expected));
            
    }

}*/