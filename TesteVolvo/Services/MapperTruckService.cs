using AutoMapper;
using TesteVolvo.DTOs;
using TesteVolvo.Models;

namespace TesteVolvo.Services
{
    public class MapperTruckService : IMapperTruckService
    {
        private readonly IMapper _mapper;

        public MapperTruckService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<TruckDto> ConvertTrucksInTruckDtos(IEnumerable<Truck> trucks)
        {
            if (CheckIfExistsAnyTruck(trucks))
            {
                return _mapper.Map<IEnumerable<TruckDto>>(trucks);
            }

            return null;
        }

        private bool CheckIfExistsAnyTruck(IEnumerable<Truck> truck)
        {
            return truck != null && truck.Any();
        }

        public TruckDto ConvertTruckInTruckDto(Truck truck)
        {
            if (CheckIfExistsTruck(truck))
            {
                return _mapper.Map<TruckDto>(truck);
            }

            return null;
        }

        private bool CheckIfExistsTruck(Truck truck)
        {
            return truck != null;
        }

        public Truck ConvertTruckDtoInTruck(TruckDto truckDto)
        {
            if (CheckIfExistsTruckDto(truckDto))
            {
                return _mapper.Map<Truck>(truckDto);
            }

            return null;
        }

        private bool CheckIfExistsTruckDto(TruckDto truckDto)
        {
            return truckDto != null;
        }
    }
}
