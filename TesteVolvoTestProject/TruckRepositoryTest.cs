using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteVolvo.Data;
using TesteVolvo.Models;
using Xunit;

namespace TesteVolvoTestProject
{
    public class TruckRepositoryTest
    {
        private List<BaseTruckModel> GetBaseTruckModels()
        {
            List<BaseTruckModel> baseTruckModels = new List<BaseTruckModel>();

            baseTruckModels.Add(new BaseTruckModel { Id = 1, Description = "AA" });
            baseTruckModels.Add(new BaseTruckModel { Id = 2, Description = "BB" });

            return baseTruckModels;
        }

        private List<TruckModel> GetTruckModels()
        {
            List<BaseTruckModel> baseTruckModels = GetBaseTruckModels();

            List<TruckModel> truckModels = new List<TruckModel>();

            TruckModel truckModel = new TruckModel(1, baseTruckModels.First(), DateTime.Now.Year);

            TruckModel truckModel2 = new TruckModel(2, baseTruckModels.Last(), DateTime.Now.Year);

            truckModels.Add(truckModel);
            truckModels.Add(truckModel2);

            return truckModels;
        }

        private List<Truck> GetTrucks()
        {            
            List<TruckModel> truckModels = GetTruckModels();
            List<Truck> trucks = new List<Truck>();

            Truck truck = new Truck(1, truckModels.First(), DateTime.Now.Year);
            Truck truck2 = new Truck(2, truckModels.Last(), DateTime.Now.Year);

            trucks.Add(truck);
            trucks.Add(truck2);

            return trucks;
        }

        [Fact(DisplayName = "Simular para obter todos os caminhões")]
        [Trait("TruckRepository", "Testes de TruckRepository")]
        public void ObterTodosCaminhoes()
        {
            // Arrange
            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();
            mockTruckRepository.Setup(x => x.GetAllTrucks()).Returns(GetTrucks());

            //Act
            var trucks = mockTruckRepository.Object.GetAllTrucks();

            // Assert
            trucks.Should().NotBeNull();
            trucks.Should().HaveCount(2);
            mockTruckRepository.Verify(r => r.GetAllTrucks(), Times.Once);
        }

        [Fact(DisplayName = "Simular para obter caminhão por id")]
        [Trait("TruckRepository", "Testes de TruckRepository")]
        public void ObterCaminhaoPorId()
        {
            // Arrange
            int id = 1;
            Truck truck = GetTrucks().First(x => x.Id == id);

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();
            mockTruckRepository.Setup(x => x.GetTruckById(id)).Returns(truck);

            //Act
            var truckMock = mockTruckRepository.Object.GetTruckById(id);

            // Assert
            truckMock.Should().NotBeNull();
            truckMock.Id.Should().Be(id);
            mockTruckRepository.Verify(r => r.GetTruckById(id), Times.Once);
        }

        [Fact(DisplayName = "Simular para não conseguir obter caminhão por id")]
        [Trait("TruckRepository", "Testes de TruckRepository")]
        public void ObterCaminhaoPorIdMasNaoTemElementoComEsseId()
        {
            // Arrange
            int id = 10;
            Truck truck = GetTrucks().FirstOrDefault(x => x.Id == id);

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();
            mockTruckRepository.Setup(x => x.GetTruckById(id)).Returns(truck);

            //Act
            var truckMock = mockTruckRepository.Object.GetTruckById(id);

            // Assert
            truckMock.Should().BeNull();
            mockTruckRepository.Verify(r => r.GetTruckById(id), Times.Once);
        }
    }
}
