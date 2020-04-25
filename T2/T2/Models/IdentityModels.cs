using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace T2.Models
{
    public class ApplicationUser : IdentityUser
    {
        /*First name*/
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        /*Last name*/
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        /*Level 1 represents Admin, Level 2 certificator user, Level 3 normal user*/
        [Required]
        public byte Level { get; set; } = 3;
        
        /*Total segnalation*/
        [Required]
        public int TotalSegnalation{ get; set; } = 0;

        /*Date of subscription*/
        [Required]
        public System.DateTime JoinDate { get; set; } = DateTime.Now;

        /*Total correct answers*/
        [Required]
        public int NCorrectAnswers { get; set; } = 0;

        /*Total fault answers*/
        [Required]
        public int NFaultAnswers { get; set; } = 0;



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here

            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<LinksModel> Links
        {
            get;
            set;
        }
        public DbSet<CategoriesModel> Categories
        {
            get;
            set;
        }
        public DbSet<RatiesModel> Raties
        {
            get;
            set;
        }
        public DbSet<ExpertizeModel> Expertize
        {
            get;
            set;
        }

        public DbSet<WhitelistModel> Whitelist
        {
            get;
            set;
        }

        public DbSet<BlacklistModel> Blacklist
        {
            get;
            set;
        }

        public DbSet<OptionsModel> Options        {
            get;
            set;
        }



        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}