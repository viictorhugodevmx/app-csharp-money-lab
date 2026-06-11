using CSharpMoneyLab.Enums;
using CSharpMoneyLab.Models;

namespace CSharpMoneyLab.Services;

public class TransactionService
{
    public List<Transaction> GetTransactionsByAccount(
        List<Transaction> transactions,
        string accountNumber
    )
    {
        return transactions
            .Where(transaction => transaction.AccountNumber == accountNumber)
            .ToList();
    }

    public List<Transaction> GetCompletedTransactions(List<Transaction> transactions)
    {
        return transactions
            .Where(transaction => transaction.Status == TransactionStatus.Completed)
            .ToList();
    }

    public List<Transaction> GetPendingTransactions(List<Transaction> transactions)
    {
        return transactions
            .Where(transaction => transaction.Status == TransactionStatus.Pending)
            .ToList();
    }

    public List<Transaction> GetRejectedTransactions(List<Transaction> transactions)
    {
        return transactions
            .Where(transaction => transaction.Status == TransactionStatus.Rejected)
            .ToList();
    }

    public decimal GetTotalDeposits(List<Transaction> transactions)
    {
        return transactions
            .Where(transaction =>
                transaction.Status == TransactionStatus.Completed &&
                transaction.Type == TransactionType.Deposit)
            .Sum(transaction => transaction.Amount);
    }

    public decimal GetTotalWithdrawals(List<Transaction> transactions)
    {
        return transactions
            .Where(transaction =>
                transaction.Status == TransactionStatus.Completed &&
                transaction.Type == TransactionType.Withdrawal)
            .Sum(transaction => transaction.Amount);
    }

    public decimal GetBalance(List<Transaction> transactions)
    {
        decimal totalDeposits = GetTotalDeposits(transactions);
        decimal totalWithdrawals = GetTotalWithdrawals(transactions);

        return totalDeposits - totalWithdrawals;
    }

    public RiskLevel CalculateRiskLevel(
        Account account,
        List<Transaction> accountTransactions,
        decimal balance
    )
    {
        int rejectedCount = GetRejectedTransactions(accountTransactions).Count;
        int pendingCount = GetPendingTransactions(accountTransactions).Count;

        if (!account.IsActive || rejectedCount > 0 || balance < 0)
        {
            return RiskLevel.High;
        }

        if (pendingCount > 0 || balance < 500m)
        {
            return RiskLevel.Medium;
        }

        return RiskLevel.Low;
    }

    public AccountSummary BuildAccountSummary(
        Account account,
        List<Transaction> transactions
    )
    {
        List<Transaction> accountTransactions = GetTransactionsByAccount(
            transactions,
            account.AccountNumber
        );

        List<Transaction> completedTransactions = GetCompletedTransactions(accountTransactions);
        List<Transaction> pendingTransactions = GetPendingTransactions(accountTransactions);
        List<Transaction> rejectedTransactions = GetRejectedTransactions(accountTransactions);

        decimal totalDeposits = GetTotalDeposits(accountTransactions);
        decimal totalWithdrawals = GetTotalWithdrawals(accountTransactions);
        decimal balance = GetBalance(accountTransactions);

        RiskLevel riskLevel = CalculateRiskLevel(
            account,
            accountTransactions,
            balance
        );

        return new AccountSummary
        {
            AccountNumber = account.AccountNumber,
            CustomerName = account.CustomerName,
            Currency = account.Currency,
            IsAccountActive = account.IsActive,
            TransactionCount = accountTransactions.Count,
            CompletedTransactionCount = completedTransactions.Count,
            PendingTransactionCount = pendingTransactions.Count,
            RejectedTransactionCount = rejectedTransactions.Count,
            TotalDeposits = totalDeposits,
            TotalWithdrawals = totalWithdrawals,
            Balance = balance,
            RiskLevel = riskLevel
        };
    }
}
