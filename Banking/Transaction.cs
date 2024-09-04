namespace Banking;
using System.ComponentModel.DataAnnotations.Schema;
public class Transaction
{
    public int Id { get; set; }
    
    public int AccountId { get; set; }
    
    public DateTime DateCreated { get; set; }
    
    public string? Operation  { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
    
    [NotMapped]
    public decimal Balance { get; set; }
    
    public Transaction() {}
    
}