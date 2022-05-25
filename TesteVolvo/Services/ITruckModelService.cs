using TesteVolvo.DTOs;

namespace TesteVolvo.Services
{
    public interface ITruckModelService
    {
        IEnumerable<TruckModelDto> GetAllTruckModels();
        TruckModelDto GetTruckModelById(int id);
        bool DeleteTruckModel(TruckModelDto truckModelDto);
        bool CheckIfCanDeleteTruckModel(int truckModelId);
        bool CreateTruckModel(TruckModelDto truckModelDto);
        bool UpdateTruckModel(TruckModelDto truckModelDto);
    }
}