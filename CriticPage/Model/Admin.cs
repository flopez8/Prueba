using System.ComponentModel.DataAnnotations;

namespace CriticPage.Model
{
    public class Admin
    {
        [Key]
        public int IdAdmin { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
