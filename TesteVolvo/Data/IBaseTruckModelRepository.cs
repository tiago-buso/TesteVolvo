using TesteVolvo.Models;

namespace TesteVolvo.Data
{
    public interface IBaseTruckModelRepository
    {
        IEnumerable<BaseTruckModel> GetAllBaseTruckModels();
        BaseTruckModel GetBaseTruckModelById(int id);
    }
}