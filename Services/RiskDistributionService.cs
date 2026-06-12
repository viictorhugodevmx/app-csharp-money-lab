using CSharpMoneyLab.Enums;
using CSharpMoneyLab.Models;

namespace CSharpMoneyLab.Services;

public class RiskDistributionService
{
    public Dictionary<RiskLevel, int> BuildRiskDistribution(List<AccountSummary> summaries)
    {
        Dictionary<RiskLevel, int> distribution = new()
        {
            { RiskLevel.Low, 0 },
            { RiskLevel.Medium, 0 },
            { RiskLevel.High, 0 }
        };

        foreach (AccountSummary summary in summaries)
        {
            distribution[summary.RiskLevel]++;
        }

        return distribution;
    }
}
