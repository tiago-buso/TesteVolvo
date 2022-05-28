using TesteVolvo.DTOs;
using TesteVolvo.Models;

namespace TesteVolvo.Services
{
    public interface IMapperBaseTruckModelService
    {        
        BaseTruckModelDto ConvertBaseTruckModelInBaseTruckModelDto(BaseTruckModel baseTruckModel);
        IEnumerable<BaseTruckModelDto> ConvertBaseTruckModelsInBaseTruckModelDtos(IEnumerable<BaseTruckModel> baseTruckModels);
    }
}