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
    public class TruckModelDtoTest
    {
        private List<BaseTruckModelDto> GetBaseTruckModelDtos()
        {
            List<BaseTruckModelDto> baseTruckModelDtos = new List<BaseTruckModelDto>();

            baseTruckModelDtos.Add(new BaseTruckModelDto { Id = 1, Description = "AA" });
            baseTruckModelDtos.Add(new BaseTruckModelDto { Id = 2, Description = "BB" });

            return baseTruckModelDtos;
        }

        [Fact(DisplayName = "Adicionar lista de modelos base de caminhão corretamente")]
        [Trait("TruckModelDTO", "Testes de criação de TruckModelDTO")]
        public void AdicionarListaModelosBaseCaminhaoCorretamente()
        {
            // Arrange
            Moq.Mock<IBaseTruckModelService> mockBaseTruckModelService = new Moq.Mock<IBaseTruckModelService>();
            mockBaseTruckModelService.Setup(x => x.GetAllBaseTruckModels()).Returns(GetBaseTruckModelDtos());
            TruckModelDto truckModelDto = new TruckModelDto();

            //Act
            truckModelDto.AddListBaseTruckModelDto(mockBaseTruckModelService.Object.GetAllBaseTruckModels());

            // Assert
            truckModelDto.IsValid.Should().BeTrue();
            truckModelDto.Notifications.Should().HaveCount(0);
            truckModelDto.ListBaseTruckModelDto.Count().Should().Be(2);
        }

        [Fact(DisplayName = "Adicionar lista de modelos base de caminhão incorretamente - lista vazia")]
        [Trait("TruckModelDTO", "Testes de criação de TruckModelDTO")]
        public void AdicionarListaModelosBaseCaminhaoIncorretamenteListaVazia()
        {
            // Arrange
            Moq.Mock<IBaseTruckModelService> mockBaseTruckModelService = new Moq.Mock<IBaseTruckModelService>();
            mockBaseTruckModelService.Setup(x => x.GetAllBaseTruckModels()).Returns(new List<BaseTruckModelDto>());
            TruckModelDto truckModelDto = new TruckModelDto();

            //Act
            truckModelDto.AddListBaseTruckModelDto(mockBaseTruckModelService.Object.GetAllBaseTruckModels());

            // Assert
            truckModelDto.Notifications.First(x => x.Key == "ListBaseTruckModelDto").Message.Should().Be("Não foi encontrado a lista de modelos base de caminhão");
            truckModelDto.IsValid.Should().BeFalse();
            truckModelDto.Notifications.Should().HaveCount(1);
            truckModelDto.ListBaseTruckModelDto.Should().BeNull();
        }

        [Fact(DisplayName = "Adicionar lista de modelos base de caminhão incorretamente - lista nula")]
        [Trait("TruckModelDTO", "Testes de criação de TruckModelDTO")]
        public void AdicionarListaModelosBaseCaminhaoIncorretamenteNula()
        {
            // Arrange     
            TruckModelDto truckModelDto = new TruckModelDto();

            //Act
            truckModelDto.AddListBaseTruckModelDto(null);

            // Assert
            truckModelDto.Notifications.First(x => x.Key == "ListBaseTruckModelDto").Message.Should().Be("Não foi encontrado a lista de modelos base de caminhão");
            truckModelDto.IsValid.Should().BeFalse();
            truckModelDto.Notifications.Should().HaveCount(1);
            truckModelDto.ListBaseTruckModelDto.Should().BeNull();
        }

        [Fact(DisplayName = "Instanciar entidade de modelos de caminhão corretamente - Validação OK")]
        [Trait("TruckModelDTO", "Testes de criação de TruckModelDTO")]
        public void InstanciarTruckModelDtoCorretamenteValidateOK()
        {
            // Arrange     
            BaseTruckModelDto baseTruckModelDto = GetBaseTruckModelDtos().First();

            TruckModelDto truckModelDto = new TruckModelDto();
            truckModelDto.Id = 1;
            truckModelDto.BaseTruckModelDto = baseTruckModelDto;
            truckModelDto.BaseTruckModelDtoId = baseTruckModelDto.Id;
            truckModelDto.YearOfModel = DateTime.Now.Year;

            //Act
            truckModelDto.Validate();

            // Assert
            truckModelDto.IsValid.Should().BeTrue();
            truckModelDto.Notifications.Should().HaveCount(0);
            truckModelDto.Id.Should().Be(1);
            truckModelDto.BaseTruckModelDto.Should().NotBeNull();
            truckModelDto.BaseTruckModelDtoId.Should().Be(baseTruckModelDto.Id);
            truckModelDto.YearOfModel.Should().Be(DateTime.Now.Year);
            truckModelDto.TruckModelFormatted.Should().Be($"{DateTime.Now.Year}/{baseTruckModelDto.Description}");
        }

        [Fact(DisplayName = "Instanciar entidade de modelos de caminhão incorretamente - Validação Base Modelo")]
        [Trait("TruckModelDTO", "Testes de criação de TruckModelDTO")]
        public void InstanciarTruckModelDTOIncorretamenteValidateNOKBaseModeloIdZero()
        {

            // Arrange                 
            TruckModelDto truckModelDto = new TruckModelDto();
            truckModelDto.Id = 1;            
            truckModelDto.YearOfModel = DateTime.Now.Year;

            //Act
            truckModelDto.Validate();

            // Assert
            truckModelDto.IsValid.Should().BeFalse();
            truckModelDto.Notifications.Should().HaveCount(1);
            truckModelDto.Notifications.First(x => x.Key == "baseTruckModelDto").Message.Should().Be("Por favor, selecione um modelo base válido");
            truckModelDto.BaseTruckModelDto.Should().BeNull();
            truckModelDto.BaseTruckModelDtoId.Should().Be(0);
            truckModelDto.TruckModelFormatted.Should().Be($"{DateTime.Now.Year}/");
        }

        [Fact(DisplayName = "Instanciar entidade de modelos de caminhão incorretamente - Validação Ano Modelo menor que o ano atual")]
        [Trait("TruckModelDTO", "Testes de criação de TruckModelDTO")]
        public void InstanciarTruckModelDTOIncorretamenteValidateNOKAnoModeloMenorQueOAtual()
        {
            // Arrange     
            BaseTruckModelDto baseTruckModelDto = GetBaseTruckModelDtos().First();
            TruckModelDto truckModelDto = new TruckModelDto();
            truckModelDto.Id = 1;
            truckModelDto.BaseTruckModelDto = baseTruckModelDto;
            truckModelDto.BaseTruckModelDtoId = baseTruckModelDto.Id;
            truckModelDto.YearOfModel = DateTime.Now.AddYears(-2).Year;

            //Act
            truckModelDto.Validate();

            // Assert
            truckModelDto.IsValid.Should().BeFalse();
            truckModelDto.Notifications.Should().HaveCount(1);
            truckModelDto.Notifications.First(x => x.Key == "yearOfModelMin").Message.Should().Be("O ano do modelo não pode ser menor que o ano atual. Por favor, informe um ano do modelo válido.");
        }

        [Fact(DisplayName = "Instanciar entidade de modelos de caminhão incorretamente - Validação Ano Modelo maior que o ano que vem")]
        [Trait("TruckModelDTO", "Testes de criação de TruckModelDTO")]
        public void InstanciarTruckModelDTOIncorretamenteValidateNOKAnoModeloMaiorQueAnoQueVem()
        {
            // Arrange     
            BaseTruckModelDto baseTruckModelDto = GetBaseTruckModelDtos().First();
            TruckModelDto truckModelDto = new TruckModelDto();
            truckModelDto.Id = 1;
            truckModelDto.BaseTruckModelDto = baseTruckModelDto;
            truckModelDto.BaseTruckModelDtoId = baseTruckModelDto.Id;
            truckModelDto.YearOfModel = DateTime.Now.AddYears(3).Year;

            //Act
            truckModelDto.Validate();

            // Assert
            truckModelDto.IsValid.Should().BeFalse();
            truckModelDto.Notifications.Should().HaveCount(1);
            truckModelDto.Notifications.First(x => x.Key == "yearOfModelMax").Message.Should().Be("O ano do modelo não pode ser maior que o ano que vem. Por favor, informe um ano do modelo válido.");
        }

        [Fact(DisplayName = "Instanciar entidade de modelos de caminhão incorretamente - Ambas validações com problema")]
        [Trait("TruckModelDTO", "Testes de criação de TruckModelDTO")]
        public void InstanciarTruckModelDTOIncorretamenteValidateNOKTotalErrado()
        {
            // Arrange              
            TruckModelDto truckModelDto = new TruckModelDto();
            truckModelDto.Id = 1;

            //Act
            truckModelDto.Validate();

            // Assert
            truckModelDto.IsValid.Should().BeFalse();
            truckModelDto.Notifications.Should().HaveCount(2);
            truckModelDto.Notifications.First(x => x.Key == "yearOfModelMin").Message.Should().Be("O ano do modelo não pode ser menor que o ano atual. Por favor, informe um ano do modelo válido.");
            truckModelDto.Notifications.First(x => x.Key == "baseTruckModelDto").Message.Should().Be("Por favor, selecione um modelo base válido");
        }
    }
}
