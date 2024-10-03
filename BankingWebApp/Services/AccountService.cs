using BankingWebApp.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace BankingWebApp.Services;

public class AccountService : IAccountService
{
	private readonly HttpClient _httpClient;
	private readonly IApiConfiguration _apiConfiguration;

	public AccountService(IHttpClientFactory httpClientFactory, IApiConfiguration apiConfiguration)
	{
		_httpClient = httpClientFactory.CreateClient("BankingApi");
		_apiConfiguration = apiConfiguration;
		
	}

	public async Task<decimal> GetBalanceAsync()
	{
		var response = await _httpClient.GetAsync(_apiConfiguration.BalanceUrl);
		response.EnsureSuccessStatusCode();

		var balanceString = await response.Content.ReadAsStringAsync();
		return decimal.Parse(balanceString);
	}
	
	public async Task<string> HandleTransactionAsync(decimal amount, string action)
	{
		var apiUrl = action == "Deposit" ? _apiConfiguration.DepositUrl : _apiConfiguration.WithdrawUrl;
		var content = new StringContent(JsonSerializer.Serialize(amount), Encoding.UTF8, "application/json");

		var response = await _httpClient.PostAsync(apiUrl, content);
		if (!response.IsSuccessStatusCode)
		{
			return await response.Content.ReadAsStringAsync();
		}
        
		return null;
	}
	
	public async Task<string> GetStatementAsync()
	{
		var response = await _httpClient.GetAsync(_apiConfiguration.StatementUrl);
		response.EnsureSuccessStatusCode();

		return await response.Content.ReadAsStringAsync();
	}
}