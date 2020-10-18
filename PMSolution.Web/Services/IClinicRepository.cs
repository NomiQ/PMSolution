using PMSolution.Web.Domain;

namespace PMSolution.Web.Services
{
    public interface IClinicRepository
    {
        Clinic GetClinic();
        Clinic GetClinicDetails(int id);
        bool AddClinic(Clinic clinic);
        bool EditClinic(Clinic clinic);
        bool Delete(Clinic clinic);


        bool IsClinicDay(string day);
        bool AddClinicDay(ClinicDay clinicDay);
    }
}