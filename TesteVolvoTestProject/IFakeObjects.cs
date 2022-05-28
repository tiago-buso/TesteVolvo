using System.Collections.Generic;
using TesteVolvo.Models;

namespace TesteVolvoTestProject
{
    public interface IFakeObjects
    {
        List<BaseTruckModel> GetBaseTruckModels();
        List<TruckModel> GetTruckModels();
        List<Truck> GetTrucks();
    }
}