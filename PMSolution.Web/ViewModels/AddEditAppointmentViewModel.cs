using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSolution.Web.ViewModels
{
    public class AddEditAppointmentViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public int PatientId { get; set; }
        public int StaffId { get; set; }
    }
}