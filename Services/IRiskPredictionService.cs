using Cilibia_Malina_Lab5.Models;

namespace Cilibia_Malina_Lab5.Services
{
    public interface IRiskPredictionService
    {
        Task<string> PredictRiskAsync(RiskInput input);
    }
}
