using Microsoft.AspNetCore.Identity;
using SalahBlog.Data;
using SalahBlog.Models;

namespace SalahBlog.Utilites
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(RoleManager<IdentityRole> roleManager,
                             ApplicationDbContext context,
                             UserManager<ApplicationUser> userManager)
        {

            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public void Initialize()
        {
            if(!_roleManager.RoleExistsAsync(WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAdmin)).GetAwaiter().GetResult();  
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAuthor)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName= "admin@gmail.com",
                    Email="admin@gmail.com",
                    FirstName="super",
                    LastName="Admin"
                },"Admin@1020").Wait();

                var appUser=_context.ApplicationUsers.FirstOrDefault(x=>x.Email=="admin@gmail.com");
                
                if(appUser!=null) { 
                    _userManager.AddToRoleAsync(appUser,WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult();
                
                }

                //var page = new Page(){Title = "About Us",Slug = "About-us"};
                //var contactPage = new Page(){ Title = "Contact", Slug = "Contact" };
                //var PrivacyPloicyPage = new Page() { Title = "Privacy Policy", Slug = "privacy" };
                

                //_context.Page.Add(page);
                //_context.Page.Add(contactPage);
                //_context.Page.Add(PrivacyPloicyPage);
                //_context.SaveChanges(); 

                var ListOfPage= new List<Page>()
                {
                    new Page(){Title = "About Us",Slug = "About-us"},
                    new Page(){ Title = "Contact", Slug = "Contact" },
                    new Page(){ Title = "Privacy Policy", Slug = "privacy" }
                };   
                _context.Page.AddRange(ListOfPage); 
                _context.SaveChanges();

            }
        }
    }
}
