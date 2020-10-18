using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PMSolution.Web.Domain;
using PMSolution.Web.Models;
using PMSolution.Web.Services;
using PMSolution.Web.ViewModels;

namespace PMSolution.Web.Controllers
{
    public class ClinicController : Controller
    {
        private readonly IClinicRepository _clinicRepository;

        public ClinicController(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        public ActionResult Index()
        {
            // retrieve clinic info if exists
            var clinic = _clinicRepository.GetClinic();

            if (clinic != null)
            {
                var mapClinic = Mapper.Map<Clinic, ClinicViewModel>(clinic);
                return View(mapClinic);
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
        public ActionResult AddClinicDays(int id)
        {
            var clinicDays = new ClinicDayViewModel()
            {
                ClinicId = id,
                Days = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Monday", Text = "Monday" },
                    new SelectListItem { Value = "Tuesday", Text = "Tuesday" },
                    new SelectListItem { Value = "Wednesday", Text = "Wednesday" },
                    new SelectListItem { Value = "Thursday", Text = "Thursday" },
                    new SelectListItem { Value = "Friday", Text = "Friday" },
                    new SelectListItem { Value = "Saturday", Text = "Saturday" },
                    new SelectListItem { Value = "Sunday", Text = "Sunday" }
                },
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

                // check if day already exists
                var dayExists = _clinicRepository.IsClinicDay(day);

                if (dayExists)
                {
                    return View(clinicDay);
                }
                else
                {
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
            }

            return View(clinicDay);
        }

        [HttpGet]
        public ActionResult EditClinic(int id)
        {
            // get clinic details
            var clinic = _clinicRepository.GetClinicDetails(id);

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
    }
}