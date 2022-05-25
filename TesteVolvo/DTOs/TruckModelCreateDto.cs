using Flunt.Notifications;
using System.ComponentModel.DataAnnotations;

namespace TesteVolvo.DTOs
{
    public class TruckModelCreateDto : Notifiable<Notification>
    {
        [Display(Name = "Modelo base")]
        public IEnumerable<BaseTruckModelReadDto> ListBaseTruckModelReadDto { get; set; }   
        
        [Display(Name = "Modelo base")]
        public int BaseTruckModelReadDtoId { get; set; }

        [Display(Name = "Ano do Modelo")]
        public int YearOfModel { get; set; }

        public TruckModelCreateDto() { }

        public TruckModelCreateDto(IEnumerable<BaseTruckModelReadDto> listBaseTruckModelReadDto) 
        {
            ValidateListBaseTruckModel(listBaseTruckModelReadDto);

            if (this.IsValid)
            {
                ListBaseTruckModelReadDto = listBaseTruckModelReadDto;
            }
        }

        public void Validate()
        {
            if (BaseTruckModelReadDtoId == 0)
            {
                AddNotification("baseTruckModelReadDto", "Por favor, selecione um modelo base válido");
            }

            if (YearOfModel < DateTime.Now.Year)
            {
                AddNotification("yearOfModelMin", "O ano do modelo não pode ser menor que o ano atual. Por favor, informe um ano do modelo válido.");
            }
            else if (YearOfModel > DateTime.Now.AddYears(1).Year)
            {
                AddNotification("yearOfModelMax", "O ano do modelo não pode ser maior que o ano que vem. Por favor, informe um ano do modelo válido.");
            }            
        }

        private void ValidateListBaseTruckModel(IEnumerable<BaseTruckModelReadDto> listBaseTruckModelReadDto)
        {
            if (listBaseTruckModelReadDto == null || (listBaseTruckModelReadDto != null && !listBaseTruckModelReadDto.Any()))
            {
                AddNotification("listBaseTruckModelReadDto", "Não foi encontrado a lista de modelos base de caminhão");
            }
        }
    }
}
