using System.Collections.Generic;
using PMSolution.Web.Domain;
using PMSolution.Web.Enums;

namespace PMSolution.Web.Services
{
    public interface IClinicRepository
    {
        Clinic GetClinic();
        Clinic GetClinic(int id);
        Clinic GetClinicDetailsOnly(int id);
        bool AddClinic(Clinic clinic);
        bool EditClinic(Clinic clinic);
        bool DeleteClinic(Clinic clinic);


        ClinicDay GetClinicDay(int id);
        IEnumerable<WeekDays> GetClinicDays(int id);
        bool AddClinicDay(ClinicDay clinicDay);
        bool UpdateClinicDay(ClinicDay clinicDay);
        bool DeleteClinicDay(ClinicDay clinicDay);
    }
}