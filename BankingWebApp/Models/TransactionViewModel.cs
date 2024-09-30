using System.ComponentModel.DataAnnotations;

namespace BankingWebApp.Models;

public class TransactionViewModel
{
    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Action is required.")]
    [RegularExpression("Deposit|Withdraw", ErrorMessage = "Action must be Deposit or Withdraw.")]
    public string Action { get; set; }
    
    public string? ErrorMessage { get; set; }
    
    public string? SuccessMessage { get; set; }
    
    public decimal Balance { get; set; }
}