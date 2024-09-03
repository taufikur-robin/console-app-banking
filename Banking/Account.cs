using Banking.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking;
public class Account
{
    public int Id { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Balance { get; set; } = 0;

    public Account()
    {
    }
}