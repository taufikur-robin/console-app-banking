using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using BankingWebApp.Models;

namespace BankingWebApp.Controllers;

public class AccountController : Controller
{
    private readonly HttpClient _httpClient;

    public AccountController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("BankingApi");
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

        var apiUrl = model.Action == "Deposit" ? "/api/account/deposit" : "/api/account/withdraw";
        var content = new StringContent(JsonSerializer.Serialize(model.Amount), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(apiUrl, content);
        
        var initializedModelWithBalance = await InitializeTransactionViewModel();

        if (!response.IsSuccessStatusCode)
        {
            initializedModelWithBalance.ErrorMessage = await response.Content.ReadAsStringAsync();
            initializedModelWithBalance.Amount = model.Amount;
            initializedModelWithBalance.Action = model.Action;
            return View("Index", initializedModelWithBalance);
        }
       
        var balanceResponse = await _httpClient.GetAsync("/api/account/balance");
        balanceResponse.EnsureSuccessStatusCode();
        var updatedBalance = await balanceResponse.Content.ReadAsStringAsync();
        model.Balance = decimal.Parse(updatedBalance);
        
        TempData["SuccessMessage"] = $"{model.Action} of £{model.Amount:F2} successful.";
        return RedirectToAction("Index");
        
        
    }
    
    public async Task <IActionResult> Statement()
    {
        var response = await _httpClient.GetAsync("/api/account/statement");
        response.EnsureSuccessStatusCode();
        
        var statement = await response.Content.ReadAsStringAsync();
    
        return View("Statement", statement);
    }
    
    private async Task<TransactionViewModel> InitializeTransactionViewModel()
    {
        var response = await _httpClient.GetAsync("/api/account/balance");
        response.EnsureSuccessStatusCode();

        var balance = await response.Content.ReadAsStringAsync();
        return new TransactionViewModel
        {
            Balance = decimal.Parse(balance),
        };
    }
}