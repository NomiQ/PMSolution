using System;
using System.Collections.Generic;
using PMSolution.Web.Domain;
using PMSolution.Web.ViewModels;

namespace PMSolution.Web.Services
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> Appointments { get; }
        List<Appointment> GetSelectedAppointments(DateTime date);
        bool AddAppointment(Appointment appointment);
    }
}