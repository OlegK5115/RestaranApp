using System.ComponentModel.DataAnnotations;

namespace RestaranApp.Dto
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "missing name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "missing email")]
        public string Email { get; set; }
    }
}
