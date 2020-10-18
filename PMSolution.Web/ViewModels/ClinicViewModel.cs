using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSolution.Web.Domain;

namespace PMSolution.Web.ViewModels
{
    public class ClinicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }

        public List<ClinicDay> ClinicDays { get; set; }
    }
}