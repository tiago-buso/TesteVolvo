using Microsoft.AspNetCore.Mvc;
using TesteVolvo.DTOs;
using TesteVolvo.Services;

namespace TesteVolvo.Controllers
{
    public class TruckModelsController : Controller
    {
        private readonly ITruckModelService _truckModelService;

        public TruckModelsController(ITruckModelService truckModelService)
        {
            _truckModelService = truckModelService;
        }

        public IActionResult Index()
        {
            IEnumerable<TruckModelReadDto> truckModels = _truckModelService.GetAllTruckModels();
            return View(truckModels);
        }
    }
}
