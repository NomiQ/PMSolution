﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PMSolution.Web.Services;
using PMSolution.Web.ViewModels;
using PMSolution.Web.Domain;

namespace PMSolution.Web.Controllers
{
    public class PatientManagementController : Controller
    {
        private readonly IPatientRepository _patientRepository;

        public PatientManagementController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public ActionResult Index()
        {
            var patients = _patientRepository.Patients.ToList();
            var model = new PatientsViewModel()
            {
                AllPatients = patients
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult SearchPatient()
        {
            var model = new SearchPatientsViewModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult ViewPatient(int id, bool ind)
        {
            var patient = _patientRepository.GetPatient(id);
            if (patient != null)
            {
                var mapPatient = Mapper.Map<Patient, PatientViewModel>(patient);
                mapPatient.Ind = ind;
                return View(mapPatient);
            }

            return HttpNotFound();

        }

        [HttpPost]
        public ActionResult ViewPatient(SearchPatientsViewModel patient)
        {
            var fPatient = _patientRepository.SearchPatient(patient.Surname, patient.DateOfBirth);
            if (fPatient != null)
            {
                var mapPatient = Mapper.Map<Patient, PatientViewModel>(fPatient);
                return View(mapPatient);
            }

            return HttpNotFound();

        }

        public ActionResult AddPatient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPatient(AddEditPatientViewModel patientRequest)
        {

            if (ModelState.IsValid)
            {
                var patient =  Mapper.Map<AddEditPatientViewModel, Patient>(patientRequest);
                var created = _patientRepository.Create(patient);

                if (created)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(patientRequest);
        }

        [HttpGet]
        public ActionResult EditPatient(int id)
        {
            // get patient details
            var patient = _patientRepository.GetPatient(id);

            if (patient != null)
            {
                // map to view model
                var editPatient = Mapper.Map<Patient, AddEditPatientViewModel>(patient);

                return View(editPatient);
            }

            return HttpNotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPatient(AddEditPatientViewModel editPatient)
        {
            if (ModelState.IsValid)
            {
                var patient = Mapper.Map<AddEditPatientViewModel, Patient>(editPatient);
                var updated = _patientRepository.Update(patient);

                if (updated)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(editPatient);
        }

        [HttpGet]
        public ActionResult DeletePatient(int id)
        {
            var patient = _patientRepository.GetPatient(id);
            if (patient != null)
            {
                var deleted = _patientRepository.Delete(patient);

                if (deleted)
                {
                    return RedirectToAction("Index");
                }

                return new HttpStatusCodeResult(400);
            }

            return HttpNotFound();
        }

    }
}