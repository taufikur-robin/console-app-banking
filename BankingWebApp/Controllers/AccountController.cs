using Microsoft.AspNetCore.Mvc;
using Banking;

namespace BankingWebApp.Controllers;

public class AccountController : Controller
{
    private readonly Account _account;

    public AccountController(Account account)
    {
        _account = account;
    }

    public IActionResult Index()
    {
        var balance = _account.GetCurrentBalance;
        return View(balance);
    }

    [HttpPost]
    public IActionResult Deposit(decimal amount)
    {
        _account.DepositFunds(amount);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Withdraw(decimal amount)
    {
        _account.WithdrawFunds(amount);
        return RedirectToAction("Index");
    }

    public IActionResult Statement()
    {
        var statement = _account.GetStatement();
        return View("Statement", statement);
    }
}