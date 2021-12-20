using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vax_Aid.Models
{
    public class UserDetails
    {
        [Key]
        public int UserDetailsId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public decimal Phone { get; set; }
    }
}
