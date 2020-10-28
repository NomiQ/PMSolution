using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMSolution.Web.Domain;

namespace PMSolution.Web.ViewModels
{
    public class BookAppointmentViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        
        [Display(Name = "Available appointments")]
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<SelectListItem> Appointments { get; set; }
    }
}