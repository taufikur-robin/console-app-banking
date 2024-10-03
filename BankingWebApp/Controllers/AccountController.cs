using Microsoft.AspNetCore.Mvc;
using BankingWebApp.Models;
using BankingWebApp.Services.Interfaces;

namespace BankingWebApp.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task <IActionResult> Index()
    {
        var model = await InitializeTransactionViewModel();
        model.SuccessMessage = TempData["SuccessMessage"] as string;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> HandleTransaction(TransactionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var initializedModel = await InitializeTransactionViewModel();
            initializedModel.Amount = model.Amount;
            initializedModel.Action = model.Action;
            return View("Index", initializedModel);
        }
        
        var errorMessage = await _accountService.HandleTransactionAsync(model.Amount, model.Action);
        var initializedModelWithBalance = await InitializeTransactionViewModel();

        if (errorMessage != null)
        {
            initializedModelWithBalance.ErrorMessage = errorMessage;
            initializedModelWithBalance.Amount = model.Amount;
            initializedModelWithBalance.Action = model.Action;
            return View("Index", initializedModelWithBalance);
        }
        
        TempData["SuccessMessage"] = $"{model.Action} of £{model.Amount:F2} successful.";
        return RedirectToAction("Index");
    }
    
    public async Task <IActionResult> Statement()
    {
        var statement = await _accountService.GetStatementAsync();
        return View("Statement", statement);
    }
    
    private async Task<TransactionViewModel> InitializeTransactionViewModel()
    {
        var balance = await _accountService.GetBalanceAsync();
        return new TransactionViewModel
        {
            Balance = balance,
        };
    }
}