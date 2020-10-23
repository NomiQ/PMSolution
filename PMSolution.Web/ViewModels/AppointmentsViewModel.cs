using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMSolution.Web.ViewModels
{
    public class AppointmentsViewModel
    {
        public IList<AppointmentViewModel> AllAppointments { get; set; }

        [Display(Name = "Select date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please select date")]
        public DateTime Date { get; set; }
    }
}