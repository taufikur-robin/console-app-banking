using Banking;
using Microsoft.AspNetCore.Mvc;


namespace BankingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpGet("balance")]
    public IActionResult GetBalance()
    {
        var account = _accountService.GetAccount();
        var balance = _accountService.GetBalance(account);
        return Ok(balance);
    }

    [HttpPost("deposit")]
    public IActionResult Deposit([FromBody] decimal amount)
    {
        var account = _accountService.GetAccount();
        _accountService.DepositFunds(account, amount);
        return Ok("Deposit successful");
    }

    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] decimal amount)
    {
        var account = _accountService.GetAccount();
        try
        {
            _accountService.WithdrawFunds(account, amount);
            return Ok("Withdrawal successful");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("statement")]
    public IActionResult GetStatement()
    {
        var account = _accountService.GetAccount();
        var statement = _accountService.GetStatement(account);
        return Ok(statement);
    }
}