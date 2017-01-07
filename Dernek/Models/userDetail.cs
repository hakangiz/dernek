using Dernek.Models.EnumProperty;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dernek.Models
{
    public class userDetail
    {
        
        public int? id { get; set; }
        [DisplayName("Adı")]
        public string name { get; set; }

        [DisplayName("Soyadı")]
        public string surname { get; set; }

        [DisplayName("Meslek")]
        public string job { get; set; }

        [DisplayName("Kimlik Seri No")]
        public string serialNumber { get; set; }

        [DisplayName("TC Kimlik No")]
        public string identityNo { get; set; }

        [DisplayName("Doğum Yeri")]
        public string placeOfBirth { get; set; }

        [DisplayName("Doğum Tarihi")]
        public DateTime? dateOfBirth { get; set; }

        [DisplayName("Ana Adı")]
        public string motherName { get; set; }

        [DisplayName("Baba Adı")]
        public string fatherName { get; set; }

        [DisplayName("Kan Grubu")]
        public string bloodGroup { get; set; }

        [DisplayName("Medeni Hali")]
        public string maritalStatus { get; set; }

        [DisplayName("İl")]
        public string county { get; set; }

        [DisplayName("İlçe")]
        public string city { get; set; }

        [DisplayName("Mahalle")]
        public string district { get; set; }

        [DisplayName("Köy")]
        public string town { get; set; }

        [DisplayName("İban")]
        public string iban { get; set; }

        [DisplayName("Cilt No")]
        public string skinNo { get; set; }

        [DisplayName("Sıra No")]
        public string rowNo { get; set; }

        [DisplayName("Aile Sıra No")]
        public string familyRowNo { get; set; }

    [DisplayName("Verildiği Yer")]
        public string placeOfGiven { get; set; }

        [DisplayName("Veriliş Tarihi")]
        public string dateOfGiven { get; set; }

        [DisplayName("Cep Tel1")]
        public string tel1 { get; set; }

        [DisplayName("Cep Tel2")]
        public string tel2 { get; set; }

        [DisplayName("Adres")]
        public string address { get; set; }
        [DisplayName("Kullanıcı Resmi")]
        public byte[] userImage { get; set; }
        public string userImageType { get; set; }

        [DisplayName("Facebook")]
        public string facebookAccount { get; set; }

        [DisplayName("Tweeter")]
        public string tweeterAccount { get; set; }

        [DisplayName("Instagram")]
        public string instagramAccount { get; set; }

        [DisplayName("Swarm")]
        public string swarmAccount { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}