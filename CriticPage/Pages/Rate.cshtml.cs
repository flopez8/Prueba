using CriticPage.Data;
using CriticPage.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CriticPage.Pages
{
    [BindProperties]
    public class RateModel : PageModel
    {
        private readonly MovieDbContext _context;
        public Movie Movie { get; set; }
        public MovieScore MovieScore { get; set; }
        public List<MovieScore> MovieScores { get; set; }
        public Statistics StatisticValues { get; set; }
        public RateModel(MovieDbContext context)
        {
            _context = context;
        }
        public void OnGet(int id)
        {
            MovieScores = _context.MovieScore.Where(c => c.MovieId == id).ToList();
            Movie movie = _context.Movie.Single(c => c.IdMovie == id);

            if (movie != null)
            {
                Movie = movie;
            }

            GetStatistics();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            MovieScore.MovieId = id;
            await _context.MovieScore.AddAsync(MovieScore);
            await _context.SaveChangesAsync();
            return RedirectToPage("DisplayMovies");
        }

        public void GetStatistics()
        {
            StatisticValues = new Statistics();

            if (MovieScores.Count == 0)
            {
                //obtener minimo
                StatisticValues.Min = 0;
                //obtener maximo
                StatisticValues.Max = 0;
                //obtener promedio
                StatisticValues.Avg = 0;

                StatisticValues.Median = 0;

                StatisticValues.Mode = 0;

                StatisticValues.StandardDeviation = 0;
            }
            else
            {
                //obtener minimo
                StatisticValues.Min = MovieScores.Min(c => c.Score);
                //obtener maximo
                StatisticValues.Max = MovieScores.Max(c => c.Score);
                //obtener promedio
                StatisticValues.Avg = MovieScores.Average(c => c.Score);
                //obtener mediana
                if (MovieScores.Count % 2 == 0)
                {
                    int halfSet = MovieScores.Count / 2;
                    var split = MovieScores.OrderBy(c => c.Score).Chunk(halfSet);
                    StatisticValues.Median = (split.ElementAt(0).Last().Score + split.ElementAt(1).First().Score) / 2;
                }
                else
                {
                    int midValue = MovieScores.Count / 2;
                    StatisticValues.Median = MovieScores.ElementAt(midValue).Score;
                }
                //obtener moda
                StatisticValues.Mode = _context.MovieScore.FromSqlRaw("Select top 1 with ties MAX(IdMovieScore) IdMovieScore, MAX(MovieId) MovieId, Score, MAX(Comment) Comment from MovieScore where(MovieScore.MovieId = {0}) group by MovieScore.Score order by COUNT(*) desc", Movie.IdMovie).Single().Score;
                //obtener desviación estándar
                StatisticValues.StandardDeviation = _context.MovieScore.FromSqlRaw("Select MAX(IdMovieScore) IdMovieScore, MAX(MovieId) MovieId, MAX(Comment) Comment, STDEV(Score) as Score from MovieScore where(MovieScore.MovieId = {0})", Movie.IdMovie).Single().Score;
            }



        }

        public class Statistics
        {
            public int Min { get; set; }
            public int Max { get; set; }
            public double Avg { get; set; }
            public double Median { get; set; }
            public int Mode { get; set; }
            public double StandardDeviation { get; set; }
        }
    }
}
