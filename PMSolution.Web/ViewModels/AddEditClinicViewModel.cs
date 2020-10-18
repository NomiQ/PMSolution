using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMSolution.Web.ViewModels
{
    public class AddEditClinicViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Clinic name")]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string FaxNumber { get; set; }

        [Required]
        public string Email { get; set; }
    }
}