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
            return _context.TruckModels.AsNoTracking().Include("BaseTruckModel").ToList();
        }

        public TruckModel GetTruckModelById(int id)
        {
            var truckModel = _context.TruckModels.AsNoTracking().Include("BaseTruckModel").FirstOrDefault(x => x.Id == id);
            if (truckModel != null)
            {
                _context.Entry(truckModel).State = EntityState.Detached;
            }
            return truckModel;
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
