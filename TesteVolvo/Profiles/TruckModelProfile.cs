using AutoMapper;
using TesteVolvo.Models;
using TesteVolvo.DTOs;

namespace TesteVolvo.Profiles
{
    public class TruckModelProfile : Profile
    {
        public TruckModelProfile()
        {
            // Source - Target

            CreateMap<TruckModel, TruckModelReadDto>().ForMember(dest => dest.BaseTruckModelReadDto, o => o.MapFrom(src => src.BaseTruckModel ));
            CreateMap<TruckModelReadDto, TruckModel>();
            CreateMap<TruckModelCreateDto, TruckModel>().ForMember(dest => dest.BaseTruckModel, o => o.MapFrom(src => src.BaseTruckModelReadDto));
        }
    }
}
