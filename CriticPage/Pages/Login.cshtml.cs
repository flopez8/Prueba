using CriticPage.Data;
using CriticPage.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CriticPage.Pages
{[BindProperties]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MovieDbContext _context;
        public Admin Admin { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager, MovieDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }
        public void OnGet()
        {
            
        }

       /* public async Task<IActionResult> OnPost()
        {
            Admin admin = new Admin();
            admin = _context.Admin.Single(c => c.Name == Admin.Name);

            if (admin == null) return;

            if(admin.Name == Admin.Name && admin.Password == Admin.Password)
            {
                _signInManager.PasswordSignInAsync();
            }
        }*/
    }
}
