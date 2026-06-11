using CSharpMoneyLab.Enums;

namespace CSharpMoneyLab.Models;

public class AccountSummary
{
    public string AccountNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string Currency { get; set; } = "MXN";
    public bool IsAccountActive { get; set; }
    public int TransactionCount { get; set; }
    public int CompletedTransactionCount { get; set; }
    public int PendingTransactionCount { get; set; }
    public int RejectedTransactionCount { get; set; }
    public decimal TotalDeposits { get; set; }
    public decimal TotalWithdrawals { get; set; }
    public decimal Balance { get; set; }
    public RiskLevel RiskLevel { get; set; }
}
