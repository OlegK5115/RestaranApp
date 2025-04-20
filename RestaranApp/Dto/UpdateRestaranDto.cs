using System.ComponentModel.DataAnnotations;

namespace RestaranApp.Dto
{
    public class UpdateRestaranDto
    {
        [Required(ErrorMessage = "missing uuid")]
        [StringLength(26)]
        public string Uuid { get; set; }

        [MinLength(2)]
        [MaxLength(30)]
        public string NewName { get; set; }

        public int NewCapacity { get; set; }
    }
}
