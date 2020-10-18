using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSolution.Web.Domain;

namespace PMSolution.Web.ViewModels
{
    public class SearchPatientsViewModel
    {
        public IEnumerable<Patient> SearchPatients { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}