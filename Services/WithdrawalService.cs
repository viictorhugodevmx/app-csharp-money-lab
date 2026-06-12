using CSharpMoneyLab.Enums;
using CSharpMoneyLab.Models;

namespace CSharpMoneyLab.Services;

public class WithdrawalService
{
    private readonly AccountLookupService _accountLookupService;
    private readonly TransactionService _transactionService;

    public WithdrawalService(
        AccountLookupService accountLookupService,
        TransactionService transactionService
    )
    {
        _accountLookupService = accountLookupService;
        _transactionService = transactionService;
    }

    public OperationResult<Transaction> TryCreateWithdrawal(
        List<Account> accounts,
        List<Transaction> transactions,
        string accountNumber,
        decimal amount
    )
    {
        Account? account = _accountLookupService.FindByAccountNumber(
            accounts,
            accountNumber
        );

        if (account is null)
        {
            return OperationResult<Transaction>.Fail("Account not found.");
        }

        if (!account.IsActive)
        {
            return OperationResult<Transaction>.Fail("Account is inactive.");
        }

        if (amount <= 0)
        {
            return OperationResult<Transaction>.Fail("Withdrawal amount must be greater than zero.");
        }

        List<Transaction> accountTransactions = _transactionService.GetTransactionsByAccount(
            transactions,
            account.AccountNumber
        );

        decimal balance = _transactionService.GetBalance(accountTransactions);

        if (balance < amount)
        {
            return OperationResult<Transaction>.Fail("Insufficient balance.");
        }

        Transaction withdrawal = new Transaction
        {
            Id = Guid.NewGuid(),
            AccountNumber = account.AccountNumber,
            Type = TransactionType.Withdrawal,
            Status = TransactionStatus.Completed,
            Amount = amount,
            Currency = account.Currency,
            CreatedAt = DateTime.UtcNow
        };

        return OperationResult<Transaction>.Ok(
            withdrawal,
            "Withdrawal created successfully."
        );
    }
}
