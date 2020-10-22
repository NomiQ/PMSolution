using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSolution.Web.Enums;

namespace PMSolution.Web.Domain
{
    public class ClinicDay
    {
        public int Id { get; set; }
        public WeekDays Day { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }

        public int ClinicId { get; set; }
    }
}