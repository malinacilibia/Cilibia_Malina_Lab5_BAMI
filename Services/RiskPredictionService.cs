using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Cilibia_Malina_Lab5.Models;

namespace Cilibia_Malina_Lab5.Services
{
    public class RiskPredictionService : IRiskPredictionService
    {
        private readonly HttpClient _httpClient;

        public RiskPredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> PredictRiskAsync(RiskInput input)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/predict", input);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    var root = doc.RootElement;

                    if (root.TryGetProperty("predictedLabel", out var label))
                    {
                        return label.GetString();
                    }
                }

                return "Nu s-a găsit predicția.";
            }
            catch (Exception ex)
            {
                return "Eroare: " + ex.Message;
            }
        }
    }
}