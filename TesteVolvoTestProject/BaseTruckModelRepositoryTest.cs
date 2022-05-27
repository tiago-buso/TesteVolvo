using FluentAssertions;
using Xunit;

namespace TesteVolvoTestProject
{
    public class BaseTruckModelRepositoryTest
    {

        private readonly IDatabaseConfiguration _databaseConfiguration;

        public BaseTruckModelRepositoryTest(IDatabaseConfiguration databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }       

        [Fact(DisplayName = "Obter todos os modelos base de caminhão do banco em memória")]
        [Trait("BaseTruckModelRepository", "Testes de BaseTruckModelRepository")]
        public void ObterTodosModelosBaseCaminhaoBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateBaseTruckModelRepositoryWithData();

            // Act
            var listBaseTruckModels = repository.GetAllBaseTruckModels();

            // Assert
            listBaseTruckModels.Should().NotBeNull();
            listBaseTruckModels.Should().HaveCount(2);
        }

        [Fact(DisplayName = "Tentar obter todos os modelos base de caminhão do banco em memória, mas não acha nenhum")]
        [Trait("BaseTruckModelRepository", "Testes de BaseTruckModelRepository")]
        public void AcharNenhumModeloBaseCaminhaoBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateBaseTruckModelRepositoryWithoutData();

            // Act
            var listBaseTruckModels = repository.GetAllBaseTruckModels();

            // Assert
            listBaseTruckModels.Should().BeEmpty();     
        }

        [Fact(DisplayName = "Obter modelo base de caminhão por Id do banco em memória")]
        [Trait("BaseTruckModelRepository", "Testes de BaseTruckModelRepository")]
        public void ObterModeloBaseCaminhaoPorIdBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateBaseTruckModelRepositoryWithData();

            // Act
            var baseTruckModel = repository.GetBaseTruckModelById(1);

            // Assert
            baseTruckModel.Should().NotBeNull();
            baseTruckModel.Id.Should().Be(1);
            baseTruckModel.Description.Should().Be("AA");
        }

        [Fact(DisplayName = "Tentar obter modelo base de caminhão por Id do banco em memória, mas não achar")]
        [Trait("BaseTruckModelRepository", "Testes de BaseTruckModelRepository")]
        public void AcharNenhumModeloBaseCaminhaoBancoEmMemoriaPorId()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateBaseTruckModelRepositoryWithData();

            // Act
            var baseTruckModel = repository.GetBaseTruckModelById(10);

            // Assert
            baseTruckModel.Should().BeNull();            
        }
    }
}

