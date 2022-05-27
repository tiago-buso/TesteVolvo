using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteVolvo.Data;
using TesteVolvo.Models;

namespace TesteVolvoTestProject
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        private DbContextOptions<AppDbContext> dbContextOptions;
        public DatabaseConfiguration()
        {
            var dbName = $"TesteVolvoDb{DateTime.Now.ToFileTimeUtc()}";

            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        public BaseTruckModelRepository CreateBaseTruckModelRepositoryWithData()
        {
            AppDbContext context = new AppDbContext(dbContextOptions);
            ClearAllData(context);
            PopulateDataBaseTruckModel(context);
            return new BaseTruckModelRepository(context);
        }

        public BaseTruckModelRepository CreateBaseTruckModelRepositoryWithoutData()
        {
            AppDbContext context = new AppDbContext(dbContextOptions);
            ClearAllData(context);
            return new BaseTruckModelRepository(context);
        }


        public TruckModelRepository CreateTruckModelRepositoryWithData()
        {
            AppDbContext context = new AppDbContext(dbContextOptions);
            ClearAllData(context);           
            PopulateDataTruckModel(context);
            return new TruckModelRepository(context);
        }

        public TruckModelRepository CreateTruckModelRepositoryWithoutData()
        {
            AppDbContext context = new AppDbContext(dbContextOptions);
            ClearAllData(context);
            return new TruckModelRepository(context);
        }

        public TruckRepository CreateTruckRepositoryWithData()
        {
            AppDbContext context = new AppDbContext(dbContextOptions);
            ClearAllData(context);
            PopulateDataTruck(context);             
            return new TruckRepository(context);
        }

        public TruckRepository CreateTruckRepositoryWithoutData()
        {
            AppDbContext context = new AppDbContext(dbContextOptions);
            ClearAllData(context);
            return new TruckRepository(context);
        }

        private void ClearAllData(AppDbContext context)
        {
            context.Trucks.RemoveRange(context.Trucks);
            context.TruckModels.RemoveRange(context.TruckModels);
            context.BaseTruckModels.RemoveRange(context.BaseTruckModels);
        }

        private void PopulateDataBaseTruckModel(AppDbContext context)
        {
            var baseTruckModels = GetBaseTruckModels();

            foreach (var baseTruckModel in baseTruckModels)
            {
                context.BaseTruckModels.Add(baseTruckModel);
            }

            context.SaveChanges();
        }

        private void PopulateDataTruckModel(AppDbContext context)
        {
            var truckModels = GetTruckModels();

            foreach (var truckModel in truckModels)
            {
                context.TruckModels.Add(truckModel);
            }

            context.SaveChanges();
        }

        private void PopulateDataTruck(AppDbContext context)
        {
            var trucks = GetTrucks();

            foreach (var truck in trucks)
            {
                context.Trucks.Add(truck);
            }

            context.SaveChanges();
        }

        private List<BaseTruckModel> GetBaseTruckModels()
        {
            List<BaseTruckModel> baseTruckModels = new List<BaseTruckModel>();

            baseTruckModels.Add(new BaseTruckModel { Description = "AA" });
            baseTruckModels.Add(new BaseTruckModel { Description = "BB" });

            return baseTruckModels;
        }

        private List<TruckModel> GetTruckModels()
        {
            List<BaseTruckModel> baseTruckModels = GetBaseTruckModels();

            List<TruckModel> truckModels = new List<TruckModel>();           

            TruckModel truckModel = new TruckModel(baseTruckModels.First(), DateTime.Now.Year);
            TruckModel truckModel2 = new TruckModel(baseTruckModels.Last(), DateTime.Now.Year);

            truckModels.Add(truckModel);
            truckModels.Add(truckModel2);

            return truckModels;
        }

        private List<Truck> GetTrucks()
        {
            List<TruckModel> truckModels = GetTruckModels();
            List<Truck> trucks = new List<Truck>();

            Truck truck = new Truck(truckModels.First(), DateTime.Now.Year);
            Truck truck2 = new Truck(truckModels.Last(), DateTime.Now.Year);

            trucks.Add(truck);
            trucks.Add(truck2);

            return trucks;
        }

    }
}
