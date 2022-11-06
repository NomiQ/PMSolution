using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PMSolution.Web.Enums;
using PMSolution.Web.Domain;

namespace PMSolution.Web.ViewModels
{
    public class PatientViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Medical refernce number")]
        public string MRN { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string CNIC { get; set; }
        
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public bool Ind { get; set; }
    }
}