using System.ComponentModel.DataAnnotations;

namespace RestaranApp.Dto
{
    public class DeleteUserDto
    {
        [Required(ErrorMessage = "missing uuid")]
        [StringLength(36)]
        public string Uuid { get; set; }
    }
}
