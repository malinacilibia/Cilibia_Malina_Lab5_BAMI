using Cilibia_Malina_Lab5.Models;
using Cilibia_Malina_Lab5.Services;
using Microsoft.AspNetCore.Mvc;
using Cilibia_Malina_Lab5.Models;
using Cilibia_Malina_Lab5.Services;
using System.Threading.Tasks;
namespace Cilibia_Malina_Lab5.Controllers
{
    public class RiskPredictionController : Controller
    {
        private readonly IRiskPredictionService _riskService;
        public RiskPredictionController(IRiskPredictionService riskService)
        {
            _riskService = riskService;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var model = new RiskPredictionViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(RiskPredictionViewModel model)
        {
            if (model.InspectionType == null) model.InspectionType = "Routine";
            if (model.ViolationDescription == null) model.ViolationDescription = "Test";

            var input = new RiskInput
            {
                InspectionType = model.InspectionType,
                ViolationDescription = model.ViolationDescription
            };

            var prediction = await _riskService.PredictRiskAsync(input);

            model.PredictedRisk = prediction;
            return View(model);
        }
    }
}