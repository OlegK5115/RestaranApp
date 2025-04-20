using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaranApp.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "missing uuid")]
        [StringLength(36)]
        public string Uuid { get; set; }

        [Required(ErrorMessage = "missing start time")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "missing end time")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "missing userid")]
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required(ErrorMessage = "missing restaranid")]
        public int RestaranId { get; set; }
        
        [ForeignKey("RestaranId")]
        public Restaran Restaran { get; set; }
    }
}
