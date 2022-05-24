namespace TesteVolvo.Data
{
    public interface ITruckRepository
    {
        int GetCountOfTrucksWithSpecificTruckModel(int truckModelId);
    }
}