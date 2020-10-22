using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PMSolution.Web.Domain;
using PMSolution.Web.Enums;

namespace PMSolution.Web.ViewModels
{
    public class AddEditStaffViewModel
    {
        public int Id { get; set; }

        [Required]
        public StaffType? Type { get; set; }

        [Required]
        public Gender? Gender { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string CNIC { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }
    }
}