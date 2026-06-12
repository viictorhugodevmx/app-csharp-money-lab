using CSharpMoneyLab.Helpers;
using CSharpMoneyLab.Models;

namespace CSharpMoneyLab.Services;

public class DashboardMetricService
{
    public List<DashboardMetric> BuildMetrics(DashboardReport report)
    {
        return new List<DashboardMetric>
        {
            new DashboardMetric(
                "Total accounts",
                report.TotalAccounts.ToString(),
                "All accounts registered in the mini fintech core"
            ),
            new DashboardMetric(
                "Active accounts",
                report.ActiveAccounts.ToString(),
                "Accounts currently active"
            ),
            new DashboardMetric(
                "Total balance",
                FormatHelper.FormatMoney(report.TotalBalance, "MXN"),
                "Completed deposits minus completed withdrawals"
            ),
            new DashboardMetric(
                "Total transactions",
                report.TotalTransactions.ToString(),
                "All transactions across all accounts"
            ),
            new DashboardMetric(
                "High risk accounts",
                report.HighRiskAccounts.ToString(),
                "Accounts with rejected movements, inactive status or negative balance"
            )
        };
    }
}
