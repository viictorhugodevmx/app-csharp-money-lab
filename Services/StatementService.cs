using CSharpMoneyLab.Enums;
using CSharpMoneyLab.Models;

namespace CSharpMoneyLab.Services;

public class StatementService
{
    public List<StatementLine> BuildStatement(
        Account account,
        List<Transaction> transactions
    )
    {
        return transactions
            .Where(transaction => transaction.AccountNumber == account.AccountNumber)
            .OrderBy(transaction => transaction.CreatedAt)
            .Select(transaction => new StatementLine
            {
                Date = transaction.CreatedAt,
                AccountNumber = transaction.AccountNumber,
                Type = transaction.Type,
                Status = transaction.Status,
                Amount = transaction.Amount,
                Currency = transaction.Currency,
                Description = BuildDescription(transaction)
            })
            .ToList();
    }

    private string BuildDescription(Transaction transaction)
    {
        string operation = transaction.Type == TransactionType.Deposit
            ? "Money received"
            : "Money sent";

        return transaction.Status switch
        {
            TransactionStatus.Completed => $"{operation} successfully",
            TransactionStatus.Pending => $"{operation} pending confirmation",
            TransactionStatus.Rejected => $"{operation} rejected",
            _ => "Unknown transaction status"
        };
    }
}
