using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PMSolution.Web.Domain;
using PMSolution.Web.ViewModels;

namespace PMSolution.Web.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddPatientViewModel, Patient>();
            CreateMap<EditPatientViewModel, Patient>();
            CreateMap<Patient, PatientViewModel>();
        }
    }
}