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
    public IActionResult HandleTransaction(decimal amount, string action)
    {
        var account = _accountService.GetAccount();

        if (action == "Deposit")
        {
            _accountService.DepositFunds(account, amount);
        }
        else if (action == "Withdraw")
        {
            _accountService.WithdrawFunds(account, amount);
        }

        return RedirectToAction("Index");
    }

    public IActionResult Statement()
    {
        var account = _accountService.GetAccount();
        var statement = _accountService.GetStatement(account);
        return View("Statement", statement);
    }
}