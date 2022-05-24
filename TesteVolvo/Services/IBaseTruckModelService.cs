using TesteVolvo.DTOs;

namespace TesteVolvo.Services
{
    public interface IBaseTruckModelService
    {
        IEnumerable<BaseTruckModelReadDto> GetAllBaseTruckModels();
        BaseTruckModelReadDto GetBaseTruckModelById(int id);
    }
}