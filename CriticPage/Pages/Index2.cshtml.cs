using CriticPage.Data;
using CriticPage.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CriticPage.Pages
{
    [BindProperties]
    [Authorize]
    public class Index2Model : PageModel
    {
        public class FileUpload
        {
            [Required]
            [Display(Name = "File")]
            public IFormFile FormFile { get; set; }
        }

        private readonly MovieDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public Movie Movie { get; set; }
        //public string Filename { get; set; }

        public Index2Model(MovieDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public FileUpload fileUpload { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            ModelState.Remove("Movie.Image");
            ModelState.Remove("Movie.Scores");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string path = _webHostEnvironment.WebRootPath + @"\UploadImages\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var formFile = fileUpload.FormFile;

            if (formFile.Length > 0)
            {
                var filePath = Path.Combine(path, formFile.FileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                   await formFile.CopyToAsync(stream);
                }

                Movie.Image = filePath;
            }

            await _context.Movie.AddAsync(Movie);
            await _context.SaveChangesAsync();
            return RedirectToPage("DisplayMovies");
        }
    }
}
