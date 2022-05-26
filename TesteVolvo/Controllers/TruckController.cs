using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TesteVolvo.Controllers.Base;
using TesteVolvo.DTOs;
using TesteVolvo.Services;

namespace TesteVolvo.Controllers
{
    public class TruckController : BaseControllerWithMessages
    {
        private readonly ITruckModelService _truckModelService;
        private readonly ITruckService _truckService;
        private readonly INotyfService _notyf;

        public TruckController(ITruckModelService truckModelService, ITruckService truckService, INotyfService notyf) : base(notyf)
        {
            _truckModelService = truckModelService;
            _truckService = truckService;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            try
            {
                IEnumerable<TruckDto> trucks = _truckService.GetAllTrucks();
                return View(trucks);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento dos registros de caminhão: {autoMapperException.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao carregar todos os registros de caminhão: {ex.Message}");
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var truck = _truckService.GetTruckById(id.Value);

                if (truck == null)
                {
                    return NotFound();
                }

                return View(truck);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento do registro de caminhão: {autoMapperException.Message}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao carregar os detalhes do registro de caminhão: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var truck = _truckService.GetTruckById(id.Value);
                if (truck == null)
                {
                    return NotFound();
                }

                return View(truck);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento do registro de caminhão: { autoMapperException.Message}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao carregar os detalhes do registro de caminhão: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {                
                var truck = _truckService.GetTruckById(id);
                bool sucesso = _truckService.DeleteTruck(truck);

                if (sucesso)
                {
                    WriteSuccessMessage("Caminhão excluído com sucesso");
                }
                else
                {
                    WriteErrorMessage($"Ocorreu erro ao excluir este caminhão");
                }

                return RedirectToAction(nameof(Index));
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento do registro de caminhão: {autoMapperException.Message}");
                return RedirectToAction(nameof(Delete));
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao excluir este caminhão: {ex.Message}");
                return RedirectToAction(nameof(Delete));
            }
        }

        public IActionResult Create()
        {
            try
            {
                TruckDto truckDto = new TruckDto();

                CreateTruckDtoObjectForCreateView(truckDto);

                if (!truckDto.IsValid)
                {
                    WriteErrorMessage(truckDto.Notifications.First().Message);
                    return RedirectToAction(nameof(Index));
                }

                return View(truckDto);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento do registro de caminhão: {autoMapperException.Message}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao carregar a tela de criação de caminhão: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        private void CreateTruckDtoObjectForCreateView(TruckDto truckDto)
        {
            var truckModelList = _truckModelService.GetAllTruckModels();
            truckDto.AddListTruckModelDto(truckModelList);          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TruckModelDtoId,YearOfManufacture")] TruckDto truckDto)
        {
            try
            {
                if (ValidateTruckDto(truckDto))
                {

                    if (_truckService.CreateTruck(truckDto))
                    {
                        WriteSuccessMessage("Caminhão criado com sucesso");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        WriteErrorMessage("Foi encontrado um erro criar um caminhão");
                        return RedirectToAction(nameof(Create));
                    }
                }
                else
                {
                    return RedirectToAction(nameof(Create));
                }
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento do registro de caminhão: {autoMapperException.Message}");
                return RedirectToAction(nameof(Create));
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao criar caminhão: {ex.Message}");
                return RedirectToAction(nameof(Create));
            }

        }

        private bool ValidateTruckDto(TruckDto truckDto)
        {
            truckDto.Validate();
            if (!truckDto.IsValid)
            {
                WriteErrorMultipleNotifications(truckDto.Notifications);
                return false;
            }

            return true;
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckDto = _truckService.GetTruckById(id.Value);
            if (truckDto == null)
            {
                return NotFound();
            }

            CreateTruckDtoObjectForCreateView(truckDto);

            if (!truckDto.IsValid)
            {
                WriteErrorMessage(truckDto.Notifications.First().Message);
                return RedirectToAction(nameof(Index));
            }

            return View(truckDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,TruckModelDtoId,YearOfManufacture")] TruckDto truckDto)
        {
            if (id != truckDto.Id)
            {
                return NotFound();
            }

            try
            {
                if (ValidateTruckDto(truckDto))
                {
                    if (_truckService.UpdateTruck(truckDto))
                    {
                        WriteSuccessMessage("Caminhão atualizado com sucesso");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        WriteErrorMessage("Foi encontrado um erro ao atualizar um caminhão");
                        return RedirectToAction(nameof(Edit));
                    }
                }
                else
                {
                    return RedirectToAction(nameof(Edit));
                }
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento do registro de caminhão: {autoMapperException.Message}");
                return RedirectToAction(nameof(Edit));
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao atualizar caminhão: {ex.Message}");
                return RedirectToAction(nameof(Edit));
            }
        }
    }
}
