using CSharpMoneyLab.Enums;

namespace CSharpMoneyLab.Helpers;

public static class FormatHelper
{
    public static string FormatMoney(decimal amount, string currency)
    {
        return $"{amount:N2} {currency}";
    }

    public static string FormatDate(DateTime date)
    {
        return date.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public static string FormatStatus(TransactionStatus status)
    {
        return status switch
        {
            TransactionStatus.Completed => "Completed",
            TransactionStatus.Pending => "Pending",
            TransactionStatus.Rejected => "Rejected",
            _ => "Unknown"
        };
    }

    public static string FormatRiskLevel(RiskLevel riskLevel)
    {
        return riskLevel switch
        {
            RiskLevel.Low => "Low risk",
            RiskLevel.Medium => "Medium risk",
            RiskLevel.High => "High risk",
            _ => "Unknown risk"
        };
    }
}
