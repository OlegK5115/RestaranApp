using System.ComponentModel.DataAnnotations;

namespace RestaranApp.Dto
{
    public class DeleteRestaranDto
    {
        [Required(ErrorMessage = "missing uuid")]
        [StringLength(26)]
        public string Uuid { get; set; }
    }
}
