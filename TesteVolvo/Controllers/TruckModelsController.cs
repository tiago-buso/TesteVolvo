using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
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
            try
            {
                IEnumerable<TruckModelDto> truckModels = _truckModelService.GetAllTruckModels();
                return View(truckModels);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento dos modelos de caminhão: {autoMapperException.Message}");
                return RedirectToAction(nameof(Index), nameof(HomeController));
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao carregar todos os modelos de caminhão: {ex.Message}");
                return RedirectToAction(nameof(Index), nameof(HomeController));
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

                var truckModel = _truckModelService.GetTruckModelById(id.Value);

                if (truckModel == null)
                {
                    return NotFound();
                }

                return View(truckModel);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento dos modelos de caminhão: {autoMapperException.Message}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao carregar os detalhes de modelo de caminhão: {ex.Message}");
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

                var truckModel = _truckModelService.GetTruckModelById(id.Value);
                if (truckModel == null)
                {
                    return NotFound();
                }

                return View(truckModel);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento dos modelos de caminhão: {autoMapperException.Message}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao carregar os detalhes de modelo de caminhão: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
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
            catch (AutoMapperMappingException autoMapperException)
            {
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento dos modelos de caminhão: {autoMapperException.Message}");
                return RedirectToAction(nameof(Delete));
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao excluir este modelo de caminhão: {ex.Message}");
                return RedirectToAction(nameof(Delete));
            }
        }
    
        public IActionResult Create()
        {
            try
            {
                TruckModelDto truckModelDto = new TruckModelDto();

                CreateTruckModelDtoObjectForCreateView(truckModelDto);

                if (!truckModelDto.IsValid)
                {
                    WriteErrorMessage(truckModelDto.Notifications.First().Message);
                    return RedirectToAction(nameof(Index));
                }

                return View(truckModelDto);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento dos modelos de caminhão: {autoMapperException.Message}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao carregar a tela de criação de modelo de caminhão: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        private TruckModelDto CreateTruckModelDtoObjectForCreateView(TruckModelDto truckModelDto)
        {
            var baseTruckModelList = _baseTruckModelService.GetAllBaseTruckModels();
            truckModelDto.AddListBaseTruckModelDto(baseTruckModelList);          
            return truckModelDto;
        }      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BaseTruckModelDtoId,YearOfModel")] TruckModelDto truckModelDto)
        {
            try
            {
                if (ValidateTruckModelDto(truckModelDto))
                {                  

                    if (_truckModelService.CreateTruckModel(truckModelDto))
                    {
                        WriteSuccessMessage("Modelo de caminhão criado com sucesso");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        WriteErrorMessage("Foi encontrado um erro criar um modelo de caminhão");
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
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento dos modelos de caminhão: {autoMapperException.Message}");
                return RedirectToAction(nameof(Create));
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao criar o modelo de caminhão: {ex.Message}");
                return RedirectToAction(nameof(Create));
            }

        }

        private bool ValidateTruckModelDto(TruckModelDto truckModelDto)
        {
            truckModelDto.Validate();
            if (!truckModelDto.IsValid)
            {
                WriteErrorMultipleNotifications(truckModelDto.Notifications);
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

            var truckModelDto = _truckModelService.GetTruckModelById(id.Value);
            if (truckModelDto == null)
            {
                return NotFound();
            }

            CreateTruckModelDtoObjectForCreateView(truckModelDto);

            if (!truckModelDto.IsValid)
            {
                WriteErrorMessage(truckModelDto.Notifications.First().Message);
                return RedirectToAction(nameof(Index));
            }
            
            return View(truckModelDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,BaseTruckModelDtoId,YearOfModel")] TruckModelDto truckModelDto)
        {
            if (id != truckModelDto.Id)
            {
                return NotFound();
            }

            try
            {
                if (ValidateTruckModelDto(truckModelDto))
                {
                    if (_truckModelService.UpdateTruckModel(truckModelDto))
                    {
                        WriteSuccessMessage("Modelo de caminhão atualizado com sucesso");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        WriteErrorMessage("Foi encontrado um erro ao atualizar um modelo de caminhão");
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
                WriteErrorMessage($"Ocorreu erro ao realizar o mapeamento dos modelos de caminhão: {autoMapperException.Message}");
                return RedirectToAction(nameof(Edit));
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Ocorreu erro ao atualizar o modelo de caminhão: {ex.Message}");
                return RedirectToAction(nameof(Edit));
            }
        }
    }
}
