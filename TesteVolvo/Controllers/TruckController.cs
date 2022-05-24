using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using TesteVolvo.Controllers.Base;

namespace TesteVolvo.Controllers
{
    public class TruckController : BaseControllerWithMessages
    {
        private readonly INotyfService _notyf;

        public TruckController(INotyfService notyf) : base(notyf)
        {
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
