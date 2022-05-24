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

        public IEnumerable<BaseTruckModelReadDto> GetAllBaseTruckModels()
        {
            IEnumerable<BaseTruckModel> baseTruckModels = _baseTruckModelRepository.GetAllBaseTruckModels();

            return ConvertBaseTruckModelsInBaseTruckModelReadDtos(baseTruckModels);
        }

        private IEnumerable<BaseTruckModelReadDto> ConvertBaseTruckModelsInBaseTruckModelReadDtos(IEnumerable<BaseTruckModel> baseTruckModels)
        {
            if (CheckIfExistsAnyBaseTruckModels(baseTruckModels))
            {
                return _mapper.Map<IEnumerable<BaseTruckModelReadDto>>(baseTruckModels);
            }

            return null;
        }

        private bool CheckIfExistsAnyBaseTruckModels(IEnumerable<BaseTruckModel> baseTruckModels)
        {
            return baseTruckModels != null && baseTruckModels.Any();
        }

        public BaseTruckModelReadDto GetBaseTruckModelById(int id)
        {
            BaseTruckModel baseTruckModel = _baseTruckModelRepository.GetBaseTruckModelById(id);

            return ConvertBaseTruckModelInBaseTruckModelReadDto(baseTruckModel);
        }

        private BaseTruckModelReadDto ConvertBaseTruckModelInBaseTruckModelReadDto(BaseTruckModel baseTruckModel)
        {
            if (CheckIfExistsBaseTruckModel(baseTruckModel))
            {
                return _mapper.Map<BaseTruckModelReadDto>(baseTruckModel);
            }

            return null;
        }

        private bool CheckIfExistsBaseTruckModel(BaseTruckModel baseTruckModel)
        {
            return baseTruckModel != null;
        }
    }
}
