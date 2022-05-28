using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteVolvo.DTOs;
using TesteVolvo.Models;

namespace TesteVolvoTestProject
{
    public class FakeObjects : IFakeObjects
    {
        public List<BaseTruckModel> GetBaseTruckModels()
        {
            List<BaseTruckModel> baseTruckModels = new List<BaseTruckModel>();

            baseTruckModels.Add(new BaseTruckModel { Id = 1, Description = "AA" });
            baseTruckModels.Add(new BaseTruckModel { Id = 2, Description = "BB" });

            return baseTruckModels;
        }

        public List<TruckModel> GetTruckModels()
        {
            List<BaseTruckModel> baseTruckModels = GetBaseTruckModels();

            List<TruckModel> truckModels = new List<TruckModel>();

            TruckModel truckModel = new TruckModel(1, baseTruckModels.First(), DateTime.Now.Year);
            TruckModel truckModel2 = new TruckModel(2, baseTruckModels.Last(), DateTime.Now.Year);

            truckModels.Add(truckModel);
            truckModels.Add(truckModel2);

            return truckModels;
        }

        public List<Truck> GetTrucks()
        {
            List<TruckModel> truckModels = GetTruckModels();
            List<Truck> trucks = new List<Truck>();

            Truck truck = new Truck(1, truckModels.First(), DateTime.Now.Year);
            Truck truck2 = new Truck(2, truckModels.Last(), DateTime.Now.Year);

            trucks.Add(truck);
            trucks.Add(truck2);

            return trucks;
        }

        public List<BaseTruckModelDto> GetBaseTruckModelDtos()
        {
            List<BaseTruckModelDto> baseTruckModels = new List<BaseTruckModelDto>();

            baseTruckModels.Add(new BaseTruckModelDto { Id = 1, Description = "AA" });
            baseTruckModels.Add(new BaseTruckModelDto { Id = 2, Description = "BB" });

            return baseTruckModels;
        }

        public List<TruckModelDto> GetTruckModelDtos()
        {
            List<BaseTruckModelDto> baseTruckModels = GetBaseTruckModelDtos();

            List<TruckModelDto> truckModels = new List<TruckModelDto>();

            TruckModelDto truckModel = new TruckModelDto();
            truckModel.Id = 1;
            truckModel.BaseTruckModelDto = baseTruckModels.First();
            truckModel.YearOfModel = DateTime.Now.Year;

            TruckModelDto truckModel2 = new TruckModelDto();
            truckModel2.Id = 2;
            truckModel2.BaseTruckModelDto = baseTruckModels.Last();
            truckModel2.YearOfModel = DateTime.Now.Year;

            truckModels.Add(truckModel);
            truckModels.Add(truckModel2);

            return truckModels;
        }

        public List<TruckDto> GetTruckDtos()
        {
            List<TruckModelDto> truckModels = GetTruckModelDtos();
            List<TruckDto> trucks = new List<TruckDto>();

            TruckDto truck = new TruckDto();
            truck.Id = 1;
            truck.TruckModelDto = truckModels.First();
            truck.YearOfManufacture = DateTime.Now.Year;

            TruckDto truck2 = new TruckDto();
            truck2.Id = 2;
            truck2.TruckModelDto = truckModels.Last();
            truck2.YearOfManufacture = DateTime.Now.Year;

            trucks.Add(truck);
            trucks.Add(truck2);

            return trucks;
        }
    }
}
