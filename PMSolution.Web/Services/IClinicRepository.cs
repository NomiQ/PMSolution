using System.Collections.Generic;
using PMSolution.Web.Domain;

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


        bool IsClinicDay(string day);
        ClinicDay GetClinicDay(int id);
        List<string> GetClinicDays(int id);
        bool AddClinicDay(ClinicDay clinicDay);
        bool UpdateClinicDay(ClinicDay clinicDay);
        bool DeleteClinicDay(ClinicDay clinicDay);
    }
}