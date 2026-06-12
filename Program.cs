using CSharpMoneyLab.Helpers;
using CSharpMoneyLab.Models;
using CSharpMoneyLab.Services;

Console.WriteLine("======================================");
Console.WriteLine("CSharp Money Lab · Paso 15");
Console.WriteLine("record: DTO ligero para métricas");
Console.WriteLine("======================================");

Console.WriteLine();

SeedDataService seedDataService = new SeedDataService();
TransactionService transactionService = new TransactionService();
DashboardReportService dashboardReportService = new DashboardReportService();
DashboardMetricService dashboardMetricService = new DashboardMetricService();

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

Console.WriteLine("Dashboard metric cards:");

foreach (DashboardMetric metric in metrics)
{
    Console.WriteLine("--------------------------------------");
    Console.WriteLine($"Label: {metric.Label}");
    Console.WriteLine($"Value: {metric.Value}");
    Console.WriteLine($"Description: {metric.Description}");
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
Console.WriteLine("type DashboardMetric = { label: string; value: string; description: string };");
Console.WriteLine("const metric = { label: 'Total balance', value: '$1,900.00', description: '...' };");

Console.WriteLine();
Console.WriteLine("C# equivalent:");
Console.WriteLine("public record DashboardMetric(string Label, string Value, string Description);");
Console.WriteLine("DashboardMetric metric = new DashboardMetric(\"Total balance\", \"1,900.00 MXN\", \"...\");");
