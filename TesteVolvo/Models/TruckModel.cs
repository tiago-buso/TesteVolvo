using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteVolvo.Models
{
    [Table("TruckModel")]
    public class TruckModel
    {       
        [Key]        
        [Column("Id")]
        public int Id { get; private set; }
       
        public BaseTruckModel BaseTruckModel { get; private set; }

        [Required(ErrorMessage = "Please inform the base truck model of the truck")]
        public int BaseTruckModelId { get; private set; }

        [Required(ErrorMessage = "Please inform the year of the truck model")]
        [Column("YearOfModel")]
        public int YearOfModel { get; private set; }

        public ICollection<Truck> Trucks { get; private set; }

        public TruckModel(int baseTruckModelId, int yearOfModel)
        {
            BaseTruckModelId = baseTruckModelId;
            YearOfModel = yearOfModel;
        }

        public TruckModel(int id, int baseTruckModelId, int yearOfModel)
        {
            Id = id;
            BaseTruckModelId = baseTruckModelId;
            YearOfModel = yearOfModel;
        }

    }
}
