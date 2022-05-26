using Microsoft.EntityFrameworkCore;
using TesteVolvo.Models;

namespace TesteVolvo.Data
{
    public class TruckRepository : ITruckRepository
    {
        private readonly AppDbContext _context;

        public TruckRepository(AppDbContext context)
        {
            _context = context;
        }

        public int GetCountOfTrucksWithSpecificTruckModel(int truckModelId)
        {
            return _context.Trucks.Count(x => x.TruckModelId == truckModelId);
        }

        public IEnumerable<Truck> GetAllTrucks()
        {
            return _context.Trucks.AsNoTracking().Include("TruckModel.BaseTruckModel").ToList();
        }

        public Truck GetTruckById(int id)
        {
            var truck = _context.Trucks.AsNoTracking().Include("TruckModel.BaseTruckModel").FirstOrDefault(x => x.Id == id);
            if (truck != null)
            {
                _context.Entry(truck).State = EntityState.Detached;
            }
            return truck;
        }

        public void CreateTruck(Truck truck)
        {
            if (truck == null)
            {
                throw new ArgumentNullException(nameof(truck));
            }

            _context.Trucks.Add(truck);
        }

        public void UpdateTruck(Truck truck)
        {
            if (truck == null)
            {
                throw new ArgumentNullException(nameof(truck));
            }

            _context.Trucks.Update(truck);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteTruck(Truck truck)
        {
            _context.Trucks.Remove(truck);
        }

    }
}
