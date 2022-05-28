using System.Collections.Generic;
using TesteVolvo.DTOs;
using TesteVolvo.Models;

namespace TesteVolvoTestProject
{
    public interface IFakeObjects
    {
        List<BaseTruckModel> GetBaseTruckModels();
        List<TruckModel> GetTruckModels();
        List<Truck> GetTrucks();
        List<BaseTruckModelDto> GetBaseTruckModelDtos();
        List<TruckModelDto> GetTruckModelDtos();
        List<TruckDto> GetTruckDtos();
    }
}