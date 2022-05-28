using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteVolvo.Data;
using TesteVolvo.DTOs;
using TesteVolvo.Models;
using TesteVolvo.Profiles;
using TesteVolvo.Services;
using Xunit;

namespace TesteVolvoTestProject
{
    public class TruckServiceTest
    {
        private readonly IFakeObjects _fakeObjects;
        private static IMapper _mapper;

        public TruckServiceTest(IFakeObjects fakeObjects)
        {
            _fakeObjects = fakeObjects;
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new TruckModelProfile());
                    mc.AddProfile(new BaseTruckModelProfile());
                    mc.AddProfile(new TruckProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact(DisplayName = "Obter todos os registros de caminhão de um objeto fake e fazer a conversão deles via auto mapper")]
        [Trait("TruckService", "Testes de TruckService")]
        public void ObterTodosCaminhoes()
        {
            // Arrange
            var trucks = _fakeObjects.GetTrucks();

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();
            mockTruckRepository.Setup(x => x.GetAllTrucks()).Returns(trucks);            

            IMapperTruckService mapperTruckService = new MapperTruckService(_mapper);

            ITruckService truckService = new TruckService(mockTruckRepository.Object, mapperTruckService);

            // Act
            var listTrucks = truckService.GetAllTrucks();

            // Assert
            listTrucks.Should().NotBeNull();
            listTrucks.Should().HaveCount(2);
            listTrucks.First().GetType().Should().Be(typeof(TruckDto));
        }

        [Fact(DisplayName = "Tentar obter todos os registros de caminhão, mas não achar nada e o auto mapper retornar null")]
        [Trait("TruckService", "Testes de TruckService")]
        public void TentarObterTodosCaminhoesAcharNadaERetornaNull()
        {
            // Arrange            
            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();
            mockTruckRepository.Setup(x => x.GetAllTrucks()).Returns(new List<Truck>());            

            IMapperTruckService mapperTruckService = new MapperTruckService(_mapper);

            ITruckService truckService = new TruckService(mockTruckRepository.Object, mapperTruckService);

            // Act
            var listTrucks = truckService.GetAllTrucks();

            // Assert
            listTrucks.Should().BeNull();
        }

        [Fact(DisplayName = "Obter o caminhão por id de um objeto fake e fazer a conversão dele via auto mapper")]
        [Trait("TruckService", "Testes de TruckService")]
        public void ObterCaminhaoPorId()
        {
            // Arrange
            var trucks = _fakeObjects.GetTrucks();

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();
            mockTruckRepository.Setup(x => x.GetTruckById(1)).Returns(trucks.First());            

            IMapperTruckService mapperTruckService = new MapperTruckService(_mapper);

            ITruckService truckService = new TruckService(mockTruckRepository.Object, mapperTruckService);

            // Act
            var truck = truckService.GetTruckById(1);

            // Assert
            truck.Should().NotBeNull();
            truck.GetType().Should().Be(typeof(TruckDto));
        }

        [Fact(DisplayName = "Tentar obter o caminhão por id, mas não achar nada e o auto mapper retornar null")]
        [Trait("TruckService", "Testes de TruckService")]
        public void TentarObterCaminhaoPorIdAcharNadaERetornaNull()
        {
            // Arrange            
            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();            

            IMapperTruckService mapperTruckService = new MapperTruckService(_mapper);

            ITruckService truckService = new TruckService(mockTruckRepository.Object, mapperTruckService);

            // Act
            var truck = truckService.GetTruckById(11);

            // Assert
            truck.Should().BeNull();
        }


        [Fact(DisplayName = "Deletar o caminhão de um objeto fake e fazer a conversão deles via auto mapper")]
        [Trait("TruckService", "Testes de TruckService")]
        public void DeletaroCaminhao()
        {
            // Arrange
            var trucks = _fakeObjects.GetTruckDtos();
            var truck = trucks.First();

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();
            mockTruckRepository.Setup(x => x.SaveChanges()).Returns(true);            

            IMapperTruckService mapperTruckService = new MapperTruckService(_mapper);

            ITruckService truckService = new TruckService(mockTruckRepository.Object, mapperTruckService);

            // Act
            var deleted = truckService.DeleteTruck(truck);

            // Assert
            deleted.Should().BeTrue();
        }


        [Fact(DisplayName = "Tentar deletar o caminhão de um objeto fake que não existe e o auto mapper retorna null")]
        [Trait("TruckService", "Testes de TruckService")]
        public void TentarDeletarCaminhaoObjetoNull()
        {
            // Arrange         
            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();           

            IMapperTruckService mapperTruckService = new MapperTruckService(_mapper);

            ITruckService truckService = new TruckService(mockTruckRepository.Object, mapperTruckService);

            // Act
            var deleted = truckService.DeleteTruck(null);

            // Assert
            deleted.Should().BeFalse();
        }      

        [Fact(DisplayName = "Criar caminhão de um objeto fake e fazer a conversão deles via auto mapper")]
        [Trait("TruckService", "Testes de TruckService")]
        public void CriarCaminhao()
        {
            // Arrange
            var trucks = _fakeObjects.GetTruckDtos();
            var truck = trucks.First();

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();
            mockTruckRepository.Setup(x => x.SaveChanges()).Returns(true);            

            IMapperTruckService mapperTruckService = new MapperTruckService(_mapper);

            ITruckService truckService = new TruckService(mockTruckRepository.Object, mapperTruckService);

            // Act
            var created = truckService.CreateTruck(truck);

            // Assert
            created.Should().BeTrue();
        }


        [Fact(DisplayName = "Tentar criar caminhão de um objeto fake que não existe e o auto mapper retorna null")]
        [Trait("TruckService", "Testes de TruckService")]
        public void TentarCriarCaminhaoObjetoNull()
        {
            // Arrange         
            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();          

            IMapperTruckService mapperTruckService = new MapperTruckService(_mapper);

            ITruckService truckService = new TruckService(mockTruckRepository.Object, mapperTruckService);

            // Act
            var created = truckService.CreateTruck(null);

            // Assert
            created.Should().BeFalse();
        }

        [Fact(DisplayName = "Atualizar caminhão de um objeto fake e fazer a conversão deles via auto mapper")]
        [Trait("TruckService", "Testes de TruckService")]
        public void AtualizarCaminhao()
        {
            // Arrange
            var trucks = _fakeObjects.GetTruckDtos();
            var truck = trucks.First();

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();
            mockTruckRepository.Setup(x => x.SaveChanges()).Returns(true);            

            IMapperTruckService mapperTruckService = new MapperTruckService(_mapper);

            ITruckService truckService = new TruckService(mockTruckRepository.Object, mapperTruckService);

            // Act
            var updated = truckService.UpdateTruck(truck);

            // Assert
            updated.Should().BeTrue();
        }


        [Fact(DisplayName = "Tentar atualizar caminhão de um objeto fake que não existe e o auto mapper retorna null")]
        [Trait("TruckService", "Testes de TruckService")]
        public void TentarAtualizarCaminhaoObjetoNull()
        {
            // Arrange         
            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();         

            IMapperTruckService mapperTruckService = new MapperTruckService(_mapper);

            ITruckService truckService = new TruckService(mockTruckRepository.Object, mapperTruckService);

            // Act
            var updated = truckService.UpdateTruck(null);

            // Assert
            updated.Should().BeFalse();
        }
    }
}
