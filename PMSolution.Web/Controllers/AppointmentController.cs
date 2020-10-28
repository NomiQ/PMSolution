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
using PMSolution.Web.Enums;

namespace PMSolution.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IClinicRepository _clinicRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository, 
                                        IPatientRepository patientRepository,
                                        IStaffRepository staffRepository,
                                        IClinicRepository clinicRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _staffRepository = staffRepository;
            _clinicRepository = clinicRepository;
        }
        public ActionResult Index(DateTime date)
        {
            // if date is null, store todays date.. not required (but safe to keep in)
            var todaysDate = DateTime.Today.Date;
            DateTime actualDate = date == null ? todaysDate : date;

            // get appoitnemts for the required date
            var appointments = _appointmentRepository.GetSelectedAppointments(actualDate).ToList();
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
                Date = actualDate,
                AllAppointments = appointmentList
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult SelectAppointmentDate(int id)
        {
            var patient = _patientRepository.GetPatient(id);
            
            if (patient != null)
            {
                var model = new SelectAppointmentDateViewModel
                {
                    PatientId = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    DateOfBirth = patient.DateOfBirth,
                    PhoneNumber = patient.PhoneNumber
                };

                return View(model);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult SelectAppointmentDate(SelectAppointmentDateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var day = model.Date.DayOfWeek.ToString();

                var enumDay = (WeekDays)Enum.Parse(typeof(WeekDays), day);
                // check if clinic is open on the date
                var clinicIsOpen = _clinicRepository
                                        .CheckDayExists(enumDay);
                if (clinicIsOpen)
                {
                    return RedirectToAction("AvailableAppointments", new { id = model.PatientId, date = model.Date });
                }
                else
                {
                    ModelState.AddModelError("Date", $"Clinic is not open on {model.Date.DayOfWeek}");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult AvailableAppointments(int id, DateTime date)
        {
            if (ModelState.IsValid)
            {
                // get patient
                var patient = _patientRepository.GetPatient(id);

                //get clinic info
                var clinic = _clinicRepository.GetClinic();

                // get availabe list of appointmets for selected date
                var appointments = _appointmentRepository
                                        .GetAvailableAppointments(date, clinic.Id);

                var appointmentsList = new List<SelectListItem>();

                foreach (var app in appointments)
                {
                    appointmentsList.Add(new SelectListItem { Value = app.StartTime, Text = app.StartTime });
                }

                // construct model
                BookAppointmentViewModel bookAppointmentModel = new BookAppointmentViewModel()
                {
                    PatientId = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    DateOfBirth = patient.DateOfBirth,
                    PhoneNumber = patient.PhoneNumber,
                    Date = date,
                    Appointments = appointmentsList
                };

                return View(bookAppointmentModel);
            }

            return HttpNotFound();
            
        }

        [HttpPost]
        public ActionResult AvailableAppointments(BookAppointmentViewModel app)
        {
            //TODO... check why the date is not 

            if (ModelState.IsValid)
            {
                var sTime = app.StartTime;
                var eTime = app.EndTime;
                string id = User.Identity.GetUserId();

                var appointment = new Appointment()
                {
                    Date = app.Date,
                    StartTime = sTime,
                    EndTime = eTime,
                    PatientId = app.PatientId,
                    UserId = id
                };

                var created = _appointmentRepository.AddAppointment(appointment);

                if (created)
                {
                    return RedirectToAction("Index", new { date = app.Date});
                }

                return HttpNotFound();
            }

            return RedirectToAction("AvailableAppointments", new { id = app.PatientId, date = app.Date });
        }

        [HttpGet]
        public ActionResult DeleteAppointment(int id)
        {
            var appointment = _appointmentRepository.GetAppointment(id);

            if (appointment != null)
            {
                // delete appointment
                var deleted = _appointmentRepository.DeleteAppointment(appointment);

                if (deleted)
                {
                    return RedirectToAction("Index", new { date = appointment.Date });
                }

                return new HttpStatusCodeResult(400);
            }

            return HttpNotFound();
        }
    }
}