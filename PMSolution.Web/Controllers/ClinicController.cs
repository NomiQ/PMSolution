using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PMSolution.Web.Domain;
using PMSolution.Web.Enums;
using PMSolution.Web.Models;
using PMSolution.Web.Services;
using PMSolution.Web.ViewModels;

namespace PMSolution.Web.Controllers
{
    public class ClinicController : Controller
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public ClinicController(IClinicRepository clinicRepository,
                                IAppointmentRepository appointmentRepository)
        {
            _clinicRepository = clinicRepository;
            _appointmentRepository = appointmentRepository;
        }

        public ActionResult Index()
        {
            // retrieve clinic info if exists
            var clinic = _clinicRepository.GetClinic();

            if (clinic != null)
            {
                var clinicViewModel = new ClinicViewModel()
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    PhoneNumber = clinic.PhoneNumber,
                    Address = clinic.Address,
                    FaxNumber = clinic.FaxNumber,
                    Email = clinic.Email,
                    // ensure days are in order
                    ClinicDays = clinic.ClinicDays.OrderBy(s => (int)(s.Day)).ToList()
                };
                
                return View(clinicViewModel);
            }

            return View();
        }

        [HttpGet]
        public ActionResult AddClinic()
        {
            var clinic = new AddEditClinicViewModel();
            return View(clinic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClinic(AddEditClinicViewModel addClinic)
        {
            if (ModelState.IsValid)
            {
                // add clinic
                var clinic = Mapper.Map<AddEditClinicViewModel, Clinic>(addClinic);
                var created = _clinicRepository.AddClinic(clinic);

                if (created)
                {
                    return RedirectToAction("Index");
                }

                return HttpNotFound();
                
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult EditClinic(int id)
        {
            // get clinic details
            var clinic = _clinicRepository.GetClinicDetailsOnly(id);

            if (clinic != null)
            {
                // map to view model
                var editClinic = Mapper.Map<Clinic, AddEditClinicViewModel>(clinic);

                return View(editClinic);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClinic(AddEditClinicViewModel editClinic)
        {
            if (ModelState.IsValid)
            {
                var clinic = Mapper.Map<AddEditClinicViewModel, Clinic>(editClinic);
                var updated = _clinicRepository.EditClinic(clinic);

                if (updated)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(editClinic);
        }

        [HttpGet]
        public ActionResult DeleteClinic(int id)
        {
            // check if clinic exists
            var clinic = _clinicRepository.GetClinic(id);

            if (clinic != null)
            {
                var deleted = _clinicRepository.DeleteClinic(clinic);

                if (deleted)
                {
                    return RedirectToAction("Index");
                }
            }

            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult AddClinicDays(int id)
        {
            // get existing days
            var existingDays = _clinicRepository.GetClinicDays(id);

            IEnumerable<SelectListItem> existingDaysList = existingDays.Select(s => new SelectListItem
            {
                Text = s.ToString(),
                Value = ((int)s).ToString()
            });

            // convert enum to selectListItem
            var enumList = Enum.GetValues(typeof(WeekDays)).Cast<WeekDays>().Select(s => new SelectListItem
            {
                Text = s.ToString(),
                Value = ((int)s).ToString()
            });

            // list to filter existing days
            List<SelectListItem> remainingDaysList = new List<SelectListItem>();

            // loop through to filter existing days
            foreach (var day in enumList)
            {
                if (!existingDaysList.Any(s => s.Value == day.Value))
                {
                    remainingDaysList.Add(day);
                }
            }

            // reconstruct IEnumerable select list to display in view
            IEnumerable<SelectListItem> remainingDays = remainingDaysList.Select(s => new SelectListItem
            {
                Text = s.Text,
                Value = s.Value
            });

            // create clinic day
            var clinicDays = new ClinicDayViewModel()
            {
                // copy remaining days
                RemainingDays = remainingDays,
                ClinicId = id,
                Hours = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "01" },
                    new SelectListItem { Value = "2", Text = "02" },
                    new SelectListItem { Value = "3", Text = "03" },
                    new SelectListItem { Value = "4", Text = "04" },
                    new SelectListItem { Value = "5", Text = "05" },
                    new SelectListItem { Value = "6", Text = "06" },
                    new SelectListItem { Value = "7", Text = "07" },
                    new SelectListItem { Value = "8", Text = "08" },
                    new SelectListItem { Value = "9", Text = "09" },
                    new SelectListItem { Value = "10", Text = "10" },
                    new SelectListItem { Value = "11", Text = "11" },
                    new SelectListItem { Value = "12", Text = "12" }
                },
                Minutes = new List<SelectListItem>
                {
                    new SelectListItem { Value = "00", Text = "00" },
                    new SelectListItem { Value = "15", Text = "15" },
                    new SelectListItem { Value = "30", Text = "30" },
                    new SelectListItem { Value = "45", Text = "45" }
                },
                AMPM = new List<SelectListItem>
                {
                    new SelectListItem { Value = "AM", Text = "AM" },
                    new SelectListItem { Value = "PM", Text = "PM" }
                }
            };

            return View(clinicDays);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClinicDays(ClinicDayViewModel clinicDay)
        {
            if (ModelState.IsValid)
            {
                var day = clinicDay.Day;
                
                var openTime = clinicDay.StartTimeHour + ":"
                                + clinicDay.StartTimeMin + " "
                                + clinicDay.StartAMPM;
                var closeTime = clinicDay.EndTimeHour + ":"
                                + clinicDay.EndTimeMin + " "
                                + clinicDay.EndAMPM;

                var addClinicDay = new ClinicDay()
                {
                    Day = day,
                    OpenTime = openTime,
                    CloseTime = closeTime,
                    ClinicId = clinicDay.ClinicId
                };

                // add clinic day
                var created = _clinicRepository.AddClinicDay(addClinicDay);
                if (created)
                {
                    return RedirectToAction("Index");
                }

                return HttpNotFound("Unale to save day. Something went wrong while saving");
            }

            return View(clinicDay);
        }

        [HttpGet]
        public ActionResult EditClinicDay(int id)
        {
            // get clinic day to edit
            var clinicDay = _clinicRepository.GetClinicDay(id);
            if (clinicDay != null)
            {
                var sTime = clinicDay.OpenTime.Split(new string[] { ":", " " }, StringSplitOptions.RemoveEmptyEntries);
                var sHours = sTime[0];
                var sMins = sTime[1];
                var sAMPM = sTime[2];

                var eTime = clinicDay.CloseTime.Split(new string[] { ":", " " }, StringSplitOptions.RemoveEmptyEntries);
                var eHours = eTime[0];
                var eMins = eTime[1];
                var eAMPM = eTime[2];

                // map to view model
                var editClinicdayViewModel = new EditClinicDayViewModel()
                {
                    Id = clinicDay.Id,
                    Day = clinicDay.Day,
                    StartTimeHour = sHours,
                    StartTimeMin = sMins,
                    StartAMPM = sAMPM,
                    EndTimeHour = eHours,
                    EndTimeMin = eMins,
                    EndAMPM = eAMPM,
                    ClinicId = clinicDay.ClinicId,                  
                    Hours = _clinicRepository.GetHoursList(),
                    Minutes = _clinicRepository.GetMinutesList(),
                    AMPM = _clinicRepository.GetAMPMList()
                };

                return View(editClinicdayViewModel);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClinicDay(EditClinicDayViewModel editClinicDay)
        {
            if (ModelState.IsValid)
            {
                var day = editClinicDay.Day;
                var openTime = editClinicDay.StartTimeHour + ":"
                                + editClinicDay.StartTimeMin + " "
                                + editClinicDay.StartAMPM;
                var closeTime = editClinicDay.EndTimeHour + ":"
                                + editClinicDay.EndTimeMin + " "
                                + editClinicDay.EndAMPM;

                // to check if start time is not before end time
                var dtStart = DateTime.Parse(openTime).ToString("HH:mm");
                var dtEnd = DateTime.Parse(closeTime).ToString("HH:mm");

                var totalStartMinutes = TimeSpan.Parse(dtStart).TotalMinutes;
                var totalEndMinutes = TimeSpan.Parse(dtEnd).TotalMinutes;


                // check if any appointments exists on before open time and after close time
                var appointmentExists = _appointmentRepository
                                          .CheckAppointmentExists(totalStartMinutes, totalEndMinutes, day);

                if (totalStartMinutes >= totalEndMinutes || appointmentExists)
                {
                    var model = new EditClinicDayViewModel()
                    {
                        Id = editClinicDay.Id,
                        Day = day,
                        StartTimeHour = editClinicDay.StartTimeHour,
                        StartTimeMin = editClinicDay.StartTimeMin,
                        StartAMPM = editClinicDay.StartAMPM,

                        EndTimeHour = editClinicDay.EndTimeHour,
                        EndTimeMin = editClinicDay.EndTimeMin,
                        EndAMPM = editClinicDay.EndAMPM,
                        ClinicId = editClinicDay.ClinicId,
                        Hours = _clinicRepository.GetHoursList(),
                        Minutes = _clinicRepository.GetMinutesList(),
                        AMPM = _clinicRepository.GetAMPMList()
                    };
                    
                    if (appointmentExists)
                    {
                        ModelState.AddModelError("CustomError", "There are appointments booked in " +
                        "the future either before the open time or after closing time!" +
                        "Please amend the appointments before making changes.");
                    }
                    else
                    {
                        ModelState.AddModelError("CustomError", "Ensure start time is not equal or before the end time!");
                    }

                    return View(model);
                }
               
                else
                {
                    // create clinic day
                    var clinicDay = new ClinicDay()
                    {
                        Id = editClinicDay.Id,
                        Day = editClinicDay.Day,
                        OpenTime = openTime,
                        CloseTime = closeTime,
                        ClinicId = editClinicDay.ClinicId
                    };

                    // update clinic day
                    var updated = _clinicRepository.UpdateClinicDay(clinicDay);
                    if (updated)
                    {
                        return RedirectToAction("Index");
                    }
                }

                return HttpNotFound("Unale to save day. Something went wrong while saving");
            }

            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult DeleteClinicDay(int id)
        {
            // get clinic day
            var clinicDay = _clinicRepository.GetClinicDay(id);
            if (clinicDay != null)
            {
                // convert day to enum day
                var day = Convert.ToString(clinicDay.Day);
                var enumDay = ((DayOfWeek)Enum.Parse(typeof(DayOfWeek), day));

                // check if there are future appointments booked on this day
                var appExists = _appointmentRepository.Appointments
                                    .Where(s => s.Date >= DateTime.Today)
                                    .Any(s => s.Date.DayOfWeek == enumDay);

                if (appExists)
                {
                    // return to index with error 
                    ViewBag.Error = $"Failed to delete {day}. There are future appointments booked on this day";
                    
                    return View("Index");
                }
                else
                {
                    // delete day
                    var deleted = _clinicRepository.DeleteClinicDay(clinicDay);

                    if (deleted)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return HttpNotFound();
        }
    }
}