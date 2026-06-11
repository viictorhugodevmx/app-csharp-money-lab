using CSharpMoneyLab.Helpers;
using CSharpMoneyLab.Models;
using CSharpMoneyLab.Services;

Console.WriteLine("======================================");
Console.WriteLine("CSharp Money Lab · Paso 13");
Console.WriteLine("FormatHelper: formato para UI/reportes");
Console.WriteLine("======================================");

Console.WriteLine();

SeedDataService seedDataService = new SeedDataService();
TransactionService transactionService = new TransactionService();
StatementService statementService = new StatementService();

List<Account> accounts = seedDataService.GetAccounts();
List<Transaction> transactions = seedDataService.GetTransactions();

Console.WriteLine("Formatted account statements:");

foreach (Account account in accounts)
{
    AccountSummary summary = transactionService.BuildAccountSummary(
        account,
        transactions
    );

    List<StatementLine> statement = statementService.BuildStatement(
        account,
        transactions
    );

    Console.WriteLine("======================================");
    Console.WriteLine($"Customer: {summary.CustomerName}");
    Console.WriteLine($"Account: {summary.AccountNumber}");
    Console.WriteLine($"Active: {summary.IsAccountActive}");
    Console.WriteLine($"Risk: {FormatHelper.FormatRiskLevel(summary.RiskLevel)}");
    Console.WriteLine($"Balance: {FormatHelper.FormatMoney(summary.Balance, summary.Currency)}");
    Console.WriteLine($"Deposits: {FormatHelper.FormatMoney(summary.TotalDeposits, summary.Currency)}");
    Console.WriteLine($"Withdrawals: {FormatHelper.FormatMoney(summary.TotalWithdrawals, summary.Currency)}");
    Console.WriteLine("Statement:");

    foreach (StatementLine line in statement)
    {
        Console.WriteLine("--------------------------------------");
        Console.WriteLine($"Date: {FormatHelper.FormatDate(line.Date)}");
        Console.WriteLine($"Type: {line.Type}");
        Console.WriteLine($"Status: {FormatHelper.FormatStatus(line.Status)}");
        Console.WriteLine($"Amount: {FormatHelper.FormatMoney(line.Amount, line.Currency)}");
        Console.WriteLine($"Description: {line.Description}");
    }

    if (statement.Count == 0)
    {
        Console.WriteLine("No transactions found for this account.");
    }
}

Console.WriteLine();
Console.WriteLine("JS/TS mental model:");
Console.WriteLine("formatMoney(summary.balance, summary.currency)");
Console.WriteLine("formatDate(transaction.createdAt)");
Console.WriteLine("formatRiskLevel(summary.riskLevel)");

Console.WriteLine();
Console.WriteLine("C# equivalent:");
Console.WriteLine("FormatHelper.FormatMoney(summary.Balance, summary.Currency)");
Console.WriteLine("FormatHelper.FormatDate(line.Date)");
Console.WriteLine("FormatHelper.FormatRiskLevel(summary.RiskLevel)");
