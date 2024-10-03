using BankingWebApp.Services.Interfaces;

namespace BankingWebApp.Services;

public class ApiConfiguration : IApiConfiguration
{
	public string? BaseUrl { get; set; }
	public string? BalanceUrl { get; set; }
	public string? DepositUrl { get; set; }
	public string? WithdrawUrl { get; set; }
	public string? StatementUrl { get; set; }
}