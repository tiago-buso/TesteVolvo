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
        private readonly IBaseTruckModelService _baseTruckModelService;

        public TruckModelsController(ITruckModelService truckModelService, INotyfService notyf, IBaseTruckModelService baseTruckModelService) : base(notyf)
        {
            _truckModelService = truckModelService;
            _notyf = notyf;
            _baseTruckModelService = baseTruckModelService;
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

        public IActionResult Delete(int? id)
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
        public IActionResult DeleteConfirmed(int id)
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
    
        public IActionResult Create()
        {            
            TruckModelCreateDto truckModelCreateDto = CreateTruckModelCreateDtoObjectForCreateView();          

            if (!truckModelCreateDto.IsValid)
            {
                WriteErrorMessage(truckModelCreateDto.Notifications.First(x => x.Key == "listBaseTruckModelReadDto").Message);
                return RedirectToAction(nameof(Index));
            }

            return View(truckModelCreateDto);
        }

        private TruckModelCreateDto CreateTruckModelCreateDtoObjectForCreateView()
        {
            var baseTruckModelList = _baseTruckModelService.GetAllBaseTruckModels();
            TruckModelCreateDto truckModelCreateDto = new TruckModelCreateDto(baseTruckModelList);          
            return truckModelCreateDto;
        }      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BaseTruckModelReadDtoId,YearOfModel")] TruckModelCreateDto truckModelCreateDto)
        {
            if (ValidateTruckModelCreateDto(truckModelCreateDto))
            {
                if (_truckModelService.CreateTruckModel(truckModelCreateDto))
                {
                    WriteSuccessMessage("Modelo de caminhão criado com sucesso");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    WriteErrorMessage("Foi encontrado um erro criar um modelo de caminhão");
                    return Create();
                }                
            }
            else
            {
                return Create();
            }
            
        }

        private bool ValidateTruckModelCreateDto(TruckModelCreateDto truckModelCreateDto)
        {
            truckModelCreateDto.Validate();
            if (!truckModelCreateDto.IsValid)
            {
                WriteErrorMultipleNotifications(truckModelCreateDto.Notifications);
                return false;
            }

            return true;
        }
    }
}
