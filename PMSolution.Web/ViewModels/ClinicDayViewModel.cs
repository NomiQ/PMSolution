﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMSolution.Web.Enums;

namespace PMSolution.Web.ViewModels
{
    public class ClinicDayViewModel
    {
        public int Id { get; set; }

        [Required]
        public WeekDays Day { get; set; }

        [Required]
        public string StartTimeHour { get; set; }

        [Required]
        public string StartTimeMin { get; set; }

        [Required]
        public string EndTimeHour { get; set; }

        [Required]
        public string EndTimeMin { get; set; }

        [Required]
        public string StartAMPM { get; set; }

        [Required]
        public string EndAMPM { get; set; }

        public int ClinicId { get; set; }

        public IEnumerable<SelectListItem> RemainingDays { get; set; }
        public IEnumerable<SelectListItem> Hours { get; set; }
        public IEnumerable<SelectListItem> Minutes { get; set; }
        public IEnumerable<SelectListItem> AMPM { get; set; }



    }
}