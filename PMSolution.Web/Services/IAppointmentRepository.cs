using System;
using System.Collections.Generic;
using PMSolution.Web.Domain;
using PMSolution.Web.ViewModels;

namespace PMSolution.Web.Services
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> Appointments { get; }
        Appointment GetAppointment(int id);
        List<Appointment> GetSelectedAppointments(DateTime date);
        List<Appointment> GetAvailableAppointments(DateTime date, int clinicId);
        bool AddAppointment(Appointment appointment);
        bool DeleteAppointment(Appointment appointment);
    }
}