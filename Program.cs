using CSharpMoneyLab.Helpers;
using CSharpMoneyLab.Models;
using CSharpMoneyLab.Services;

Console.WriteLine("======================================");
Console.WriteLine("CSharp Money Lab · Paso 14");
Console.WriteLine("DashboardReport: métricas globales");
Console.WriteLine("======================================");

Console.WriteLine();

SeedDataService seedDataService = new SeedDataService();
TransactionService transactionService = new TransactionService();
DashboardReportService dashboardReportService = new DashboardReportService();

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

Console.WriteLine("Global dashboard metrics:");
Console.WriteLine("--------------------------------------");
Console.WriteLine($"Total accounts: {report.TotalAccounts}");
Console.WriteLine($"Active accounts: {report.ActiveAccounts}");
Console.WriteLine($"Inactive accounts: {report.InactiveAccounts}");
Console.WriteLine($"Total transactions: {report.TotalTransactions}");
Console.WriteLine($"Total deposits: {FormatHelper.FormatMoney(report.TotalDeposits, "MXN")}");
Console.WriteLine($"Total withdrawals: {FormatHelper.FormatMoney(report.TotalWithdrawals, "MXN")}");
Console.WriteLine($"Total balance: {FormatHelper.FormatMoney(report.TotalBalance, "MXN")}");
Console.WriteLine($"Low risk accounts: {report.LowRiskAccounts}");
Console.WriteLine($"Medium risk accounts: {report.MediumRiskAccounts}");
Console.WriteLine($"High risk accounts: {report.HighRiskAccounts}");

Console.WriteLine();
Console.WriteLine("Account summary cards:");

foreach (AccountSummary summary in report.AccountSummaries)
{
    Console.WriteLine("--------------------------------------");
    Console.WriteLine($"Customer: {summary.CustomerName}");
    Console.WriteLine($"Account: {summary.AccountNumber}");
    Console.WriteLine($"Active: {summary.IsAccountActive}");
    Console.WriteLine($"Risk: {FormatHelper.FormatRiskLevel(summary.RiskLevel)}");
    Console.WriteLine($"Balance: {FormatHelper.FormatMoney(summary.Balance, summary.Currency)}");
}

Console.WriteLine();
Console.WriteLine("JS/TS mental model:");
Console.WriteLine("const totalBalance = summaries.reduce((sum, item) => sum + item.balance, 0);");
Console.WriteLine("const highRiskAccounts = summaries.filter(item => item.riskLevel === 'High').length;");

Console.WriteLine();
Console.WriteLine("C# equivalent:");
Console.WriteLine("decimal totalBalance = summaries.Sum(summary => summary.Balance);");
Console.WriteLine("int highRiskAccounts = summaries.Count(summary => summary.RiskLevel == RiskLevel.High);");
