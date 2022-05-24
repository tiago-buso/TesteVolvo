using System.ComponentModel.DataAnnotations;

namespace TesteVolvo.DTOs
{
    public class TruckModelReadDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Modelo base")]
        public BaseTruckModelReadDto BaseTruckModelReadDto { get; private set; }

        [Display(Name = "Ano do Modelo")]
        public int YearOfModel { get; private set; }
    }
}
