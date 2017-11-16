using Dernek.Models;
using Dernek.Models.EnumProperty;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Web;

[assembly: OwinStartupAttribute(typeof(Dernek.Startup))]
namespace Dernek
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            //HanDbContext hanContext = new HanDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            if (!roleManager.RoleExists(enums.Roles.Accounting.ToString()))
            {
                var roleAccounting = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                roleAccounting.Name = enums.Roles.Accounting.ToString();
                roleManager.Create(roleAccounting);
            }
            if (!roleManager.RoleExists(enums.Roles.Member.ToString()))
            {
                var roleMember = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                roleMember.Name = enums.Roles.Member.ToString();
                roleManager.Create(roleMember);

                //Add User
                var user1 = new ApplicationUser();
                user1.UserName = "semih@gmail.com";
                user1.Email = "semih@gmail.com";
                user1.LockoutEnabled = true;

                //////For Details
                //var user1Detail = new userDetail
                //{
                //    iban = "TR00345",
                //    identityNo = "5555555555",
                //    name = "semih",
                //    surname = "yıldız",
                //    ApplicationUser = user1
                //};

                //user1.userDetail = user1Detail;

                var chkUser = UserManager.Create(user1, "123456");

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user1.Id, enums.Roles.Member.ToString());

                }


                var user2 = new ApplicationUser();
                user2.UserName = "mehmet@gmail.com";
                user2.Email = "mehmet@gmail.com";
                user2.LockoutEnabled = true;

                //var user2Detail = new userDetail
                //{
                //    iban = "TR00200",
                //    identityNo = "74587769",
                //    name = "mehmet",
                //    surname = "aslan",
                //    ApplicationUser = user2
                //};

                //user2.userDetail = user2Detail;

                var chkUser2 = UserManager.Create(user2, "123456");  

                //Add default User to Role Admin
                if (chkUser2.Succeeded)
                {
                    var result2 = UserManager.AddToRole(user2.Id, enums.Roles.Member.ToString());
                }

            }

            if (!roleManager.RoleExists(enums.Roles.Manager.ToString()))
            {
                var roleM = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                roleM.Name = enums.Roles.Manager.ToString();
                roleManager.Create(roleM);

                //Add User
                var user = new ApplicationUser();
                user.UserName = "hakangiz@gmail.com";
                user.Email = "hakangiz@gmail.com";
                user.LockoutEnabled = true;

                //var userDetail = new userDetail
                //{
                //    iban = "TR00345",
                //    identityNo = "1766666666",
                //    name = "hakan",
                //    surname = "cengiz",
                //    ApplicationUser = user
                //};

                //user.userDetail = userDetail;

                var chkUser = UserManager.Create(user, "123456");

                

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, enums.Roles.Manager.ToString());

                    //Add default activity
                    activity act = new activity();
                    act.activityTypes = enums.activityTypes.Kurs;
                    act.location = "Koyundere Kapalı Spor Salonu";
                    act.price = 40;
                    //act.createrUserId = UserManager.FindById(HttpContext.Current.User.Identity.GetUserId()).Id;
                    act.description = "Bla Bla";
                    act.name = "Halk Oyunları (Ege Yöresi)";
                    act.endDate = DateTime.Today;
                    act.startDate = DateTime.Today;
                    act.recordDate = DateTime.Today;
                    act.status = enums.activityStatus.Open;
                    context.activity.Add(act);

                    user.activities.Add(act);

                    context.SaveChanges();
                }

                //Society Case Account
                var caseUser = new ApplicationUser();
                caseUser.UserName = "Han";
                caseUser.Email = "han@han.com";
                caseUser.LockoutEnabled = true;

                //var caseUserDetail = new userDetail
                //{
                //    iban = "",
                //    identityNo = "",
                //    name = "Han",
                //    surname = "Halk Oyunları",
                //    ApplicationUser = caseUser
                //};

                //caseUser.userDetail = caseUserDetail;

                var chkCaseUser = UserManager.Create(caseUser, "123456");
                //Add default User to Role Admin
                if (chkCaseUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(caseUser.Id, enums.Roles.Manager.ToString());
                    context.SaveChanges();
                }
            }


        }
    }
}
