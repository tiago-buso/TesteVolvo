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

        [Required]
        [MaxLength(2)]
        [Column("Description")]
        public string Description { get; set; }

        public ICollection<TruckModel> TruckModels { get; private set; }
    }
}
