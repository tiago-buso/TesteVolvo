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
    public class TruckModelServiceTest
    {
        private readonly IFakeObjects _fakeObjects;
        private static IMapper _mapper;

        public TruckModelServiceTest(IFakeObjects fakeObjects)
        {
            _fakeObjects = fakeObjects;
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new TruckModelProfile());
                    mc.AddProfile(new BaseTruckModelProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact(DisplayName = "Obter todos os modelos de caminhão de um objeto fake e fazer a conversão deles via auto mapper")]
        [Trait("TruckModelService", "Testes de TruckModelService")]
        public void ObterTodosModelosCaminhao()
        {
            // Arrange
            var models = _fakeObjects.GetTruckModels();

            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();
            mockTruckModelRepository.Setup(x => x.GetAllTruckModels()).Returns(models);

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();

            IMapperTruckModelService mapperTruckModelService = new MapperTruckModelService(_mapper);

            ITruckModelService truckModelService = new TruckModelService(mockTruckModelRepository.Object, mockTruckRepository.Object, mapperTruckModelService);

            // Act
            var listTruckModels = truckModelService.GetAllTruckModels();

            // Assert
            listTruckModels.Should().NotBeNull();
            listTruckModels.Should().HaveCount(2);
            listTruckModels.First().GetType().Should().Be(typeof(TruckModelDto));
        }

        [Fact(DisplayName = "Tentar obter todos os modelos de caminhão, mas não achar nada e o auto mapper retornar null")]
        [Trait("TruckModelService", "Testes de TruckModelService")]
        public void TentarObterTodosModelosCaminhaoAcharNadaERetornaNull()
        {
            // Arrange            
            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();
            mockTruckModelRepository.Setup(x => x.GetAllTruckModels()).Returns(new List<TruckModel>());

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();

            IMapperTruckModelService mapperTruckModelService = new MapperTruckModelService(_mapper);

            ITruckModelService truckModelService = new TruckModelService(mockTruckModelRepository.Object, mockTruckRepository.Object, mapperTruckModelService);

            // Act
            var listTruckModels = truckModelService.GetAllTruckModels();

            // Assert
            listTruckModels.Should().BeNull();
        }

        [Fact(DisplayName = "Obter modelos de caminhão por id de um objeto fake e fazer a conversão deles via auto mapper")]
        [Trait("TruckModelService", "Testes de TruckModelService")]
        public void ObterModeloCaminhaoPorId()
        {
            // Arrange
            var models = _fakeObjects.GetTruckModels();

            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();
            mockTruckModelRepository.Setup(x => x.GetTruckModelById(1)).Returns(models.First());

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();

            IMapperTruckModelService mapperTruckModelService = new MapperTruckModelService(_mapper);

            ITruckModelService truckModelService = new TruckModelService(mockTruckModelRepository.Object, mockTruckRepository.Object, mapperTruckModelService);

            // Act
            var truckModels = truckModelService.GetTruckModelById(1);

            // Assert
            truckModels.Should().NotBeNull();
            truckModels.GetType().Should().Be(typeof(TruckModelDto));
        }

        [Fact(DisplayName = "Tentar obter modelo de caminhão por id, mas não achar nada e o auto mapper retornar null")]
        [Trait("TruckModelService", "Testes de TruckModelService")]
        public void TentarObterModeloCaminhaoPorIdAcharNadaERetornaNull()
        {
            // Arrange            
            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();

            IMapperTruckModelService mapperTruckModelService = new MapperTruckModelService(_mapper);

            ITruckModelService truckModelService = new TruckModelService(mockTruckModelRepository.Object, mockTruckRepository.Object, mapperTruckModelService);

            // Act
            var truckModel = truckModelService.GetTruckModelById(11);

            // Assert
            truckModel.Should().BeNull();
        }


        [Fact(DisplayName = "Deletar modelo de caminhão de um objeto fake e fazer a conversão deles via auto mapper")]
        [Trait("TruckModelService", "Testes de TruckModelService")]
        public void DeletarModeloCaminhao()
        {
            // Arrange
            var models = _fakeObjects.GetTruckModelDtos();
            var model = models.First();

            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();
            mockTruckModelRepository.Setup(x => x.SaveChanges()).Returns(true);

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();

            IMapperTruckModelService mapperTruckModelService = new MapperTruckModelService(_mapper);

            ITruckModelService truckModelService = new TruckModelService(mockTruckModelRepository.Object, mockTruckRepository.Object, mapperTruckModelService);

            // Act
            var deleted = truckModelService.DeleteTruckModel(model);

            // Assert
            deleted.Should().BeTrue();           
        }


        [Fact(DisplayName = "Tentar deletar modelo de caminhão de um objeto fake que não existe e o auto mapper retorna null")]
        [Trait("TruckModelService", "Testes de TruckModelService")]
        public void TentarDeletarModeloCaminhaoObjetoNull()
        {
            // Arrange         
            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();            

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();

            IMapperTruckModelService mapperTruckModelService = new MapperTruckModelService(_mapper);

            ITruckModelService truckModelService = new TruckModelService(mockTruckModelRepository.Object, mockTruckRepository.Object, mapperTruckModelService);

            // Act
            var deleted = truckModelService.DeleteTruckModel(null);

            // Assert
            deleted.Should().BeFalse();
        }

        [Fact(DisplayName = "Verificar se pode deletar um modelo de caminhão")]
        [Trait("TruckModelService", "Testes de TruckModelService")]
        public void VerificarSePodeDeletarModeloCaminhao()
        {
            // Arrange           
            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();            

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();
            mockTruckRepository.Setup(x => x.GetCountOfTrucksWithSpecificTruckModel(150)).Returns(0);

            IMapperTruckModelService mapperTruckModelService = new MapperTruckModelService(_mapper);

            ITruckModelService truckModelService = new TruckModelService(mockTruckModelRepository.Object, mockTruckRepository.Object, mapperTruckModelService);

            // Act
            var canBeDeleted = truckModelService.CheckIfCanDeleteTruckModel(150);

            // Assert
            canBeDeleted.Should().BeTrue();
        }

        [Fact(DisplayName = "Verificar se pode deletar um modelo de caminhão e nesse caso não pode porque já existe caminhão com esse modelo")]
        [Trait("TruckModelService", "Testes de TruckModelService")]
        public void VerificarSePodeDeletarModeloCaminhaoMasNaoPode()
        {
            // Arrange           
            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();
            mockTruckRepository.Setup(x => x.GetCountOfTrucksWithSpecificTruckModel(1)).Returns(1);

            IMapperTruckModelService mapperTruckModelService = new MapperTruckModelService(_mapper);

            ITruckModelService truckModelService = new TruckModelService(mockTruckModelRepository.Object, mockTruckRepository.Object, mapperTruckModelService);

            // Act
            var canBeDeleted = truckModelService.CheckIfCanDeleteTruckModel(1);

            // Assert
            canBeDeleted.Should().BeFalse();
        }

        [Fact(DisplayName = "Criar modelo de caminhão de um objeto fake e fazer a conversão deles via auto mapper")]
        [Trait("TruckModelService", "Testes de TruckModelService")]
        public void CriarModeloCaminhao()
        {
            // Arrange
            var models = _fakeObjects.GetTruckModelDtos();
            var model = models.First();

            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();
            mockTruckModelRepository.Setup(x => x.SaveChanges()).Returns(true);

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();

            IMapperTruckModelService mapperTruckModelService = new MapperTruckModelService(_mapper);

            ITruckModelService truckModelService = new TruckModelService(mockTruckModelRepository.Object, mockTruckRepository.Object, mapperTruckModelService);

            // Act
            var created = truckModelService.CreateTruckModel(model);

            // Assert
            created.Should().BeTrue();
        }


        [Fact(DisplayName = "Tentar criar modelo de caminhão de um objeto fake que não existe e o auto mapper retorna null")]
        [Trait("TruckModelService", "Testes de TruckModelService")]
        public void TentarCriarModeloCaminhaoObjetoNull()
        {
            // Arrange         
            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();

            IMapperTruckModelService mapperTruckModelService = new MapperTruckModelService(_mapper);

            ITruckModelService truckModelService = new TruckModelService(mockTruckModelRepository.Object, mockTruckRepository.Object, mapperTruckModelService);

            // Act
            var created = truckModelService.CreateTruckModel(null);

            // Assert
            created.Should().BeFalse();
        }

        [Fact(DisplayName = "Atualizar modelo de caminhão de um objeto fake e fazer a conversão deles via auto mapper")]
        [Trait("TruckModelService", "Testes de TruckModelService")]
        public void AtualizarModeloCaminhao()
        {
            // Arrange
            var models = _fakeObjects.GetTruckModelDtos();
            var model = models.First();

            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();
            mockTruckModelRepository.Setup(x => x.SaveChanges()).Returns(true);

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();

            IMapperTruckModelService mapperTruckModelService = new MapperTruckModelService(_mapper);

            ITruckModelService truckModelService = new TruckModelService(mockTruckModelRepository.Object, mockTruckRepository.Object, mapperTruckModelService);

            // Act
            var updated = truckModelService.UpdateTruckModel(model);

            // Assert
            updated.Should().BeTrue();
        }


        [Fact(DisplayName = "Tentar atualizar modelo de caminhão de um objeto fake que não existe e o auto mapper retorna null")]
        [Trait("TruckModelService", "Testes de TruckModelService")]
        public void TentarAtualizarModeloCaminhaoObjetoNull()
        {
            // Arrange         
            Moq.Mock<ITruckModelRepository> mockTruckModelRepository = new Moq.Mock<ITruckModelRepository>();

            Moq.Mock<ITruckRepository> mockTruckRepository = new Moq.Mock<ITruckRepository>();

            IMapperTruckModelService mapperTruckModelService = new MapperTruckModelService(_mapper);

            ITruckModelService truckModelService = new TruckModelService(mockTruckModelRepository.Object, mockTruckRepository.Object, mapperTruckModelService);

            // Act
            var updated = truckModelService.UpdateTruckModel(null);

            // Assert
            updated.Should().BeFalse();
        }

    }
}
