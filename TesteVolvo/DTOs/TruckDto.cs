using Flunt.Notifications;
using System.ComponentModel.DataAnnotations;

namespace TesteVolvo.DTOs
{
    public class TruckDto : Notifiable<Notification>
    {
        [Display(Name = "Id")]
        public int Id { get; set; }        

        [Display(Name = "Modelo do caminhão")]
        public TruckModelDto TruckModelDto { get; set; }

        [Display(Name = "Modelo do caminhão")]
        public int TruckModelDtoId { get; set; }       

        [Display(Name = "Ano de fabricação")]
        public int YearOfManufacture { get; set; }

        [Display(Name = "Modelo do caminhão")]
        public IEnumerable<TruckModelDto> ListTruckModelDto { get; set; }

        public TruckDto()
        {
        }

        public void AddListTruckModelDto(IEnumerable<TruckModelDto> listTruckModelDto)
        {
            ValidateListTruckModel(listTruckModelDto);

            if (this.IsValid)
            {
                ListTruckModelDto = listTruckModelDto;
            }
        }

        public void Validate()
        {
            if (TruckModelDtoId == 0)
            {
                AddNotification("truckModelDto", "Por favor, selecione um modelo válido.");
            }

            if (YearOfManufacture != DateTime.Now.Year)
            {
                AddNotification("yearOfManufacture", "O ano de fabricação tem que ser o ano atual.");
            }          
        }

        private void ValidateListTruckModel(IEnumerable<TruckModelDto> listTruckModelDto)
        {
            if (listTruckModelDto == null || (listTruckModelDto != null && !listTruckModelDto.Any()))
            {
                AddNotification("ListTruckModelDto", "Não foi encontrado a lista de modelos de caminhão");
            }
        }

    }
}
