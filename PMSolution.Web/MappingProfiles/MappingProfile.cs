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
            // Domain to ViewModels mappings
            CreateMap<Patient, AddEditPatientViewModel>();
            CreateMap<Patient, PatientViewModel>();
            CreateMap<Patient, PatientsViewModel>();
            CreateMap<Staff, AddEditStaffViewModel>();
            CreateMap<Staff, StaffsViewModel>();
            CreateMap<Staff, StaffViewModel>();
            CreateMap<Clinic, ClinicViewModel>();
            CreateMap<Clinic, AddEditClinicViewModel>();
            
            // ViewModels to Domain mappings
            CreateMap<AddEditPatientViewModel, Patient>();
            CreateMap<AddEditStaffViewModel, Staff>();
            CreateMap<AddEditClinicViewModel, Clinic>();
            CreateMap<ClinicViewModel, Clinic>();
        }
    }
}