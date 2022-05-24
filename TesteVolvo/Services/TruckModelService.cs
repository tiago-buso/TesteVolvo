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

        public TruckModelService(ITruckModelRepository truckModelRepository, ITruckRepository truckRepository, IMapper mapper)
        {
            _truckModelRepository = truckModelRepository;
            _truckRepository = truckRepository;
            _mapper = mapper;
        }

        public IEnumerable<TruckModelReadDto> GetAllTruckModels()
        {
            IEnumerable<TruckModel> truckModels = _truckModelRepository.GetAllTruckModels();

            return ConvertTruckModelsInTruckModelReadDtos(truckModels);
        }

        private IEnumerable<TruckModelReadDto> ConvertTruckModelsInTruckModelReadDtos(IEnumerable<TruckModel> truckModels)
        {
            if (CheckIfExistsAnyTruckModels(truckModels))
            {
                return _mapper.Map<IEnumerable<TruckModelReadDto>>(truckModels);
            }

            return null;
        }

        private bool CheckIfExistsAnyTruckModels(IEnumerable<TruckModel> truckModels)
        {
            return truckModels != null && truckModels.Any();
        }

        public TruckModelReadDto GetTruckModelById(int id)
        {
            TruckModel truckModel = _truckModelRepository.GetTruckModelById(id);

            return ConvertTruckModelInTruckModelReadDto(truckModel);
        }

        private TruckModelReadDto ConvertTruckModelInTruckModelReadDto(TruckModel truckModel)
        {
            if (CheckIfExistsTruckModel(truckModel))
            {
                return _mapper.Map<TruckModelReadDto>(truckModel);
            }

            return null;
        }

        private bool CheckIfExistsTruckModel(TruckModel truckModel)
        {
            return truckModel != null;
        }

        public bool DeleteTruckModel(TruckModelReadDto truckModelReadDto)
        {
            TruckModel truckModel = ConvertTruckModelReadDtolInTruckMode(truckModelReadDto);

            if (truckModel != null)
            {
                _truckModelRepository.DeleteTruckModel(truckModel);
                return _truckModelRepository.SaveChanges();
            }            

            return false;
        }

        private TruckModel ConvertTruckModelReadDtolInTruckMode(TruckModelReadDto truckModelReadDto)
        {
            if (CheckIfExistsTruckModelReadDto(truckModelReadDto))
            {
                return _mapper.Map<TruckModel>(truckModelReadDto);
            }

            return null;
        }

        private bool CheckIfExistsTruckModelReadDto(TruckModelReadDto truckModelReadDto)
        {
            return truckModelReadDto != null;
        }

        public bool CheckIfCanDeleteTruckModel(int truckModelId)
        {
            return _truckRepository.GetCountOfTrucksWithSpecificTruckModel(truckModelId) == 0;
        }

        //public bool UpdateTruckModel(TruckModelViewModel truckModelViewModel)
        //{
        //    if (IsValidTruckModelViewModel(truckModelViewModel))
        //    {
        //        TruckModel truckModel = ConvertTruckModelViewModelInTruckModel(truckModelViewModel);
        //        _truckModelRepository.UpdateTruckModel(truckModel);
        //        return _truckModelRepository.SaveChanges();
        //    }

        //}

        //private bool IsValidTruckModelViewModel(TruckModelViewModel truckModelViewModel)
        //{
        //    throw new NotImplementedException();
        //}

        //private TruckModel ConvertTruckModelViewModelInTruckModel(TruckModelViewModel truckModelViewModel)
        //{
        //    return _mapper.Map<TruckModel>(truckModelViewModel);
        //}


    }
}
