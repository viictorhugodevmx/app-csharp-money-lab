using CSharpMoneyLab.Models;

namespace CSharpMoneyLab.Services;

public class AccountLookupService
{
    public Account? FindByAccountNumber(
        List<Account> accounts,
        string accountNumber
    )
    {
        return accounts.FirstOrDefault(account =>
            account.AccountNumber == accountNumber
        );
    }

    public string GetCustomerNameOrDefault(Account? account)
    {
        return account?.CustomerName ?? "Unknown customer";
    }

    public bool IsAccountActive(Account? account)
    {
        return account?.IsActive ?? false;
    }
}
