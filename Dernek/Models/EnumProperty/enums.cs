using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dernek.Models.EnumProperty
{
    public class enums
    {
        //It must writing in userId place for payment table. //Represents the Society Case.
        public static ApplicationUser GetCaseUser()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Users.Where(x => x.UserName == "Han").First();
        }
        public enum Roles
        {
            Manager,
            Accounting,
            Member
        };

        public enum activityTypes
        {
            Wedding, //Düğün
            IntraSocietyActivities, //Piknik vb.
            Kurs, 
            Sales, //Tshirt satışı
            Donate //Bağış
        }

        public enum activityStatus
        {
            Pending,
            Open,
            Closed,
            Deleted
        }


        

        ////Roles
        //public const string Yönetici= "Yönetici";
        //public const string Muhasebe = "Muhasebe";
        //public const string Üye = "Üye";
    }
}