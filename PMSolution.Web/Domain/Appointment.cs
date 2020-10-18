using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSolution.Web.Domain
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public int PatientId { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
    }
}