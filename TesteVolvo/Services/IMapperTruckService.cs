using TesteVolvo.DTOs;
using TesteVolvo.Models;

namespace TesteVolvo.Services
{
    public interface IMapperTruckService
    {
        Truck ConvertTruckDtoInTruck(TruckDto truckDto);
        TruckDto ConvertTruckInTruckDto(Truck truck);
        IEnumerable<TruckDto> ConvertTrucksInTruckDtos(IEnumerable<Truck> truck);
    }
}