using Microsoft.AspNetCore.Mvc;
using Banking;
using BankingWebApp.Models;

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
        var model = new TransactionViewModel
        {
            Balance = balance
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult HandleTransaction(TransactionViewModel model)
    {
        var account = _accountService.GetAccount();
        model.Balance = _accountService.GetBalance(account);
        
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        try
        {
            if (model.Action == "Deposit")
            {
                _accountService.DepositFunds(account, model.Amount);
                model.SuccessMessage = $"Successfully deposited £{model.Amount:F2}.";
            }
            else if (model.Action == "Withdraw")
            {
                _accountService.WithdrawFunds(account, model.Amount);
                model.SuccessMessage = $"Successfully withdrew £{model.Amount:F2}.";
            }
        }
        catch (InvalidOperationException exception)
        {
            model.ErrorMessage = exception.Message;
            return View("Index", model);
        }
        
        model.Balance = _accountService.GetBalance(account);
        
        return View("Index", model);
    }

    public IActionResult Statement()
    {
        var account = _accountService.GetAccount();
        var statement = _accountService.GetStatement(account);
        return View("Statement", statement);
    }
}