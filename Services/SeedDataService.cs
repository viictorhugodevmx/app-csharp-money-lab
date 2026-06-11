using CSharpMoneyLab.Enums;
using CSharpMoneyLab.Models;

namespace CSharpMoneyLab.Services;

public class SeedDataService
{
    public List<Account> GetAccounts()
    {
        return new List<Account>
        {
            new Account
            {
                Id = Guid.NewGuid(),
                AccountNumber = "ACC-1001",
                CustomerName = "Víctor Hugo Segundo Aguilar",
                Currency = "MXN",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new Account
            {
                Id = Guid.NewGuid(),
                AccountNumber = "ACC-1002",
                CustomerName = "Cliente Demo Fintech",
                Currency = "MXN",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new Account
            {
                Id = Guid.NewGuid(),
                AccountNumber = "ACC-1003",
                CustomerName = "Cliente Riesgo Demo",
                Currency = "MXN",
                CreatedAt = DateTime.UtcNow,
                IsActive = false
            }
        };
    }

    public List<Transaction> GetTransactions()
    {
        return new List<Transaction>
        {
            new Transaction
            {
                Id = Guid.NewGuid(),
                AccountNumber = "ACC-1001",
                Type = TransactionType.Deposit,
                Status = TransactionStatus.Completed,
                Amount = 1500.75m,
                Currency = "MXN",
                CreatedAt = DateTime.UtcNow
            },
            new Transaction
            {
                Id = Guid.NewGuid(),
                AccountNumber = "ACC-1001",
                Type = TransactionType.Withdrawal,
                Status = TransactionStatus.Completed,
                Amount = 320.50m,
                Currency = "MXN",
                CreatedAt = DateTime.UtcNow
            },
            new Transaction
            {
                Id = Guid.NewGuid(),
                AccountNumber = "ACC-1001",
                Type = TransactionType.Withdrawal,
                Status = TransactionStatus.Completed,
                Amount = 180.25m,
                Currency = "MXN",
                CreatedAt = DateTime.UtcNow
            },
            new Transaction
            {
                Id = Guid.NewGuid(),
                AccountNumber = "ACC-1002",
                Type = TransactionType.Deposit,
                Status = TransactionStatus.Pending,
                Amount = 2750.00m,
                Currency = "MXN",
                CreatedAt = DateTime.UtcNow
            },
            new Transaction
            {
                Id = Guid.NewGuid(),
                AccountNumber = "ACC-1002",
                Type = TransactionType.Deposit,
                Status = TransactionStatus.Completed,
                Amount = 900.00m,
                Currency = "MXN",
                CreatedAt = DateTime.UtcNow
            },
            new Transaction
            {
                Id = Guid.NewGuid(),
                AccountNumber = "ACC-1003",
                Type = TransactionType.Withdrawal,
                Status = TransactionStatus.Rejected,
                Amount = 1250.00m,
                Currency = "MXN",
                CreatedAt = DateTime.UtcNow
            }
        };
    }
}
