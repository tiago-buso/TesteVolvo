using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using TesteVolvo.Controllers.Base;
using TesteVolvo.DTOs;
using TesteVolvo.Services;

namespace TesteVolvo.Controllers
{
    public class TruckModelsController : BaseControllerWithMessages
    {
        private readonly ITruckModelService _truckModelService;
        private readonly INotyfService _notyf;


        public TruckModelsController(ITruckModelService truckModelService, INotyfService notyf) : base(notyf)
        {
            _truckModelService = truckModelService;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            IEnumerable<TruckModelReadDto> truckModels = _truckModelService.GetAllTruckModels();
            return View(truckModels);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckModel = _truckModelService.GetTruckModelById(id.Value);

            if (truckModel == null)
            {
                return NotFound();
            }

            return View(truckModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckModel = _truckModelService.GetTruckModelById(id.Value);
            if (truckModel == null)
            {
                return NotFound();
            }

            return View(truckModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_truckModelService.CheckIfCanDeleteTruckModel(id))
            {
                WriteErrorMessage("Não é possível excluir um modelo de caminhão que já tem um caminhão cadastrado");
                return RedirectToAction(nameof(Index));
            }

            var truckModel = _truckModelService.GetTruckModelById(id);
            bool sucesso = _truckModelService.DeleteTruckModel(truckModel);

            if (sucesso)
            {
                WriteSuccessMessage("Modelo excluído com sucesso");
            }
            
            return RedirectToAction(nameof(Index));
        }       
    }
}
