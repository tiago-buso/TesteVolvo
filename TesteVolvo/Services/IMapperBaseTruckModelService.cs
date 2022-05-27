using TesteVolvo.DTOs;
using TesteVolvo.Models;

namespace TesteVolvo.Services
{
    public interface IMapperBaseTruckModelService
    {
        BaseTruckModel ConvertBaseTruckModelDtoInBaseTruckModel(BaseTruckModelDto baseTruckModelDto);
        BaseTruckModelDto ConvertBaseTruckModelInBaseTruckModelDto(BaseTruckModel baseTruckModel);
        IEnumerable<BaseTruckModelDto> ConvertBaseTruckModelsInBaseTruckModelDtos(IEnumerable<BaseTruckModel> baseTruckModels);
    }
}