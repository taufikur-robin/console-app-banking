namespace BankingWebApp.Services.Interfaces;

public interface IAccountService
{
	Task<decimal> GetBalanceAsync();
	Task<string> HandleTransactionAsync(decimal amount, string action);
	Task<string> GetStatementAsync();
}