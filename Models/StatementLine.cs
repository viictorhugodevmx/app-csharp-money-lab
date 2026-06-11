using CSharpMoneyLab.Enums;

namespace CSharpMoneyLab.Models;

public class StatementLine
{
    public DateTime Date { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public TransactionType Type { get; set; }
    public TransactionStatus Status { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "MXN";
    public string Description { get; set; } = string.Empty;
}
