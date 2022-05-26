using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TesteVolvo.Data;
using TesteVolvo.Models;
using Xunit;

namespace TesteVolvoTestProject
{
    public class BaseTruckModelRepositoryTest
    {
        private List<BaseTruckModel> GetBaseTruckModels()
        {
            List<BaseTruckModel> baseTruckModels = new List<BaseTruckModel>();

            baseTruckModels.Add(new BaseTruckModel { Id = 1, Description = "AA" });
            baseTruckModels.Add(new BaseTruckModel { Id = 2, Description = "BB" });

            return baseTruckModels;
        }

        [Fact(DisplayName = "Simular para obter todos os modelos base de caminhão")]
        [Trait("BaseTruckModelRepository", "Testes de BaseTruckModelRepository")]
        public void ObterTodosModelosBaseCaminhao()
        {
            // Arrange
            Moq.Mock<IBaseTruckModelRepository> mockBaseTruckModelRepository = new Moq.Mock<IBaseTruckModelRepository>();
            mockBaseTruckModelRepository.Setup(x => x.GetAllBaseTruckModels()).Returns(GetBaseTruckModels());    

            //Act
            var baseTruckModels = mockBaseTruckModelRepository.Object.GetAllBaseTruckModels();

            // Assert
            baseTruckModels.Should().NotBeNull();
            baseTruckModels.Should().HaveCount(2);
            mockBaseTruckModelRepository.Verify(r => r.GetAllBaseTruckModels(), Times.Once);
        }

        [Fact(DisplayName = "Simular para obter modelo base de caminhão por id")]
        [Trait("BaseTruckModelRepository", "Testes de BaseTruckModelRepository")]
        public void ObterModeloBaseCaminhaoPorId()
        {
            // Arrange
            int id = 1;
            BaseTruckModel baseTruckModel = GetBaseTruckModels().First(x => x.Id == id);

            Moq.Mock<IBaseTruckModelRepository> mockBaseTruckModelRepository = new Moq.Mock<IBaseTruckModelRepository>();
            mockBaseTruckModelRepository.Setup(x => x.GetBaseTruckModelById(id)).Returns(baseTruckModel);

            //Act
            var baseTruckModelMock = mockBaseTruckModelRepository.Object.GetBaseTruckModelById(id);

            // Assert
            baseTruckModelMock.Should().NotBeNull();
            baseTruckModelMock.Id.Should().Be(id);
            mockBaseTruckModelRepository.Verify(r => r.GetBaseTruckModelById(id), Times.Once);           
        }

        [Fact(DisplayName = "Simular para não conseguir obter modelo base de caminhão por id")]
        [Trait("BaseTruckModelRepository", "Testes de BaseTruckModelRepository")]
        public void ObterModeloBaseCaminhaoPorIdMasNaoTemElementoComEsseId()
        {
            // Arrange
            int id = 10;
            BaseTruckModel baseTruckModel = GetBaseTruckModels().FirstOrDefault(x => x.Id == id);

            Moq.Mock<IBaseTruckModelRepository> mockBaseTruckModelRepository = new Moq.Mock<IBaseTruckModelRepository>();
            mockBaseTruckModelRepository.Setup(x => x.GetBaseTruckModelById(id)).Returns(baseTruckModel);

            //Act
            var baseTruckModelMock = mockBaseTruckModelRepository.Object.GetBaseTruckModelById(id);

            // Assert
            baseTruckModelMock.Should().BeNull();            
            mockBaseTruckModelRepository.Verify(r => r.GetBaseTruckModelById(id), Times.Once);
        }
    }
}
