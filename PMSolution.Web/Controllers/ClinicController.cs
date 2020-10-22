﻿using System;
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
            var existingDays = _clinicRepository.GetClinicDays(id);
            var weekDays = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
       
            var daysList = new List<SelectListItem>();

            // ignore existing days
            foreach (var day in weekDays)
            {   
                if (!existingDays.Contains(day))
                {
                    daysList.Add(new SelectListItem { Value = day, Text = day });
                }
            }

            // create clinic day
            var clinicDays = new ClinicDayViewModel()
            {
                ClinicId = id,
                Days = daysList,
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

                var clinicDay = new ClinicDay()
                {
                    Id = editClinicDay.Id,
                    Day = day,
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

                return HttpNotFound("Unale to save day. Something went wrong while saving");

            }
            return null;
        }

        [HttpGet]
        public ActionResult DeleteClinicDay(int id)
        {
            // get clinic day
            var clinicDay = _clinicRepository.GetClinicDay(id);
            if (clinicDay != null)
            {
                var deleted = _clinicRepository.DeleteClinicDay(clinicDay);

                if (deleted)
                {
                    return RedirectToAction("Index");
                }
            }

            return HttpNotFound();
        }

    }
}