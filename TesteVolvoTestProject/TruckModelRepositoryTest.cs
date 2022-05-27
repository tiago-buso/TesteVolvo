using FluentAssertions;
using System;
using System.Linq;
using TesteVolvo.Models;
using Xunit;

namespace TesteVolvoTestProject
{
    public class TruckModelRepositoryTest
    {
        private readonly IDatabaseConfiguration _databaseConfiguration;

        public TruckModelRepositoryTest(IDatabaseConfiguration databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }            

        [Fact(DisplayName = "Obter todos os modelos de caminhão do banco em memória")]
        [Trait("TruckModelRepository", "Testes de TruckModelRepository")]
        public void ObterTodosModelosCaminhaoBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckModelRepositoryWithData();

            // Act
            var listTruckModels = repository.GetAllTruckModels();

            // Assert
            listTruckModels.Should().NotBeNull();
            listTruckModels.Should().HaveCount(2);
            listTruckModels.First().BaseTruckModel.Should().NotBeNull();
        }

        [Fact(DisplayName = "Tentar obter todos os modelos de caminhão do banco em memória, mas não acha nenhum")]
        [Trait("TruckModelRepository", "Testes de TruckModelRepository")]
        public void AcharNenhumModeloCaminhaoBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckModelRepositoryWithoutData();

            // Act
            var listTruckModels = repository.GetAllTruckModels();

            // Assert
            listTruckModels.Should().BeEmpty();
        }

        [Fact(DisplayName = "Obter modelo de caminhão por Id do banco em memória")]
        [Trait("TruckModelRepository", "Testes de TruckModelRepository")]
        public void ObterModeloCaminhaoPorIdBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckModelRepositoryWithData();

            // Act
            var truckModel = repository.GetTruckModelById(1);

            // Assert
            truckModel.Should().NotBeNull();
            truckModel.Id.Should().Be(1);
            truckModel.BaseTruckModel.Should().NotBeNull();
            truckModel.BaseTruckModel.Description.Should().Be("AA");
        }

        [Fact(DisplayName = "Tentar obter modelo de caminhão por Id do banco em memória, mas não achar")]
        [Trait("TruckModelRepository", "Testes de TruckModelRepository")]
        public void AcharNenhumModeloBaseCaminhaoBancoEmMemoriaPorId()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckModelRepositoryWithData();

            // Act
            var truckModel = repository.GetTruckModelById(10);

            // Assert
            truckModel.Should().BeNull();
        }

        [Fact(DisplayName = "Criar modelo de caminhão no banco em memória")]
        [Trait("TruckModelRepository", "Testes de TruckModelRepository")]
        public void CriarModeloBaseCaminhaoBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckModelRepositoryWithData();
            var baseModelrepository = _databaseConfiguration.CreateBaseTruckModelRepositoryWithData();

            var baseTruckModelId = baseModelrepository.GetAllBaseTruckModels().First().Id;
            var newTruckModel = new TruckModel(baseTruckModelId, DateTime.Now.Year);

            int oldCount = repository.GetAllTruckModels().Count();

            // Act
            repository.CreateTruckModel(newTruckModel);
            bool created = repository.SaveChanges();

            // Assert
            var listTruckModel = repository.GetAllTruckModels();
            listTruckModel.Should().HaveCount(oldCount + 1);
            created.Should().BeTrue();
        }

        [Fact(DisplayName = "Atualizar modelo de caminhão no banco em memória")]
        [Trait("TruckModelRepository", "Testes de TruckModelRepository")]
        public void AtualizarModeloBaseCaminhaoBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckModelRepositoryWithData();
            var truckModel = repository.GetTruckModelById(1);

            // Act
            truckModel.UpdateYearOfModel(DateTime.Now.AddYears(30).Year);
            repository.UpdateTruckModel(truckModel);
            var updated = repository.SaveChanges();

            // Assert
            var truckModelUpdated = repository.GetTruckModelById(1);
            truckModelUpdated.YearOfModel.Should().Be(DateTime.Now.AddYears(30).Year);
            updated.Should().BeTrue();
        }


        [Fact(DisplayName = "Deletar modelo de caminhão no banco em memória")]
        [Trait("TruckModelRepository", "Testes de TruckModelRepository")]
        public void DeletarModeloBaseCaminhaoBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckModelRepositoryWithData();
            var truckModel = repository.GetTruckModelById(1);           

            // Act            
            repository.DeleteTruckModel(truckModel);
            var deleted = repository.SaveChanges();

            // Assert                     
            deleted.Should().BeTrue();         
        }

    }
}
