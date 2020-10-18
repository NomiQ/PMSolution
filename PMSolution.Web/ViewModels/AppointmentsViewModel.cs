using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSolution.Web.ViewModels
{
    public class AppointmentsViewModel
    {
        public IList<AppointmentViewModel> Appointments { get; set; }
    }
}