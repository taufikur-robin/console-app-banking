namespace BankingWebApp.Services.Interfaces;

public interface IApiConfiguration
{
	string? BaseUrl { get;}
	string? BalanceUrl { get; }
	string? DepositUrl { get; }
	string? WithdrawUrl { get; }
	string? StatementUrl { get; }
}