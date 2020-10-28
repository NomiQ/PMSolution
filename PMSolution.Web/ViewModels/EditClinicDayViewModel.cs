using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMSolution.Web.Enums;

namespace PMSolution.Web.ViewModels
{
    public class EditClinicDayViewModel
    {
        public int Id { get; set; }

        public WeekDays Day { get; set; }

        public string StartTimeHour { get; set; }

        public string StartTimeMin { get; set; }

        public string EndTimeHour { get; set; }

        public string EndTimeMin { get; set; }

        public string StartAMPM { get; set; }

        public string EndAMPM { get; set; }

        public string customError { get; set; }

        public int ClinicId { get; set; }

        public IEnumerable<SelectListItem> Hours { get; set; }
        public IEnumerable<SelectListItem> Minutes { get; set; }
        public IEnumerable<SelectListItem> AMPM { get; set; }
    }
}