using AutoMapper;
using TesteVolvo.Models;
using TesteVolvo.DTOs;

namespace TesteVolvo.Profiles
{
    public class TruckProfile : Profile
    {
        public TruckProfile()
        {
            // Source - Target

            CreateMap<Truck, TruckDto>()
               .ForMember(dest => dest.TruckModelDto, o => o.MapFrom(src => src.TruckModel))
               .ForMember(dest => dest.TruckModelDtoId, o => o.MapFrom(src => src.TruckModelId));

            CreateMap<TruckDto, Truck>()
                .ForMember(dest => dest.TruckModel, o => o.MapFrom(src => src.TruckModelDto))
                .ForMember(dest => dest.TruckModelId, o => o.MapFrom(src => src.TruckModelDtoId));
        }
    }
}
