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

        [Required]
        public int BaseTruckModelId { get; private set; }

        [Required]
        [Column("YearOfModel")]
        public int YearOfModel { get; private set; }

        public ICollection<Truck> Trucks { get; private set; }

        public TruckModel(int id, BaseTruckModel baseTruckModel, int yearOfModel)
        {
            Id = id;
            BaseTruckModel = baseTruckModel;
            YearOfModel = yearOfModel;
        }
    }
}
