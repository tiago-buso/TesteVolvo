using TesteVolvo.Models;

namespace TesteVolvo.Data
{
    public interface ITruckModelRepository
    {
        void CreateTruckModel(TruckModel truckModel);
        void DeleteTruckModel(TruckModel truckModel);
        IEnumerable<TruckModel> GetAllTruckModels();
        TruckModel GetTruckModelById(int id);
        bool SaveChanges();
        void UpdateTruckModel(TruckModel truckModel);
    }
}