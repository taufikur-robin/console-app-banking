using Microsoft.AspNetCore.Mvc;
using Banking;

namespace BankingWebApp.Controllers;

public class AccountController : Controller
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    public IActionResult Index()
    {
        var account = _accountService.GetAccount();
        var balance = _accountService.GetBalance(account);
        return View(balance);
    }

    [HttpPost]
    public IActionResult Deposit(decimal amount)
    {
        var account = _accountService.GetAccount();
        _accountService.DepositFunds(account, amount);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Withdraw(decimal amount)
    {
        var account = _accountService.GetAccount();
        _accountService.WithdrawFunds(account, amount);
        return RedirectToAction("Index");
    }

    public IActionResult Statement()
    {
        var account = _accountService.GetAccount();
        var statement = _accountService.GetStatement(account);
        return View("Statement", statement);
    }
}