using AutoMapper;
using TesteVolvo.Data;
using TesteVolvo.Models;
using TesteVolvo.DTOs;

namespace TesteVolvo.Services
{
    public class TruckModelService : ITruckModelService
    {
        private readonly ITruckModelRepository _truckModelRepository;
        private readonly ITruckRepository _truckRepository;
        private readonly IMapper _mapper;
        private readonly IMapperTruckModelService _mapperTruckModelService;

        public TruckModelService(ITruckModelRepository truckModelRepository, ITruckRepository truckRepository, IMapper mapper, IMapperTruckModelService mapperTruckModelService)
        {
            _truckModelRepository = truckModelRepository;
            _truckRepository = truckRepository;
            _mapper = mapper;
            _mapperTruckModelService = mapperTruckModelService;
        }

        public IEnumerable<TruckModelDto> GetAllTruckModels()
        {
            IEnumerable<TruckModel> truckModels = _truckModelRepository.GetAllTruckModels();

            return _mapperTruckModelService.ConvertTruckModelsInTruckModelDtos(truckModels);
        }       

        public TruckModelDto GetTruckModelById(int id)
        {
            TruckModel truckModel = _truckModelRepository.GetTruckModelById(id);

            return _mapperTruckModelService.ConvertTruckModelInTruckModelDto(truckModel);
        }        

        public bool DeleteTruckModel(TruckModelDto TruckModelDto)
        {
            TruckModel truckModel = _mapperTruckModelService.ConvertTruckModelDtoInTruckModel(TruckModelDto);

            if (truckModel != null)
            {
                _truckModelRepository.DeleteTruckModel(truckModel);
                return _truckModelRepository.SaveChanges();
            }            

            return false;
        }       

        public bool CheckIfCanDeleteTruckModel(int truckModelId)
        {
            return _truckRepository.GetCountOfTrucksWithSpecificTruckModel(truckModelId) == 0;
        }

        public bool CreateTruckModel(TruckModelDto truckModelDto)
        {
            TruckModel truckModel = _mapperTruckModelService.ConvertTruckModelDtoInTruckModel(truckModelDto);

            if (truckModel != null)
            {
                _truckModelRepository.CreateTruckModel(truckModel);            
                return _truckModelRepository.SaveChanges();
            }

            return false;
        }

        public bool UpdateTruckModel(TruckModelDto truckModelDto)
        {
            TruckModel truckModel = _mapperTruckModelService.ConvertTruckModelDtoInTruckModel(truckModelDto);

            if (truckModel != null)
            {
                _truckModelRepository.UpdateTruckModel(truckModel);
                return _truckModelRepository.SaveChanges();
            }

            return false;
        }
    }
}
