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
    public class BaseTruckModelServiceTest
    {
        private readonly IFakeObjects _fakeObjects;
        private static IMapper _mapper;       

        public BaseTruckModelServiceTest(IFakeObjects fakeObjects)
        {
            _fakeObjects = fakeObjects;
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new BaseTruckModelProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }         
        }

        [Fact(DisplayName = "Obter todos os modelos base de caminhão de um objeto fake e fazer a conversão deles via auto mapper")]
        [Trait("BaseTruckModelService", "Testes de BaseTruckModelService")]
        public void ObterTodosModelosBaseCaminhao()
        {
            // Arrange
            var baseModels = _fakeObjects.GetBaseTruckModels();

            Moq.Mock<IBaseTruckModelRepository> mockBaseTruckModelRepository = new Moq.Mock<IBaseTruckModelRepository>();            
            mockBaseTruckModelRepository.Setup(x => x.GetAllBaseTruckModels()).Returns(baseModels);

            IMapperBaseTruckModelService mapperBaseTruckModelService = new MapperBaseTruckModelService(_mapper);          

            IBaseTruckModelService baseTruckModelService = new BaseTruckModelService(mockBaseTruckModelRepository.Object, mapperBaseTruckModelService);

            // Act
            var listBaseTruckModels = baseTruckModelService.GetAllBaseTruckModels();

            // Assert
            listBaseTruckModels.Should().NotBeNull();
            listBaseTruckModels.Should().HaveCount(2);
            listBaseTruckModels.First().GetType().Should().Be(typeof(BaseTruckModelDto));
        }

        [Fact(DisplayName = "Tentar obter todos os modelos base de caminhão, mas não achar nada e o auto mapper retornar null")]
        [Trait("BaseTruckModelService", "Testes de BaseTruckModelService")]
        public void TentarObterTodosModelosBaseCaminhaoAcharNadaERetornaNull()
        {
            // Arrange            
            Moq.Mock<IBaseTruckModelRepository> mockBaseTruckModelRepository = new Moq.Mock<IBaseTruckModelRepository>();
            mockBaseTruckModelRepository.Setup(x => x.GetAllBaseTruckModels()).Returns(new List<BaseTruckModel>());

            IMapperBaseTruckModelService mapperBaseTruckModelService = new MapperBaseTruckModelService(_mapper);

            IBaseTruckModelService baseTruckModelService = new BaseTruckModelService(mockBaseTruckModelRepository.Object, mapperBaseTruckModelService);

            // Act
            var listBaseTruckModels = baseTruckModelService.GetAllBaseTruckModels();

            // Assert
            listBaseTruckModels.Should().BeNull();                        
        }

        [Fact(DisplayName = "Obter  modelos base de caminhão por id de um objeto fake e fazer a conversão deles via auto mapper")]
        [Trait("BaseTruckModelService", "Testes de BaseTruckModelService")]
        public void ObterModeloBaseCaminhaoPorId()
        {
            // Arrange
            var baseModels = _fakeObjects.GetBaseTruckModels();

            Moq.Mock<IBaseTruckModelRepository> mockBaseTruckModelRepository = new Moq.Mock<IBaseTruckModelRepository>();
            mockBaseTruckModelRepository.Setup(x => x.GetBaseTruckModelById(1)).Returns(baseModels.First());

            IMapperBaseTruckModelService mapperBaseTruckModelService = new MapperBaseTruckModelService(_mapper);

            IBaseTruckModelService baseTruckModelService = new BaseTruckModelService(mockBaseTruckModelRepository.Object, mapperBaseTruckModelService);

            // Act
            var baseTruckModels = baseTruckModelService.GetBaseTruckModelById(1);

            // Assert
            baseTruckModels.Should().NotBeNull();   
            baseTruckModels.GetType().Should().Be(typeof(BaseTruckModelDto));
        }

        [Fact(DisplayName = "Tentar obter modelo base de caminhão por id, mas não achar nada e o auto mapper retornar null")]
        [Trait("BaseTruckModelService", "Testes de BaseTruckModelService")]
        public void TentarObterTodosModeloBaseCaminhaoPorIdAcharNadaERetornaNull()
        {
            // Arrange            
            Moq.Mock<IBaseTruckModelRepository> mockBaseTruckModelRepository = new Moq.Mock<IBaseTruckModelRepository>();           

            IMapperBaseTruckModelService mapperBaseTruckModelService = new MapperBaseTruckModelService(_mapper);

            IBaseTruckModelService baseTruckModelService = new BaseTruckModelService(mockBaseTruckModelRepository.Object, mapperBaseTruckModelService);

            // Act
            var listBaseTruckModels = baseTruckModelService.GetBaseTruckModelById(11);

            // Assert
            listBaseTruckModels.Should().BeNull();
        }

    }
}
