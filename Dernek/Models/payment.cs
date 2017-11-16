using Dernek.Models.EnumProperty;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dernek.Models
{
    public class payment
    {
        public int id { get; set; }
        public int? activityId { get; set; }
        //[ForeignKey("activityId")]
        public virtual activity Activity { get; set; }
        [DisplayName("Toplam Tutar")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public double payTotal { get; set; }
        [DisplayName("Ödeme Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime paymentDate { get; set; }
        
        public string createrUserId { get; set; }
        [DisplayName("Ödeme Ayı")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime mounth { get; set; }
        [DisplayName("Açıklama")]
        public string description { get; set; }

        public  string applicationUserId { get; set; }
        //[ForeignKey("applicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
