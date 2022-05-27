using AutoMapper;
using TesteVolvo.DTOs;
using TesteVolvo.Models;

namespace TesteVolvo.Services
{
    public class MapperBaseTruckModelService : IMapperBaseTruckModelService
    {
        private readonly IMapper _mapper;

        public MapperBaseTruckModelService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<BaseTruckModelDto> ConvertBaseTruckModelsInBaseTruckModelDtos(IEnumerable<BaseTruckModel> baseTruckModels)
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

        public BaseTruckModelDto ConvertBaseTruckModelInBaseTruckModelDto(BaseTruckModel baseTruckModel)
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

        public BaseTruckModel ConvertBaseTruckModelDtoInBaseTruckModel(BaseTruckModelDto baseTruckModelDto)
        {
            if (CheckIfExistsBaseTruckModelDto(baseTruckModelDto))
            {
                return _mapper.Map<BaseTruckModel>(baseTruckModelDto);
            }

            return null;
        }

        private bool CheckIfExistsBaseTruckModelDto(BaseTruckModelDto baseTruckModelDto)
        {
            return baseTruckModelDto != null;
        }
    }
}
