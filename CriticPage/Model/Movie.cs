using System.ComponentModel.DataAnnotations;

namespace CriticPage.Model
{
    public class Movie
    {
        [Key]
        public int IdMovie { get; set; }
        [Required(ErrorMessage ="El título de la película es requerido")]
        public string Title { get; set; }
        [Required(ErrorMessage ="La sinopsis de la película es requerida")]
        public string Synopsis { get; set; }
        public string Image { get; set; }

        public List<MovieScore> Scores { get; set; }
    }
}
