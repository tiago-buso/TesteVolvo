using Flunt.Notifications;
using System.ComponentModel.DataAnnotations;

namespace TesteVolvo.DTOs
{
    public class TruckModelCreateDto : Notifiable<Notification>
    {              

        [Display(Name = "Modelo base")]
        public BaseTruckModelReadDto BaseTruckModelReadDto { get; private set; }

        [Display(Name = "Ano do Modelo")]
        public int YearOfModel { get; private set; }

        public TruckModelCreateDto(BaseTruckModelReadDto baseTruckModelReadDto, int yearOfModel)
        {
            Validate(baseTruckModelReadDto, yearOfModel);

            if (this.IsValid)
            {
                BaseTruckModelReadDto = baseTruckModelReadDto;
                YearOfModel = yearOfModel;
            }
        }

        private void Validate(BaseTruckModelReadDto baseTruckModelReadDto, int yearOfModel)
        {
            if (baseTruckModelReadDto == null || (baseTruckModelReadDto != null && string.IsNullOrEmpty(baseTruckModelReadDto.Description)))
            {
                AddNotification("baseTruckModelReadDto", "Por favor, selecione um modelo base válido");
            }

            if (yearOfModel < DateTime.Now.Year)
            {
                AddNotification("yearOfModel", "O ano do modelo não pode ser menor que o ano atual. Por favor, informe um ano do modelo válido.");
            }
        }
    }
}
