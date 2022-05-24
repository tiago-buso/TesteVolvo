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
            return _context.BaseTruckModels.ToList();
        }

        public BaseTruckModel GetBaseTruckModelById(int id)
        {
            return _context.BaseTruckModels.FirstOrDefault(x => x.Id == id);
        }
    }
}
