using Dernek.Models;
using Dernek.Models.EnumProperty;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dernek.Models
{
    public class activity
    {
        public int id { get; set; }
        public enums.activityTypes activityTypes { get; set; }
        [DisplayName("Aktivite Adı")]
        public string name { get; set; }
        [DisplayName("Kayıt Tarihi")]
        public DateTime recordDate { get; set; }
        [DisplayName("Başlangıç Tarihi")]
        public DateTime startDate { get; set; }
        [DisplayName("Bitiş tarihi")]
        public DateTime endDate { get; set; }
        [DisplayName("Lokasyon")]
        public string location { get; set; }
        [DisplayName("Fiyatı")]
        public double price { get; set; }
        [DisplayName("Miktarı")]
        public double? quantity { get; set; }
        [DisplayName("Maliyet")]
        public double? cost { get; set; }
        [DisplayName("Açıklama")]
        public string description { get; set; }
        [DisplayName("Km")]
        public double? km { get; set; }
        public string createrUserId { get; set; }
        
        public double? dancerPerRate { get; set; } 
        public enums.activityStatus status { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public virtual ICollection<payment> Payments { get; set; }


        public activity()
        {
            ApplicationUsers = new HashSet<ApplicationUser>();
            Payments = new HashSet<payment>();
        }

    }
}
