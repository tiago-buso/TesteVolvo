using Microsoft.EntityFrameworkCore;
using TesteVolvo.Models;

namespace TesteVolvo.Data
{
    public class BaseTruckModelRepository : IBaseTruckModelRepository
    {
        private readonly AppDbContext _context;

        public BaseTruckModelRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BaseTruckModel> GetAllBaseTruckModels()
        {
            return _context.BaseTruckModels.AsNoTracking().ToList();
        }

        public BaseTruckModel GetBaseTruckModelById(int id)
        {
            var baseTruckModel =  _context.BaseTruckModels.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (baseTruckModel != null)
            {
                _context.Entry(baseTruckModel).State = EntityState.Detached;
            }

            return baseTruckModel;

        }
    }
}
