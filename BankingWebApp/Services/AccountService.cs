using BankingWebApp.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace BankingWebApp.Services;

public class AccountService : IAccountService
{
	private readonly HttpClient _httpClient;
	private readonly IConfiguration _configuration;
	private readonly string? _balanceUrl;
	private readonly string? _depositUrl;
	private readonly string? _withdrawUrl;
	private readonly string? _statementUrl;

	public AccountService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
	{
		_httpClient = httpClientFactory.CreateClient("BankingApi");
		_configuration = configuration;
		
		_balanceUrl = _configuration["ApiUrls:Balance"];
		_depositUrl = _configuration["ApiUrls:Deposit"];
		_withdrawUrl = _configuration["ApiUrls:Withdraw"];
		_statementUrl = _configuration["ApiUrls:Statement"];
	}

	public async Task<decimal> GetBalanceAsync()
	{
		var response = await _httpClient.GetAsync(_balanceUrl);
		response.EnsureSuccessStatusCode();

		var balanceString = await response.Content.ReadAsStringAsync();
		return decimal.Parse(balanceString);
	}
	
	public async Task<string> HandleTransactionAsync(decimal amount, string action)
	{
		var apiUrl = action == "Deposit" ? _depositUrl : _withdrawUrl;
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
		var response = await _httpClient.GetAsync(_statementUrl);
		response.EnsureSuccessStatusCode();

		return await response.Content.ReadAsStringAsync();
	}
}