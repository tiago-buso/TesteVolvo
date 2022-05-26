using TesteVolvo.Data;
using TesteVolvo.DTOs;
using TesteVolvo.Models;

namespace TesteVolvo.Services
{
    public class TruckService : ITruckService
    {
        private readonly ITruckRepository _truckRepository;
        private readonly IMapperTruckService _mapperTruckService;

        public TruckService(ITruckRepository truckRepository, IMapperTruckService mapperTruckService)
        {
            _truckRepository = truckRepository;
            _mapperTruckService = mapperTruckService;
        }

        public IEnumerable<TruckDto> GetAllTrucks()
        {
            IEnumerable<Truck> trucks = _truckRepository.GetAllTrucks();

            return _mapperTruckService.ConvertTrucksInTruckDtos(trucks);
        }

        public TruckDto GetTruckById(int id)
        {
            Truck truck = _truckRepository.GetTruckById(id);

            return _mapperTruckService.ConvertTruckInTruckDto(truck);
        }

        public bool DeleteTruck(TruckDto truckDto)
        {
            Truck truck = _mapperTruckService.ConvertTruckDtoInTruck(truckDto);

            if (truck != null)
            {
                _truckRepository.DeleteTruck(truck);
                return _truckRepository.SaveChanges();
            }

            return false;
        }

        public bool CreateTruck(TruckDto truckDto)
        {
            Truck truck = _mapperTruckService.ConvertTruckDtoInTruck(truckDto);

            if (truck != null)
            {
                _truckRepository.CreateTruck(truck);
                return _truckRepository.SaveChanges();
            }

            return false;
        }

        public bool UpdateTruck(TruckDto truckDto)
        {
            Truck truck = _mapperTruckService.ConvertTruckDtoInTruck(truckDto);

            if (truck != null)
            {
                _truckRepository.UpdateTruck(truck);
                return _truckRepository.SaveChanges();
            }

            return false;
        }
    }
}
