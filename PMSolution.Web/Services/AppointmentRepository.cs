using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSolution.Web.Domain;
using PMSolution.Web.Models;

namespace PMSolution.Web.Services
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly IClinicRepository _clinicRepository;

        public AppointmentRepository(ApplicationDbContext appDbContext,
                                    IClinicRepository clinicRepository)
        {
            _appDbContext = appDbContext;
            _clinicRepository = clinicRepository;
        }

        public IEnumerable<Appointment> Appointments
        {
            get
            {
                return _appDbContext.Appointments;
            }
        }

        public Appointment GetAppointment(int id)
        {
            var appointment = _appDbContext.Appointments
                                    .FirstOrDefault(s => s.Id == id);
            
            return appointment;
        }

        public List<Appointment> GetSelectedAppointments(DateTime date)
        {
            var appointments = _appDbContext.Appointments
                                .Where(s => s.Date == date)
                                .ToList();

            // sort appointments by start time 
            var sortedAppointments = appointments.OrderBy(s => 
                                        (DateTime.Parse(s.StartTime,
                                        System.Globalization.CultureInfo.CurrentCulture)))
                                        .ToList();
            return sortedAppointments;
        }

        public List<Appointment> GetAvailableAppointments(DateTime date, int clinicId)
        {
            var clinicOpeningHours = _clinicRepository
                                            .GetClinicBusinessHours(clinicId);
            var appointmentDay = clinicOpeningHours
                                    .FirstOrDefault(s => s.Day.ToString() == date.DayOfWeek.ToString());

            var appointmentsList = new List<Appointment>();

            DateTime startTime = Convert.ToDateTime(appointmentDay.OpenTime);
            DateTime endTime = Convert.ToDateTime(appointmentDay.CloseTime);

            double openTime = Convert.ToDouble(startTime.Hour) + Convert.ToDouble(startTime.Minute) / 60;
            double closeTime = Convert.ToDouble(endTime.Hour) + Convert.ToDouble(endTime.Minute) / 60;
            

            int duration = 30;
            double increment = 0.5;

            while (openTime < closeTime)
            {
                string startTimeString = startTime.ToString("hh:mm tt");

                var appointmentExists = _appDbContext.Appointments
                                                .Any(s => s.Date == date
                                                && s.StartTime == startTimeString);
                if (!appointmentExists)
                {
                    appointmentsList.Add(new Appointment()
                    {
                        Date = date,
                        StartTime = startTimeString,
                        EndTime = startTime.AddMinutes(duration).ToString("hh:mm tt")
                });
                }
                startTime = startTime.AddMinutes(duration);
                openTime += increment;

            }

            return appointmentsList;
        }

        public bool AddAppointment(Appointment appointment)
        {
            _appDbContext.Appointments.Add(appointment);
            var created = _appDbContext.SaveChanges();
            return created > 0;
        }

        public bool DeleteAppointment(Appointment appointment)
        {
            // remove appointment
            _appDbContext.Appointments.Remove(appointment);
            var removed = _appDbContext.SaveChanges();

            return removed > 0;
        }
    }
}