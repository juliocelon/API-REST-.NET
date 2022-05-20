using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [Required]
        public string Login { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

        [StringLength(10)]
        public string Role { get; set; }

        [StringLength(10)]
        public float USD_Balance { get; set; }
    }
}
