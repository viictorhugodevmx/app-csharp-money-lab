namespace CSharpMoneyLab.Models;

public class Account
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string Currency { get; set; } = "MXN";
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}
