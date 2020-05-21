using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coreprojecr2.Models
{
    public class Register
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        [Compare("Password", ErrorMessage ="Password and Confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
