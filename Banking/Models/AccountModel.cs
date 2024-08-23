using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Banking.Models;

public class AccountModel
{
    public int Id { get; set; }
    
    [Column(TypeName = "decimal(10,2)")] public decimal Balance { get; set; } = 0;
}