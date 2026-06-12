using CSharpMoneyLab.Enums;
using CSharpMoneyLab.Helpers;
using CSharpMoneyLab.Models;
using CSharpMoneyLab.Services;

Console.WriteLine("======================================");
Console.WriteLine("CSharp Money Lab · Paso 20");
Console.WriteLine("Mini core fintech flow");
Console.WriteLine("======================================");

Console.WriteLine();

SeedDataService seedDataService = new SeedDataService();
AccountLookupService accountLookupService = new AccountLookupService();
TransactionService transactionService = new TransactionService();
DashboardReportService dashboardReportService = new DashboardReportService();
DashboardMetricService dashboardMetricService = new DashboardMetricService();
RiskDistributionService riskDistributionService = new RiskDistributionService();
StatementService statementService = new StatementService();

WithdrawalService withdrawalService = new WithdrawalService(
    accountLookupService,
    transactionService
);

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

Console.WriteLine("1) Dashboard metrics");
Console.WriteLine("--------------------------------------");

foreach (DashboardMetric metric in metrics)
{
    Console.WriteLine($"{metric.Label}: {metric.Value}");
    Console.WriteLine($"  {metric.Description}");
}

Console.WriteLine();
Console.WriteLine("2) Risk distribution");
Console.WriteLine("--------------------------------------");

foreach (KeyValuePair<RiskLevel, int> item in riskDistribution)
{
    Console.WriteLine($"{FormatHelper.FormatRiskLevel(item.Key)}: {item.Value}");
}

Console.WriteLine();
Console.WriteLine("3) Account lookup");
Console.WriteLine("--------------------------------------");

Account? selectedAccount = accountLookupService.FindByAccountNumber(
    accounts,
    "ACC-1001"
);

if (selectedAccount is null)
{
    Console.WriteLine("Selected account not found.");
    return;
}

Console.WriteLine($"Selected customer: {selectedAccount.CustomerName}");
Console.WriteLine($"Selected account: {selectedAccount.AccountNumber}");
Console.WriteLine($"Active: {selectedAccount.IsActive}");

Console.WriteLine();
Console.WriteLine("4) Withdrawal operation");
Console.WriteLine("--------------------------------------");

OperationResult<Transaction> withdrawalResult = withdrawalService.TryCreateWithdrawal(
    accounts,
    transactions,
    selectedAccount.AccountNumber,
    250.00m
);

Console.WriteLine($"Success: {withdrawalResult.Success}");
Console.WriteLine($"Message: {withdrawalResult.Message}");

if (withdrawalResult.Data is not null)
{
    transactions.Add(withdrawalResult.Data);

    Console.WriteLine($"New transaction: {withdrawalResult.Data.Id}");
    Console.WriteLine($"Amount: {FormatHelper.FormatMoney(withdrawalResult.Data.Amount, withdrawalResult.Data.Currency)}");
}

Console.WriteLine();
Console.WriteLine("5) Updated selected account summary");
Console.WriteLine("--------------------------------------");

AccountSummary updatedSummary = transactionService.BuildAccountSummary(
    selectedAccount,
    transactions
);

Console.WriteLine($"Customer: {updatedSummary.CustomerName}");
Console.WriteLine($"Account: {updatedSummary.AccountNumber}");
Console.WriteLine($"Balance: {FormatHelper.FormatMoney(updatedSummary.Balance, updatedSummary.Currency)}");
Console.WriteLine($"Transactions: {updatedSummary.TransactionCount}");
Console.WriteLine($"Risk: {FormatHelper.FormatRiskLevel(updatedSummary.RiskLevel)}");

Console.WriteLine();
Console.WriteLine("6) Updated statement");
Console.WriteLine("--------------------------------------");

List<StatementLine> statement = statementService.BuildStatement(
    selectedAccount,
    transactions
);

foreach (StatementLine line in statement)
{
    Console.WriteLine("--------------------------------------");
    Console.WriteLine($"Date: {FormatHelper.FormatDate(line.Date)}");
    Console.WriteLine($"Type: {line.Type}");
    Console.WriteLine($"Status: {FormatHelper.FormatStatus(line.Status)}");
    Console.WriteLine($"Amount: {FormatHelper.FormatMoney(line.Amount, line.Currency)}");
    Console.WriteLine($"Description: {line.Description}");
}

Console.WriteLine();
Console.WriteLine("Architecture summary:");
Console.WriteLine("SeedDataService        -> mock fintech data");
Console.WriteLine("AccountLookupService   -> safe account search / null safety");
Console.WriteLine("TransactionService     -> balances, summaries and risk");
Console.WriteLine("DashboardReportService -> global dashboard report");
Console.WriteLine("DashboardMetricService -> UI-ready metric cards");
Console.WriteLine("RiskDistributionService-> dictionary-based risk counts");
Console.WriteLine("WithdrawalService      -> controlled business operation");
Console.WriteLine("StatementService       -> account statement lines");
Console.WriteLine("FormatHelper           -> UI/report formatting");
Console.WriteLine("Program.cs             -> orchestrates the mini flow");

Console.WriteLine();
Console.WriteLine("JS/TS mental model:");
Console.WriteLine("mockData -> services -> result wrapper -> dashboard DTOs -> statement rows");

Console.WriteLine();
Console.WriteLine("C# equivalent:");
Console.WriteLine("SeedDataService -> Services -> OperationResult<T> -> Models/DTOs -> Program.cs");
