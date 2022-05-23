using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteVolvo.Models
{
    [Table("Truck")]
    public class Truck
    {
        [Key]        
        [Column("Id")]
        public int Id { get; private set; }

        public TruckModel TruckModel { get; private set; }

        [Required(ErrorMessage = "Please inform the model of the truck")]
        public int TruckModelId { get; private set; }

        [Required(ErrorMessage =  "Please inform the year of manufacture")]
        [Column("YearOfManufacture")]
        public int YearOfManufacture { get; private set; }

    }
}
