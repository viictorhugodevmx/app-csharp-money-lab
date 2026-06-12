using CSharpMoneyLab.Helpers;
using CSharpMoneyLab.Models;
using CSharpMoneyLab.Services;

Console.WriteLine("======================================");
Console.WriteLine("CSharp Money Lab · Paso 19");
Console.WriteLine("try/catch: errores esperados vs inesperados");
Console.WriteLine("======================================");

Console.WriteLine();

SeedDataService seedDataService = new SeedDataService();
TransactionService transactionService = new TransactionService();
AccountLookupService accountLookupService = new AccountLookupService();

WithdrawalService withdrawalService = new WithdrawalService(
    accountLookupService,
    transactionService
);

List<Account> accounts = seedDataService.GetAccounts();
List<Transaction> transactions = seedDataService.GetTransactions();

Console.WriteLine("Withdrawal attempts:");
Console.WriteLine("--------------------------------------");

OperationResult<Transaction> successfulWithdrawal = withdrawalService.TryCreateWithdrawal(
    accounts,
    transactions,
    "ACC-1001",
    250.00m
);

PrintWithdrawalResult("Valid withdrawal", successfulWithdrawal);

OperationResult<Transaction> insufficientBalance = withdrawalService.TryCreateWithdrawal(
    accounts,
    transactions,
    "ACC-1002",
    5000.00m
);

PrintWithdrawalResult("Insufficient balance", insufficientBalance);

OperationResult<Transaction> missingAccount = withdrawalService.TryCreateWithdrawal(
    accounts,
    transactions,
    "ACC-9999",
    100.00m
);

PrintWithdrawalResult("Missing account", missingAccount);

OperationResult<Transaction> inactiveAccount = withdrawalService.TryCreateWithdrawal(
    accounts,
    transactions,
    "ACC-1003",
    100.00m
);

PrintWithdrawalResult("Inactive account", inactiveAccount);

OperationResult<Transaction> invalidAmount = withdrawalService.TryCreateWithdrawal(
    accounts,
    transactions,
    "ACC-1001",
    0.00m
);

PrintWithdrawalResult("Invalid amount", invalidAmount);

OperationResult<Transaction> unexpectedError = withdrawalService.TryCreateWithdrawal(
    accounts,
    transactions,
    "ACC-1001",
    9999.00m
);

PrintWithdrawalResult("Unexpected provider error", unexpectedError);

Console.WriteLine();
Console.WriteLine("Error handling rule:");
Console.WriteLine("Business validation errors -> OperationResult.Fail(...)");
Console.WriteLine("Unexpected technical errors -> catch (Exception ex)");

Console.WriteLine();
Console.WriteLine("JS/TS mental model:");
Console.WriteLine("try { createWithdrawal(); } catch (error) { return { success: false, message: error.message }; }");

Console.WriteLine();
Console.WriteLine("C# equivalent:");
Console.WriteLine("try { ... } catch (Exception ex) { return OperationResult<Transaction>.Fail(ex.Message); }");

static void PrintWithdrawalResult(
    string scenario,
    OperationResult<Transaction> result
)
{
    Console.WriteLine("--------------------------------------");
    Console.WriteLine($"Scenario: {scenario}");
    Console.WriteLine($"Success: {result.Success}");
    Console.WriteLine($"Message: {result.Message}");

    if (result.Data is not null)
    {
        Console.WriteLine($"Transaction Id: {result.Data.Id}");
        Console.WriteLine($"Amount: {FormatHelper.FormatMoney(result.Data.Amount, result.Data.Currency)}");
        Console.WriteLine($"Type: {result.Data.Type}");
        Console.WriteLine($"Status: {result.Data.Status}");
    }
}
