using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSolution.Web.Domain
{
    public class ClinicDay
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }

        public int ClinicId { get; set; }
    }
}