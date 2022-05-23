using System.ComponentModel.DataAnnotations;

namespace TesteVolvo.DTOs
{
    public class TruckModelReadDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; private set; }

        [Display(Name = "Ano do Modelo")]
        public int YearOfModel { get; private set; }
    }
}
