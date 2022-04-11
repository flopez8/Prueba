using CriticPage.Data;
using CriticPage.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CriticPage.Pages
{
    public class DisplayMoviesModel : PageModel
    {
        public class Ratings
        {
            public int id;
            public double rating;
        }
        private readonly MovieDbContext _context;
        public IEnumerable<Movie> Movies { get; set; }
        public List<Ratings> ratings { get; set; }

        public DisplayMoviesModel(MovieDbContext context)
        {
            _context = context;
        }

        public void OnGet(string search)
        {
            ratings = new List<Ratings>();
            Movies = _context.Movie;
            var movieRatings = _context.MovieScore.ToList();
            if (!String.IsNullOrEmpty(search))
            {
                Movies = Movies.Where(c => c.Title.ToLower().Contains(search.ToLower()));
            }
            foreach (var movie in Movies.ToList())
            {
                double rating = 0;
                int reviewCount = 0;
                List<MovieScore> filteredRatings = movieRatings.Where(c => c.MovieId == movie.IdMovie).ToList();
                foreach (var item in filteredRatings)
                {
                    rating += item.Score;
                }
                reviewCount = filteredRatings.Count();
                ratings.Add(new Ratings()
                {
                    id = movie.IdMovie,
                    rating = rating / reviewCount
                });
            }
        }
    }
}
