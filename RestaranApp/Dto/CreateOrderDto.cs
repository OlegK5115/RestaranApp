using System.ComponentModel.DataAnnotations;

namespace RestaranApp.Dto
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "missing user")]
        public string UserUuid { get; set; }

        [Required(ErrorMessage = "missing restaran")]
        public string RestaranUuid { get; set; }

        [Required(ErrorMessage = "missing start time")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "missing end time")]
        public DateTime EndTime { get; set; }
    }
}
