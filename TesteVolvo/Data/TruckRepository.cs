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

    }
}
