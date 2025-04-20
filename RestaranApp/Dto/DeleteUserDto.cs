using System.ComponentModel.DataAnnotations;

namespace RestaranApp.Dto
{
    public class DeleteUserDto
    {
        [Required(ErrorMessage = "missing uuid")]
        [StringLength(26)]
        public string Uuid { get; set; }
    }
}
