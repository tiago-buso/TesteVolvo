using TesteVolvo.DTOs;

namespace TesteVolvo.Services
{
    public interface IBaseTruckModelService
    {
        IEnumerable<BaseTruckModelDto> GetAllBaseTruckModels();
        BaseTruckModelDto GetBaseTruckModelById(int id);
    }
}