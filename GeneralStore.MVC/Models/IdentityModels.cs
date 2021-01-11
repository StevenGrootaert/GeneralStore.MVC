using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GeneralStore.MVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //this below was added in step "product controller and index"
        public DbSet<GeneralStore.MVC.Models.Product> Products { get; set; }
        // I don't remember adding this one though. how did this automajically add this? 
        // I could delete the system.data.entity - that's how I know I didn't type this. 
        public System.Data.Entity.DbSet<GeneralStore.MVC.Models.Customer> Customers { get; set; }
        // I'm adding this one now becuase I made a new controller and I want the index to return a view of the transactions to a list
        //public DbSet<GeneralStore.MVC.Models.Transaction> Transactions { get; set; }
        // an easier way to write this from Nick's walkthrough. 
        public DbSet<Transaction> Transactions { get; set; }
    }
}