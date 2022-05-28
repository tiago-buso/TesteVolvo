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
        private readonly IDatabaseConfiguration _databaseConfiguration;

        public TruckRepositoryTest(IDatabaseConfiguration databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }

        [Fact(DisplayName = "Obter todos os registros de caminhão do banco em memória")]
        [Trait("TruckRepository", "Testes de TruckRepository")]
        public void ObterTodosRegistrosCaminhaoBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckRepositoryWithData();

            // Act
            var listTrucks = repository.GetAllTrucks();

            // Assert
            listTrucks.Should().NotBeNull();
            listTrucks.Should().HaveCount(2);
            listTrucks.First().TruckModel.Should().NotBeNull();
        }

        [Fact(DisplayName = "Tentar obter todos os registros de caminhão do banco em memória, mas não acha nenhum")]
        [Trait("TruckRepository", "Testes de TruckRepository")]
        public void AcharNenhumCaminhaoBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckRepositoryWithoutData();

            // Act
            var listTrucks = repository.GetAllTrucks();

            // Assert
            listTrucks.Should().BeEmpty();
        }

        [Fact(DisplayName = "Obter registro de caminhão por Id do banco em memória")]
        [Trait("TruckRepository", "Testes de TruckRepository")]
        public void ObterCaminhaoPorIdBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckRepositoryWithData();

            // Act
            var truck = repository.GetTruckById(1);

            // Assert
            truck.Should().NotBeNull();
            truck.Id.Should().Be(1);
            truck.TruckModel.Should().NotBeNull();
            truck.TruckModel.BaseTruckModel.Should().NotBeNull();
            truck.TruckModel.BaseTruckModel.Description.Should().Be("AA");
        }

        [Fact(DisplayName = "Tentar obter registro de caminhão por Id do banco em memória, mas não achar")]
        [Trait("TruckRepository", "Testes de TruckRepository")]
        public void AcharNenhumCaminhaoBancoEmMemoriaPorId()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckRepositoryWithData();

            // Act
            var truck = repository.GetTruckById(10);

            // Assert
            truck.Should().BeNull();
        }

        [Fact(DisplayName = "Criar registro de caminhão no banco em memória")]
        [Trait("TruckRepository", "Testes de TruckRepository")]
        public void CriarCaminhaoBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckRepositoryWithData();
            var modelRepository = _databaseConfiguration.CreateTruckModelRepositoryWithData();

            var modelTruckId = modelRepository.GetAllTruckModels().First().Id;
            var newTruck = new Truck(modelTruckId, DateTime.Now.Year);

            int oldCount = repository.GetAllTrucks().Count();

            // Act
            repository.CreateTruck(newTruck);
            bool created = repository.SaveChanges();

            // Assert
            var listTruck = repository.GetAllTrucks();
            listTruck.Should().HaveCount(oldCount + 1);
            created.Should().BeTrue();
        }

        [Fact(DisplayName = "Atualizar registro de caminhão no banco em memória")]
        [Trait("TruckRepository", "Testes de TruckRepository")]
        public void AtualizarCaminhaoBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckRepositoryWithData();
            var truck = repository.GetTruckById(1);

            // Act
            truck.UpdateYearOfManufacture(DateTime.Now.AddYears(30).Year);
            repository.UpdateTruck(truck);
            var updated = repository.SaveChanges();

            // Assert
            var truckUpdated = repository.GetTruckById(1);
            truckUpdated.YearOfManufacture.Should().Be(DateTime.Now.AddYears(30).Year);
            updated.Should().BeTrue();
        }


        [Fact(DisplayName = "Deletar registro de caminhão no banco em memória")]
        [Trait("TruckRepository", "Testes de TruckRepository")]
        public void DeletarCaminhaoBancoEmMemoria()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckRepositoryWithData();
            var truck = repository.GetTruckById(1);

            // Act            
            repository.DeleteTruck(truck);
            var deleted = repository.SaveChanges();

            // Assert                     
            deleted.Should().BeTrue();
        }


        [Fact(DisplayName = "Obter quantidade de camimnhões que tenha um determinado Id")]
        [Trait("TruckRepository", "Testes de TruckRepository")]
        public void ObterQuantidadeCaminhoesPorTruckModelId()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckRepositoryWithData();           

            // Act            
            var qtde = repository.GetCountOfTrucksWithSpecificTruckModel(1);

            // Assert            
            qtde.Should().BeGreaterThanOrEqualTo(1);      
        }


        [Fact(DisplayName = "Obter quantidade de camimnhões que tenha um determinado Id que não exista")]
        [Trait("TruckRepository", "Testes de TruckRepository")]
        public void ObterQuantidadeCaminhoesPorTruckModelIdMasNaoExisteNenhum()
        {
            // Arrange
            var repository = _databaseConfiguration.CreateTruckRepositoryWithData();

            // Act            
            var qtde = repository.GetCountOfTrucksWithSpecificTruckModel(100);

            // Assert            
            qtde.Should().Be(0);
        }
    }
}
