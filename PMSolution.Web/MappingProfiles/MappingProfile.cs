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
            CreateMap<AddEditPatientViewModel, Patient>();
            CreateMap<Patient, PatientViewModel>();
            CreateMap<Patient, PatientsViewModel>();

            CreateMap<AddEditStaffViewModel, Staff>();
            CreateMap<Staff, StaffsViewModel>();
            CreateMap<Staff, StaffViewModel>();
        }
    }
}