using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dernek.Models
{
    public class parameter
    {
        public int id { get; set; }
        [Required]
        public string Parameter { get; set; }
    }
}
