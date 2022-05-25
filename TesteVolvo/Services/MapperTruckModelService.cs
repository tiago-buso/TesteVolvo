using AutoMapper;
using TesteVolvo.DTOs;
using TesteVolvo.Models;

namespace TesteVolvo.Services
{
    public class MapperTruckModelService : IMapperTruckModelService
    {
        private readonly IMapper _mapper;

        public MapperTruckModelService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<TruckModelDto> ConvertTruckModelsInTruckModelDtos(IEnumerable<TruckModel> truckModels)
        {
            if (CheckIfExistsAnyTruckModels(truckModels))
            {
                return _mapper.Map<IEnumerable<TruckModelDto>>(truckModels);
            }

            return null;
        }

        private bool CheckIfExistsAnyTruckModels(IEnumerable<TruckModel> truckModels)
        {
            return truckModels != null && truckModels.Any();
        }

        public TruckModelDto ConvertTruckModelInTruckModelDto(TruckModel truckModel)
        {
            if (CheckIfExistsTruckModel(truckModel))
            {
                return _mapper.Map<TruckModelDto>(truckModel);
            }

            return null;
        }

        private bool CheckIfExistsTruckModel(TruckModel truckModel)
        {
            return truckModel != null;
        }

        public TruckModel ConvertTruckModelDtoInTruckModel(TruckModelDto truckModelDto)
        {
            if (CheckIfExistsTruckModelDto(truckModelDto))
            {
                return _mapper.Map<TruckModel>(truckModelDto);
            }

            return null;
        }

        private bool CheckIfExistsTruckModelDto(TruckModelDto truckModelDto)
        {
            return truckModelDto != null;
        }      
    }
}
