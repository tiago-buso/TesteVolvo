using Microsoft.AspNetCore.Mvc;

namespace TesteVolvo.Controllers
{
    public class TruckModelsController : Controller
    {
        //private readonly ITruckModelService _truckModelService;        

        //public TruckModelsController(ITruckModelService truckModelService)
        //{
        //    _truckModelService = truckModelService;            
        //}

        public IActionResult Index()
        {
            return View();
        }
    }
}
