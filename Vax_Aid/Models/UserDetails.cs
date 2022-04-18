using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vax_Aid.Models
{
    public class UserDetails
    {
        [Key]
        public int UserDetailsId { get; set; }
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }
        public string Ethnicity { get; set; }
        public string Gender { get; set; }
        public DateTime DateofBirth { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public string Email { get; set; }
        public decimal Phone { get; set; }
        public string Nationality { get; set; }
        public string IdentityType { get; set; }
        public string IdentityNo { get; set; }
        public string Occupation { get; set; }
        public string MedicalConditions { get; set; }
        public string DoseType { get; set; }
        public bool Disability { get; set; }

        public int VendorLocationId { get; set; }
        public int VaccineInfoId { get; set; }


        [ForeignKey("VendorLocationId")]
        public VendorLocation VendorLocation { get; set; }
        [ForeignKey("VaccineInfoId")]
        public VaccineInfo VaccineInfo { get; set; }
        public int FlowStatus { get; set; }
        public string Remarks { get; set; }
    }
}
