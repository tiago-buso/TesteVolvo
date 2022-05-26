using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteVolvo.DTOs;
using TesteVolvo.Services;
using Xunit;

namespace TesteVolvoTestProject
{
    public class TruckDtoTest
    {
        private List<BaseTruckModelDto> GetBaseTruckModelDtos()
        {
            List<BaseTruckModelDto> baseTruckModelDtos = new List<BaseTruckModelDto>();

            baseTruckModelDtos.Add(new BaseTruckModelDto { Id = 1, Description = "AA" });
            baseTruckModelDtos.Add(new BaseTruckModelDto { Id = 2, Description = "BB" });

            return baseTruckModelDtos;
        }

        private List<TruckModelDto> GetTruckModelDtos()
        {
            List<BaseTruckModelDto> baseTruckModelDtos = GetBaseTruckModelDtos();

            List<TruckModelDto> truckModelDtos = new List<TruckModelDto>();            

            TruckModelDto truckModelDto = new TruckModelDto();
            truckModelDto.Id = 1;
            truckModelDto.YearOfModel = DateTime.Now.Year;
            truckModelDto.BaseTruckModelDto = baseTruckModelDtos.First();

            TruckModelDto truckModelDto2 = new TruckModelDto();
            truckModelDto2.Id = 2;
            truckModelDto2.YearOfModel = DateTime.Now.Year;
            truckModelDto2.BaseTruckModelDto = baseTruckModelDtos.Last();

            truckModelDtos.Add(truckModelDto);
            truckModelDtos.Add(truckModelDto2);

            return truckModelDtos;
        }      


        [Fact(DisplayName = "Adicionar lista de modelos de caminhão corretamente")]
        [Trait("TruckDTO", "Testes de criação de TruckDTO")]
        public void AdicionarListaModelosCaminhaoCorretamente()
        {
            // Arrange
            Moq.Mock<ITruckModelService> mockTruckModelService = new Moq.Mock<ITruckModelService>();
            mockTruckModelService.Setup(x => x.GetAllTruckModels()).Returns(GetTruckModelDtos());
            TruckDto truckDto = new TruckDto();

            //Act
            truckDto.AddListTruckModelDto(mockTruckModelService.Object.GetAllTruckModels());

            // Assert
            truckDto.IsValid.Should().BeTrue();            
            truckDto.Notifications.Should().HaveCount(0);
            truckDto.ListTruckModelDto.Count().Should().Be(2);
        }


        [Fact(DisplayName = "Adicionar lista de modelos de caminhão incorretamente - lista vazia")]
        [Trait("TruckDTO", "Testes de criação de TruckDTO")]
        public void AdicionarListaModelosCaminhaoIncorretamenteListaVazia()
        {
            // Arrange
            Moq.Mock<ITruckModelService> mockTruckModelService = new Moq.Mock<ITruckModelService>();
            mockTruckModelService.Setup(x => x.GetAllTruckModels()).Returns(new List<TruckModelDto>());
            TruckDto truckDto = new TruckDto();

            //Act
            truckDto.AddListTruckModelDto(mockTruckModelService.Object.GetAllTruckModels());

            // Assert
            truckDto.Notifications.First(x => x.Key == "ListTruckModelDto").Message.Should().Be("Não foi encontrado a lista de modelos de caminhão");
            truckDto.IsValid.Should().BeFalse();
            truckDto.Notifications.Should().HaveCount(1);
            truckDto.ListTruckModelDto.Should().BeNull();
        }

        [Fact(DisplayName = "Adicionar lista de modelos de caminhão incorretamente - lista nula")]
        [Trait("TruckDTO", "Testes de criação de TruckDTO")]
        public void AdicionarListaModelosCaminhaoIncorretamenteNula()
        {
            // Arrange     
            TruckDto truckDto = new TruckDto();

            //Act
            truckDto.AddListTruckModelDto(null);

            // Assert
            truckDto.Notifications.First(x => x.Key == "ListTruckModelDto").Message.Should().Be("Não foi encontrado a lista de modelos de caminhão");
            truckDto.IsValid.Should().BeFalse();
            truckDto.Notifications.Should().HaveCount(1);
            truckDto.ListTruckModelDto.Should().BeNull();
        }

        [Fact(DisplayName = "Instanciar entidade de modelos de caminhão corretamente - Validação OK")]
        [Trait("TruckDTO", "Testes de criação de TruckDTO")]
        public void InstanciarTruckDtoCorretamenteValidateOK()
        {
            // Arrange     
            TruckModelDto truckModelDto = GetTruckModelDtos().First();
            TruckDto truckDto = new TruckDto();
            truckDto.Id = 1;
            truckDto.TruckModelDto = truckModelDto;
            truckDto.TruckModelDtoId = truckModelDto.Id;
            truckDto.YearOfManufacture = DateTime.Now.Year;

            //Act
            truckDto.Validate();

            // Assert
            truckDto.IsValid.Should().BeTrue();
            truckDto.Notifications.Should().HaveCount(0);
            truckDto.Id.Should().Be(1);
            truckDto.TruckModelDto.Should().NotBeNull();
            truckDto.TruckModelDtoId.Should().Be(truckModelDto.Id);
            truckDto.YearOfManufacture.Should().Be(DateTime.Now.Year);
        }

        [Fact(DisplayName = "Instanciar entidade de modelos de caminhão incorretamente - Validação Modelo")]
        [Trait("TruckDTO", "Testes de criação de TruckDTO")]
        public void InstanciarTruckDtoIncorretamenteValidateNOKModeloIdZero()
        {
            // Arrange     

            TruckDto truckDto = new TruckDto();
            truckDto.Id = 1;            
            truckDto.YearOfManufacture = DateTime.Now.Year;

            //Act
            truckDto.Validate();

            // Assert
            truckDto.IsValid.Should().BeFalse();
            truckDto.Notifications.Should().HaveCount(1);
            truckDto.Notifications.First(x => x.Key == "truckModelDto").Message.Should().Be("Por favor, selecione um modelo válido.");            
            truckDto.TruckModelDto.Should().BeNull();
            truckDto.TruckModelDtoId.Should().Be(0);
            
        }

        [Fact(DisplayName = "Instanciar entidade de modelos de caminhão incorretamente - Validação Ano Fabricação")]
        [Trait("TruckDTO", "Testes de criação de TruckDTO")]
        public void InstanciarTruckDtoIncorretamenteValidateNOKAnoFabricacaoErrado()
        {
            // Arrange     
            TruckModelDto truckModelDto = GetTruckModelDtos().First();
            TruckDto truckDto = new TruckDto();
            truckDto.Id = 1;
            truckDto.TruckModelDto = truckModelDto;
            truckDto.TruckModelDtoId = truckModelDto.Id;
            truckDto.YearOfManufacture = DateTime.Now.AddYears(3).Year;

            //Act
            truckDto.Validate();

            // Assert
            truckDto.IsValid.Should().BeFalse();
            truckDto.Notifications.Should().HaveCount(1);
            truckDto.Notifications.First(x => x.Key == "yearOfManufacture").Message.Should().Be("O ano de fabricação tem que ser o ano atual.");                        
        }

        [Fact(DisplayName = "Instanciar entidade de modelos de caminhão incorretamente - Ambas validações com problema")]
        [Trait("TruckDTO", "Testes de criação de TruckDTO")]
        public void InstanciarTruckDtoIncorretamenteValidateNOKTotalErrado()
        {
            // Arrange              
            TruckDto truckDto = new TruckDto();
            truckDto.Id = 1;            

            //Act
            truckDto.Validate();

            // Assert
            truckDto.IsValid.Should().BeFalse();
            truckDto.Notifications.Should().HaveCount(2);
            truckDto.Notifications.First(x => x.Key == "yearOfManufacture").Message.Should().Be("O ano de fabricação tem que ser o ano atual.");
            truckDto.Notifications.First(x => x.Key == "truckModelDto").Message.Should().Be("Por favor, selecione um modelo válido.");
        }
    }
}
