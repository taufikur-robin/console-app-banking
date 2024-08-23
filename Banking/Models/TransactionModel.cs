using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.Models;

public class TransactionModel
{
    public int Id { get; set; }
    
    public int AccountId { get; set; }
    
    public DateTime DateCreated { get; set; }
    
    public string? Operation  { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
    
}