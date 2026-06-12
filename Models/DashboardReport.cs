namespace CSharpMoneyLab.Models;

public class DashboardReport
{
    public int TotalAccounts { get; set; }
    public int ActiveAccounts { get; set; }
    public int InactiveAccounts { get; set; }
    public int TotalTransactions { get; set; }
    public decimal TotalDeposits { get; set; }
    public decimal TotalWithdrawals { get; set; }
    public decimal TotalBalance { get; set; }
    public int LowRiskAccounts { get; set; }
    public int MediumRiskAccounts { get; set; }
    public int HighRiskAccounts { get; set; }
    public List<AccountSummary> AccountSummaries { get; set; } = new();
}
