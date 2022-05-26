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
    public class TruckModelRepositoryTest
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

        [Fact(DisplayName = "Simular para obter todos os modelos de caminhão")]
        [Trait("TruckModelRepository", "Testes de TruckModelRepository")]
        public void ObterTodosModelosCaminhao()
        {
            // Arrange
            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();
            mockTruckModelRepository.Setup(x => x.GetAllTruckModels()).Returns(GetTruckModels());

            //Act
            var truckModels = mockTruckModelRepository.Object.GetAllTruckModels();

            // Assert
            truckModels.Should().NotBeNull();
            truckModels.Should().HaveCount(2);
            mockTruckModelRepository.Verify(r => r.GetAllTruckModels(), Times.Once);
        }

        [Fact(DisplayName = "Simular para obter modelo de caminhão por id")]
        [Trait("TruckModelRepository", "Testes de TruckModelRepository")]
        public void ObterModeloCaminhaoPorId()
        {
            // Arrange
            int id = 1;
            TruckModel truckModel = GetTruckModels().First(x => x.Id == id);

            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();
            mockTruckModelRepository.Setup(x => x.GetTruckModelById(id)).Returns(truckModel);

            //Act
            var truckModelMock = mockTruckModelRepository.Object.GetTruckModelById(id);

            // Assert
            truckModelMock.Should().NotBeNull();
            truckModelMock.Id.Should().Be(id);
            mockTruckModelRepository.Verify(r => r.GetTruckModelById(id), Times.Once);
        }

        [Fact(DisplayName = "Simular para não conseguir obter modelo de caminhão por id")]
        [Trait("TruckModelRepository", "Testes de TruckModelRepository")]
        public void ObterModeloCaminhaoPorIdMasNaoTemElementoComEsseId()
        {
            // Arrange
            int id = 10;
            TruckModel truckModel = GetTruckModels().FirstOrDefault(x => x.Id == id);

            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();
            mockTruckModelRepository.Setup(x => x.GetTruckModelById(id)).Returns(truckModel);

            //Act
            var baseTruckModelMock = mockTruckModelRepository.Object.GetTruckModelById(id);

            // Assert
            baseTruckModelMock.Should().BeNull();
            mockTruckModelRepository.Verify(r => r.GetTruckModelById(id), Times.Once);
        }
     
    }
}
