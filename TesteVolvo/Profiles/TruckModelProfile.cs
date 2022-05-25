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

            CreateMap<TruckModel, TruckModelDto>()
                .ForMember(dest => dest.BaseTruckModelDto, o => o.MapFrom(src => src.BaseTruckModel ))
                .ForMember(dest => dest.BaseTruckModelDtoId, o => o.MapFrom(src => src.BaseTruckModelId)); 
            
            CreateMap<TruckModelDto, TruckModel>()
                .ForMember(dest => dest.BaseTruckModel, o => o.MapFrom(src => src.BaseTruckModelDto))
                .ForMember(dest => dest.BaseTruckModelId, o => o.MapFrom(src => src.BaseTruckModelDtoId));
        }
    }
}
