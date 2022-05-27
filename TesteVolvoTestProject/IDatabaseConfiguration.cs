using System.Collections.Generic;
using TesteVolvo.Data;
using TesteVolvo.Models;

namespace TesteVolvoTestProject
{
    public interface IDatabaseConfiguration
    {
        BaseTruckModelRepository CreateBaseTruckModelRepositoryWithData();
        BaseTruckModelRepository CreateBaseTruckModelRepositoryWithoutData();
        TruckModelRepository CreateTruckModelRepositoryWithData();
        TruckModelRepository CreateTruckModelRepositoryWithoutData();
        List<BaseTruckModel> GetBaseTruckModels();
        List<TruckModel> GetTruckModels();
    }
}