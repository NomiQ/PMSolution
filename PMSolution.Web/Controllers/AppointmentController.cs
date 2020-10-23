using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PMSolution.Web.Domain;
using PMSolution.Web.Services;
using PMSolution.Web.ViewModels;
using Microsoft.AspNet.Identity;

namespace PMSolution.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IStaffRepository _staffRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository, 
                                        IPatientRepository patientRepository,
                                        IStaffRepository staffRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _staffRepository = staffRepository;
        }
        public ActionResult Index(DateTime date)
        {
            // get appoitnemts for the required date
            var appointments = _appointmentRepository.GetSelectedAppointments(date).ToList();
            var appointmentList = new List<AppointmentViewModel>();

            if (appointments.Count > 0)
            {
                foreach (var app in appointments)
                {
                    var patient = _patientRepository.GetPatient(app.PatientId);

                    var appDetail = new AppointmentViewModel
                    {
                        Id = app.Id,
                        Date = app.Date,
                        StartTime = app.StartTime,
                        EndTime = app.EndTime,
                        PatientId = patient.Id,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        PhoneNumber = patient.PhoneNumber,
                        DateOfBirth = patient.DateOfBirth,
                        CNIC = patient.CNIC
                    };
                    appointmentList.Add(appDetail);
                }
            }

            var model = new AppointmentsViewModel()
            {
                Date = date,
                AllAppointments = appointmentList
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult AddAppointment(int id)
        {
            var patient = _patientRepository.GetPatient(id);
            if (patient != null)
            {
                //add patient details to appointment view model
                var createAppointment = new AppointmentViewModel
                {
                    PatientId = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    DateOfBirth = patient.DateOfBirth,
                    PhoneNumber = patient.PhoneNumber,
                    Hours = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "1", Text = "01:00" },
                        new SelectListItem { Value = "2", Text = "02:00" },
                        new SelectListItem { Value = "3", Text = "03:00" },
                        new SelectListItem { Value = "4", Text = "04:00" },
                        new SelectListItem { Value = "5", Text = "05:00" },
                        new SelectListItem { Value = "6", Text = "06:00" },
                        new SelectListItem { Value = "7", Text = "07:00" },
                        new SelectListItem { Value = "8", Text = "08:00" },
                        new SelectListItem { Value = "9", Text = "09:00" },
                        new SelectListItem { Value = "10", Text = "10:00" },
                        new SelectListItem { Value = "11", Text = "11:00" },
                        new SelectListItem { Value = "12", Text = "12:00" }
                    },
                    Minutes = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "60", Text = "00" },
                        new SelectListItem { Value = "05", Text = "05" },
                        new SelectListItem { Value = "10", Text = "10" },
                        new SelectListItem { Value = "15", Text = "15" },
                        new SelectListItem { Value = "20", Text = "20" },
                        new SelectListItem { Value = "25", Text = "25" },
                        new SelectListItem { Value = "30", Text = "30" },
                        new SelectListItem { Value = "35", Text = "35" },
                        new SelectListItem { Value = "40", Text = "40" },
                        new SelectListItem { Value = "45", Text = "45" },
                        new SelectListItem { Value = "50", Text = "50" },
                        new SelectListItem { Value = "55", Text = "55" }
                    },
                    AMPM = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "AM", Text = "AM" },
                        new SelectListItem { Value = "PM", Text = "PM" }
                    }
                    

                    
                };

                return View(createAppointment);
            }

            return HttpNotFound("Patient details not found in the system"); 
        }

        [HttpPost]
        public ActionResult AddAppointment(AppointmentViewModel app)
        {
            // check if model is valid

            if (ModelState.IsValid)
            {
                var sTime = $"{app.StartTimeHour}:{app.StartTimeMin} {app.StartAMPM}";
                var eTime = $"{app.EndTimeHour}:{app.EndTimeMin} {app.EndAMPM}";
                string id = User.Identity.GetUserId();

                var appointment = new Appointment()
                {
                    Date = app.Date,
                    StartTime = sTime,
                    EndTime = eTime,
                    PatientId = app.PatientId
                    //StaffId = staffId
                };

                var created = _appointmentRepository.AddAppointment(appointment);

                if (created)
                {
                    return RedirectToAction("Index");
                }

                return HttpNotFound();
            }

            return View();
            
        }

    }
}