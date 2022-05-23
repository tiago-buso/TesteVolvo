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

        [Required (ErrorMessage =  "Please inform the description of the truck model")]
        [MaxLength(1000, ErrorMessage = "Please inform only 1000 chars in the description field")]
        [Column("Description")]
        public string Description { get; private set; }

        [Required(ErrorMessage = "Please inform the year of the truck model")]
        [Column("YearOfModel")]
        public int YearOfModel { get; private set; }

        public ICollection<Truck> Trucks { get; private set; }

        public TruckModel(string description, int yearOfModel)
        {            
            Description = description;
            YearOfModel = yearOfModel;
        }

        public TruckModel(int id, string description, int yearOfModel)
        {
            Id = id;
            Description = description;
            YearOfModel = yearOfModel;
        }

    }
}
