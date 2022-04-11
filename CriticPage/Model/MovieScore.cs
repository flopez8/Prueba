using System.ComponentModel.DataAnnotations;

namespace CriticPage.Model

{
    public class MovieScore
    {
        [Key]
        public int IdMovieScore { get; set; }
        [Required]
        public int Score { get; set; }
        public string Comment { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
