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

        public AppointmentRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Appointment> Appointments
        {
            get
            {
                return _appDbContext.Appointments;
            }
        }

        //public bool CheckAppointmentTime(DateTime date, string sTime, string eTime)
        //{
        //    var exists = _appDbContext.Appointments
        //                        .Any(s => s.Date == date &&
        //                            s.StartTime == sTime &&
        //                            s.EndTime == eTime);



        //}

        public bool AddAppointment(Appointment appointment)
        {
            _appDbContext.Appointments.Add(appointment);
            var created = _appDbContext.SaveChanges();
            return created > 0;
        }
    }
}