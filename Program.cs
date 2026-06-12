using CSharpMoneyLab.Helpers;
using CSharpMoneyLab.Models;
using CSharpMoneyLab.Services;

Console.WriteLine("======================================");
Console.WriteLine("CSharp Money Lab · Paso 18");
Console.WriteLine("OperationResult: errores controlados");
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

Console.WriteLine();
Console.WriteLine("JS/TS mental model:");
Console.WriteLine("return { success: true, data: transaction, message: 'Withdrawal created successfully.' };");
Console.WriteLine("return { success: false, data: null, message: 'Insufficient balance.' };");

Console.WriteLine();
Console.WriteLine("C# equivalent:");
Console.WriteLine("OperationResult<Transaction>.Ok(transaction, \"Withdrawal created successfully.\");");
Console.WriteLine("OperationResult<Transaction>.Fail(\"Insufficient balance.\");");

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
