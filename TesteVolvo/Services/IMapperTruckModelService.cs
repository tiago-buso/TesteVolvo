using TesteVolvo.DTOs;
using TesteVolvo.Models;

namespace TesteVolvo.Services
{
    public interface IMapperTruckModelService
    {       
        TruckModel ConvertTruckModelDtoInTruckModel(TruckModelDto TruckModelDto);
        TruckModelDto ConvertTruckModelInTruckModelDto(TruckModel truckModel);
        IEnumerable<TruckModelDto> ConvertTruckModelsInTruckModelDtos(IEnumerable<TruckModel> truckModels);
    }
}