using TesteVolvo.DTOs;

namespace TesteVolvo.Services
{
    public interface ITruckService
    {
        bool CreateTruck(TruckDto truckDto);
        bool DeleteTruck(TruckDto truckDto);
        IEnumerable<TruckDto> GetAllTrucks();
        TruckDto GetTruckById(int id);
        bool UpdateTruck(TruckDto truckDto);
    }
}