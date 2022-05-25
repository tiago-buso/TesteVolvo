using AutoMapper;
using TesteVolvo.DTOs;
using TesteVolvo.Models;

namespace TesteVolvo.Profiles
{
    public class BaseTruckModelProfile : Profile
    {
        public BaseTruckModelProfile()
        {
            // Source - Target

            CreateMap<BaseTruckModel, BaseTruckModelDto>();
            CreateMap<BaseTruckModelDto, BaseTruckModel>();
        }
    }
}
