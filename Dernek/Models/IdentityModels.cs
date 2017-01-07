using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using Dernek.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dernek.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int userId { get; set; }
        public bool realMember { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public virtual ICollection<activity> activities { get; set; }

        public virtual userDetail userDetail { get; set; }

        public virtual ICollection<payment> payments { get; set; }

        public virtual ICollection<monthlyUserFollowUp> monthlyUserFollowUp { get; set; }

        public ApplicationUser()
        {
            activities = new HashSet<activity>();
            payments = new HashSet<payment>();
            monthlyUserFollowUp = new HashSet<monthlyUserFollowUp>();
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("HanDbContext", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }

        public DbSet<activity> activity { get; set; }
        public DbSet<parameter> parameter { get; set; }
        public DbSet<payment> payment { get; set; }
        public DbSet<userDetail> userDetail { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<activity>()
                .HasMany(x => x.ApplicationUsers)
                .WithMany(t => t.activities)
                .Map(a =>
                {
                    a.MapLeftKey("activityId");
                    a.MapRightKey("userId");
                    a.ToTable("userActivity");
                }
                );

            modelBuilder.Entity<ApplicationUser>().Property(x => x.userId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<userDetail>()
                .HasOptional(a => a.ApplicationUser)
                .WithOptionalDependent(b => b.userDetail)
                .Map(a=>a.MapKey("userId")
                );


            //modelBuilder.Entity<payment>()
            //    .HasOptional<ApplicationUser>(a => a.ApplicationUser)
            //    .WithMany(x => x.payments)
            //    .Map(c => c.MapKey("userId"));
            modelBuilder.Entity<payment>()
                .HasOptional(a => a.ApplicationUser)
                .WithMany(x => x.payments)
                .HasForeignKey(m=>m.applicationUserId);

            //modelBuilder.Entity<payment>()
            //    .HasOptional<activity>(a => a.Activity)
            //    .WithMany(x => x.Payments)
            //    .Map(c => c.MapKey("activityId"));
            modelBuilder.Entity<payment>()
                .HasOptional(a => a.Activity)
                .WithMany(x => x.Payments)
                .HasForeignKey(m => m.activityId);

            modelBuilder.Entity<monthlyUserFollowUp>()
                .HasOptional(x => x.ApplicationUsers)
                .WithMany(t => t.monthlyUserFollowUp)
                .Map(a =>a.MapKey("userId"));

            base.OnModelCreating(modelBuilder);
        }
    }
}