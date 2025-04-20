using System.ComponentModel.DataAnnotations;

namespace RestaranApp.Dto
{
    public class CreateRestaranDto
    {
        [Required(ErrorMessage = "missing name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "missing capacity")]
        public int Capacity { get; set; }
    }
}
