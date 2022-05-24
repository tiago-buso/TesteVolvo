using TesteVolvo.DTOs;

namespace TesteVolvo.Services
{
    public interface ITruckModelService
    {
        IEnumerable<TruckModelReadDto> GetAllTruckModels();
        TruckModelReadDto GetTruckModelById(int id);
        bool DeleteTruckModel(TruckModelReadDto truckModelReadDto);
        bool CheckIfCanDeleteTruckModel(int truckModelId);
    }
}