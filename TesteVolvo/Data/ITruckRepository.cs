using TesteVolvo.Models;

namespace TesteVolvo.Data
{
    public interface ITruckRepository
    {
        int GetCountOfTrucksWithSpecificTruckModel(int truckModelId);
        IEnumerable<Truck> GetAllTrucks();
        Truck GetTruckById(int id);
        void CreateTruck(Truck truck);
        void UpdateTruck(Truck truck);
        bool SaveChanges();
        void DeleteTruck(Truck truck);
    }
}