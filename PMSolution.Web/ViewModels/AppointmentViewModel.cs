using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMSolution.Web.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Appointment date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Start time")]
        public string StartTimeHour { get; set; }

        [Required]
        public string StartTimeMin { get; set; }

        [Required]
        [Display(Name = "End time")]
        public string EndTimeHour { get; set; }

        [Required]
        public string EndTimeMin { get; set; }

        public string StartAMPM { get; set; }

        public string EndAMPM { get; set; }


        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int StaffId { get; set; }


        public IEnumerable<SelectListItem> Hours { get; set; }
        public IEnumerable<SelectListItem> Minutes { get; set; }

        public IEnumerable<SelectListItem> AMPM { get; set; }
    }
}