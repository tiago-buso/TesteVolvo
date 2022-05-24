using Microsoft.EntityFrameworkCore;
using TesteVolvo.Models;

namespace TesteVolvo.Data
{
    public class TruckModelRepository : ITruckModelRepository
    {
        private readonly AppDbContext _context;

        public TruckModelRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TruckModel> GetAllTruckModels()
        {
            return _context.TruckModels.Include("BaseTruckModel").ToList();
        }

        public TruckModel GetTruckModelById(int id)
        {
            return _context.TruckModels.FirstOrDefault(x => x.Id == id);
        }

        public void CreateTruckModel(TruckModel truckModel)
        {
            if (truckModel == null)
            {
                throw new ArgumentNullException(nameof(truckModel));
            }

            _context.TruckModels.Add(truckModel);
        }

        public void UpdateTruckModel(TruckModel truckModel)
        {
            if (truckModel == null)
            {
                throw new ArgumentNullException(nameof(truckModel));
            }

            _context.TruckModels.Update(truckModel);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteTruckModel(TruckModel truckModel)
        {
            _context.TruckModels.Remove(truckModel);
        }
    }
}
