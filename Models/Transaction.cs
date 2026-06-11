using CSharpMoneyLab.Enums;

namespace CSharpMoneyLab.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public TransactionType Type { get; set; }
    public TransactionStatus Status { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "MXN";
    public DateTime CreatedAt { get; set; }
}
