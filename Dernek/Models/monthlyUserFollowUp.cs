using Dernek.Models.EnumProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dernek.Models
{
    public class monthlyUserFollowUp
    {
        public  int id { get; set; }
        public ApplicationUser ApplicationUsers { get; set; }
        public DateTime month { get; set; }
        public Boolean followStatus { get; set; }
        public DateTime followDate { get; set; }
        public DateTime actionDate { get; set; }
        public string createrUserId { get; set; }
    }
}