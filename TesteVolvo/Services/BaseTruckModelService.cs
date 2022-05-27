using AutoMapper;
using TesteVolvo.Data;
using TesteVolvo.DTOs;
using TesteVolvo.Models;

namespace TesteVolvo.Services
{
    public class BaseTruckModelService : IBaseTruckModelService
    {
        private readonly IBaseTruckModelRepository _baseTruckModelRepository;
        private readonly IMapperBaseTruckModelService _mapperBaseTruckModelService;

        public BaseTruckModelService(IBaseTruckModelRepository baseTruckModelRepository, IMapperBaseTruckModelService mapperBaseTruckModelService)
        {
            _baseTruckModelRepository = baseTruckModelRepository;
            _mapperBaseTruckModelService = mapperBaseTruckModelService;
        }

        public IEnumerable<BaseTruckModelDto> GetAllBaseTruckModels()
        {
            IEnumerable<BaseTruckModel> baseTruckModels = _baseTruckModelRepository.GetAllBaseTruckModels();

            return _mapperBaseTruckModelService.ConvertBaseTruckModelsInBaseTruckModelDtos(baseTruckModels);
        }     

        public BaseTruckModelDto GetBaseTruckModelById(int id)
        {
            BaseTruckModel baseTruckModel = _baseTruckModelRepository.GetBaseTruckModelById(id);

            return _mapperBaseTruckModelService.ConvertBaseTruckModelInBaseTruckModelDto(baseTruckModel);
        }       
    }
}
