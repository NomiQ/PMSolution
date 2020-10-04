using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSolution.Web.Domain;

namespace PMSolution.Web.ViewModels
{
    public class PatientsViewModel
    {
        public IEnumerable<Patient> AllPatients { get; set; }
    }
}