using CSharpMoneyLab.Enums;
using CSharpMoneyLab.Models;

namespace CSharpMoneyLab.Services;

public class DashboardReportService
{
    public DashboardReport BuildReport(List<AccountSummary> summaries)
    {
        return new DashboardReport
        {
            TotalAccounts = summaries.Count,
            ActiveAccounts = summaries.Count(summary => summary.IsAccountActive),
            InactiveAccounts = summaries.Count(summary => !summary.IsAccountActive),
            TotalTransactions = summaries.Sum(summary => summary.TransactionCount),
            TotalDeposits = summaries.Sum(summary => summary.TotalDeposits),
            TotalWithdrawals = summaries.Sum(summary => summary.TotalWithdrawals),
            TotalBalance = summaries.Sum(summary => summary.Balance),
            LowRiskAccounts = summaries.Count(summary => summary.RiskLevel == RiskLevel.Low),
            MediumRiskAccounts = summaries.Count(summary => summary.RiskLevel == RiskLevel.Medium),
            HighRiskAccounts = summaries.Count(summary => summary.RiskLevel == RiskLevel.High),
            AccountSummaries = summaries
        };
    }
}
