using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vax_Aid.Models;

namespace Vax_Aid.ViewModels
{
    public class UserFormVM
    {
        public int UserFormVMId { get; set; }
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }
        public string Ethnicity { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime DateofBirth { get; set; }
        public Address Address { get; set; }
        [Required]
        public int AddressId { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Not a valid Email")]
        public string Email { get; set; }
        public decimal Phone { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string IdentityType { get; set; }
        [Required]
        public string IdentityNo { get; set; }
        public string Occupation { get; set; }
        public string MedicalConditions { get; set; }
        [Required]
        public string DoseType { get; set; }
        public bool Disability { get; set; }
    


        public VaccineInfo vaccineInfo { get; set; }
        public int VaccineInfoId { get; set; }

        public VendorLocation VendorLocation { get; set; }
        public int VendorLocationId { get; set; }

        public UserViewModelVM userViewModel { get; set; }
        public UserDetails UserDetails { get; set; }
        public int UserDetailsId { get; set; }



    }
}
