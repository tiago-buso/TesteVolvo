using AutoMapper;
using TesteVolvo.Models;
using TesteVolvo.DTOs;

namespace TesteVolvo.Profiles
{
    public class TruckModelProfile : Profile
    {
        public TruckModelProfile()
        {
            // Source - Targert

            CreateMap<TruckModel, TruckModelReadDto>();
            CreateMap<TruckModelCreateDto, TruckModel>();
        }
    }
}
