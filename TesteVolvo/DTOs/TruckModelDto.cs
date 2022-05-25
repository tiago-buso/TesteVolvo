using Flunt.Notifications;
using System.ComponentModel.DataAnnotations;

namespace TesteVolvo.DTOs
{
    public class TruckModelDto : Notifiable<Notification>
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Modelo base")]
        public IEnumerable<BaseTruckModelDto> ListBaseTruckModelDto { get; set; }   
        
        [Display(Name = "Modelo base")]
        public int BaseTruckModelDtoId { get; set; }

        [Display(Name = "Modelo base")]
        public BaseTruckModelDto BaseTruckModelDto { get; set; }

        [Display(Name = "Ano do Modelo")]
        public int YearOfModel { get; set; }

        public TruckModelDto() { }

        public void AddListBaseTruckModelDto(IEnumerable<BaseTruckModelDto> listBaseTruckModelDto) 
        {
            ValidateListBaseTruckModel(listBaseTruckModelDto);

            if (this.IsValid)
            {
                ListBaseTruckModelDto = listBaseTruckModelDto;
            }
        }

        public void Validate()
        {
            if (BaseTruckModelDtoId == 0)
            {
                AddNotification("baseTruckModelDto", "Por favor, selecione um modelo base válido");
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

        private void ValidateListBaseTruckModel(IEnumerable<BaseTruckModelDto> ListBaseTruckModelDto)
        {
            if (ListBaseTruckModelDto == null || (ListBaseTruckModelDto != null && !ListBaseTruckModelDto.Any()))
            {
                AddNotification("ListBaseTruckModelDto", "Não foi encontrado a lista de modelos base de caminhão");
            }
        }
    }
}
