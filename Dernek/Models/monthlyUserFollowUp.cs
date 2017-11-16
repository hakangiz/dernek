using Dernek.Models.EnumProperty;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dernek.Models
{
    public class monthlyUserFollowUp
    {
        public  int id { get; set; }

        [DisplayName("Geçerli Ay")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime month { get; set; }

        [DisplayName("Katılım Durumu")]
        public Boolean followStatus { get; set; }

        [DisplayName("Katılım Ay")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime followDate { get; set; }

        [DisplayName("İşlem Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime actionDate { get; set; }

        [DisplayName("İşlemi Kaydeden")]
        public string createrUserId { get; set; }
        public string applicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}