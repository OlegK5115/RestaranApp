using System.ComponentModel.DataAnnotations;

namespace RestaranApp.Entities
{
    public class Restaran
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "missing uuid")]
        [StringLength(36)]
        public string Uuid { get; set; }
        
        [Required(ErrorMessage = "missing name")]
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "missing capacity")]
        public int Capacity { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
