using CSharpMoneyLab.Helpers;
using CSharpMoneyLab.Models;
using CSharpMoneyLab.Services;

Console.WriteLine("======================================");
Console.WriteLine("CSharp Money Lab · Paso 16");
Console.WriteLine("Dictionary: distribución por riesgo");
Console.WriteLine("======================================");

Console.WriteLine();

SeedDataService seedDataService = new SeedDataService();
TransactionService transactionService = new TransactionService();
DashboardReportService dashboardReportService = new DashboardReportService();
DashboardMetricService dashboardMetricService = new DashboardMetricService();
RiskDistributionService riskDistributionService = new RiskDistributionService();

List<Account> accounts = seedDataService.GetAccounts();
List<Transaction> transactions = seedDataService.GetTransactions();

List<AccountSummary> summaries = new();

foreach (Account account in accounts)
{
    AccountSummary summary = transactionService.BuildAccountSummary(
        account,
        transactions
    );

    summaries.Add(summary);
}

DashboardReport report = dashboardReportService.BuildReport(summaries);
List<DashboardMetric> metrics = dashboardMetricService.BuildMetrics(report);
Dictionary<CSharpMoneyLab.Enums.RiskLevel, int> riskDistribution =
    riskDistributionService.BuildRiskDistribution(summaries);

Console.WriteLine("Dashboard metric cards:");

foreach (DashboardMetric metric in metrics)
{
    Console.WriteLine("--------------------------------------");
    Console.WriteLine($"Label: {metric.Label}");
    Console.WriteLine($"Value: {metric.Value}");
    Console.WriteLine($"Description: {metric.Description}");
}

Console.WriteLine();
Console.WriteLine("Risk distribution:");

foreach (KeyValuePair<CSharpMoneyLab.Enums.RiskLevel, int> item in riskDistribution)
{
    Console.WriteLine("--------------------------------------");
    Console.WriteLine($"Risk: {FormatHelper.FormatRiskLevel(item.Key)}");
    Console.WriteLine($"Accounts: {item.Value}");
}

Console.WriteLine();
Console.WriteLine("Account summary cards:");

foreach (AccountSummary summary in report.AccountSummaries)
{
    Console.WriteLine("--------------------------------------");
    Console.WriteLine($"Customer: {summary.CustomerName}");
    Console.WriteLine($"Account: {summary.AccountNumber}");
    Console.WriteLine($"Risk: {FormatHelper.FormatRiskLevel(summary.RiskLevel)}");
    Console.WriteLine($"Balance: {FormatHelper.FormatMoney(summary.Balance, summary.Currency)}");
}

Console.WriteLine();
Console.WriteLine("JS/TS mental model:");
Console.WriteLine("const riskDistribution = { Low: 1, Medium: 1, High: 1 };");
Console.WriteLine("Object.entries(riskDistribution).forEach(([risk, count]) => console.log(risk, count));");

Console.WriteLine();
Console.WriteLine("C# equivalent:");
Console.WriteLine("Dictionary<RiskLevel, int> riskDistribution = new();");
Console.WriteLine("foreach (KeyValuePair<RiskLevel, int> item in riskDistribution) { ... }");
