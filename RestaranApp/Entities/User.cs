using Microsoft.EntityFrameworkCore;
using RestaranApp.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaranApp.Entities
{
    public class User
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("uuid")]
        [Required(ErrorMessage = "missing uuid")]
        [StringLength(36)]
        public string Uuid { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "missing name")]
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "missing email")]
        [MinLength(2)]
        [MaxLength(30)]
        public string Email { get; set; }

        [Column("status")]
        [Required(ErrorMessage = "missing status")]
        [MinLength(2)]
        [MaxLength(20)]
        public string Status { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}

// в nuget скачать Microsoft.EntityFrameworkCore