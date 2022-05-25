using AutoMapper;
using TesteVolvo.Data;
using TesteVolvo.DTOs;
using TesteVolvo.Models;

namespace TesteVolvo.Services
{
    public class BaseTruckModelService : IBaseTruckModelService
    {
        private readonly IBaseTruckModelRepository _baseTruckModelRepository;
        private readonly IMapper _mapper;

        public BaseTruckModelService(IBaseTruckModelRepository baseTruckModelRepository, IMapper mapper)
        {
            _baseTruckModelRepository = baseTruckModelRepository;
            _mapper = mapper;
        }

        public IEnumerable<BaseTruckModelDto> GetAllBaseTruckModels()
        {
            IEnumerable<BaseTruckModel> baseTruckModels = _baseTruckModelRepository.GetAllBaseTruckModels();

            return ConvertBaseTruckModelsInBaseTruckModelReadDtos(baseTruckModels);
        }

        private IEnumerable<BaseTruckModelDto> ConvertBaseTruckModelsInBaseTruckModelReadDtos(IEnumerable<BaseTruckModel> baseTruckModels)
        {
            if (CheckIfExistsAnyBaseTruckModels(baseTruckModels))
            {
                return _mapper.Map<IEnumerable<BaseTruckModelDto>>(baseTruckModels);
            }

            return null;
        }

        private bool CheckIfExistsAnyBaseTruckModels(IEnumerable<BaseTruckModel> baseTruckModels)
        {
            return baseTruckModels != null && baseTruckModels.Any();
        }

        public BaseTruckModelDto GetBaseTruckModelById(int id)
        {
            BaseTruckModel baseTruckModel = _baseTruckModelRepository.GetBaseTruckModelById(id);

            return ConvertBaseTruckModelInBaseTruckModelReadDto(baseTruckModel);
        }

        private BaseTruckModelDto ConvertBaseTruckModelInBaseTruckModelReadDto(BaseTruckModel baseTruckModel)
        {
            if (CheckIfExistsBaseTruckModel(baseTruckModel))
            {
                return _mapper.Map<BaseTruckModelDto>(baseTruckModel);
            }

            return null;
        }

        private bool CheckIfExistsBaseTruckModel(BaseTruckModel baseTruckModel)
        {
            return baseTruckModel != null;
        }
    }
}
