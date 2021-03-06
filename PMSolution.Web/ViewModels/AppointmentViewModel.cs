﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMSolution.Web.Domain;

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
        public string StartTime { get; set; }

        [Required]
        [Display(Name = "End time")]
        public string EndTime { get; set; }

        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNIC { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<Appointment> Appointments { get; set; }

        public int ClinicId { get; set; }
        public int StaffId { get; set; }

    }
}