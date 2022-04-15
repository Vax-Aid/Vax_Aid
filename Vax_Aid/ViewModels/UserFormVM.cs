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
    


        public VaccineInfo vaccineInfo { get; set; }
        public int VaccineInfoId { get; set; }

        public VendorLocation VendorLocation { get; set; }
        public int VendorLocationId { get; set; }

        public UserViewModelVM userViewModel { get; set; }
        public UserDetails UserDetails { get; set; }
        public int UserDetailsId { get; set; }



    }
}
