using CSharpMoneyLab.Enums;
using CSharpMoneyLab.Helpers;
using CSharpMoneyLab.Models;
using CSharpMoneyLab.Services;

Console.WriteLine("======================================");
Console.WriteLine("CSharp Money Lab · Paso 17");
Console.WriteLine("Null safety: Account? y fallback");
Console.WriteLine("======================================");

Console.WriteLine();

SeedDataService seedDataService = new SeedDataService();
TransactionService transactionService = new TransactionService();
DashboardReportService dashboardReportService = new DashboardReportService();
DashboardMetricService dashboardMetricService = new DashboardMetricService();
RiskDistributionService riskDistributionService = new RiskDistributionService();
AccountLookupService accountLookupService = new AccountLookupService();

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
Dictionary<RiskLevel, int> riskDistribution =
    riskDistributionService.BuildRiskDistribution(summaries);

Console.WriteLine("Account lookup demo:");
Console.WriteLine("--------------------------------------");

Account? existingAccount = accountLookupService.FindByAccountNumber(
    accounts,
    "ACC-1001"
);

Account? missingAccount = accountLookupService.FindByAccountNumber(
    accounts,
    "ACC-9999"
);

Console.WriteLine($"Existing account customer: {accountLookupService.GetCustomerNameOrDefault(existingAccount)}");
Console.WriteLine($"Existing account active: {accountLookupService.IsAccountActive(existingAccount)}");

Console.WriteLine();

Console.WriteLine($"Missing account customer: {accountLookupService.GetCustomerNameOrDefault(missingAccount)}");
Console.WriteLine($"Missing account active: {accountLookupService.IsAccountActive(missingAccount)}");

Console.WriteLine();
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

foreach (KeyValuePair<RiskLevel, int> item in riskDistribution)
{
    Console.WriteLine("--------------------------------------");
    Console.WriteLine($"Risk: {FormatHelper.FormatRiskLevel(item.Key)}");
    Console.WriteLine($"Accounts: {item.Value}");
}

Console.WriteLine();
Console.WriteLine("JS/TS mental model:");
Console.WriteLine("const account = accounts.find(account => account.accountNumber === 'ACC-9999');");
Console.WriteLine("const customerName = account?.customerName ?? 'Unknown customer';");

Console.WriteLine();
Console.WriteLine("C# equivalent:");
Console.WriteLine("Account? account = accounts.FirstOrDefault(account => account.AccountNumber == \"ACC-9999\");");
Console.WriteLine("string customerName = account?.CustomerName ?? \"Unknown customer\";");
