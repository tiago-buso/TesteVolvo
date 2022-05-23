using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteVolvo.Models
{
    [Table("BaseTruckModel")]
    public class BaseTruckModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please inform the description of the truck model")]
        [MaxLength(2, ErrorMessage = "Please inform only 2 chars in the description field")]
        [Column("Description")]
        public string Description { get; set; }

        public ICollection<TruckModel> TruckModels { get; private set; }
    }
}
