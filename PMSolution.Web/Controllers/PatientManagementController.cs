using System;
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
        private readonly IMapper _mapper;

        public PatientManagementController(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
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
        public ActionResult ViewPatient(int id)
        {
            var patient = _patientRepository.GetPatient(id);
            if (patient != null)
            {
                var mapPatient = _mapper.Map<Patient, PatientViewModel>(patient);
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
                var patient = _mapper.Map<AddEditPatientViewModel, Patient>(patientRequest);
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
                var editPatient = _mapper.Map<Patient, AddEditPatientViewModel>(patient);

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
                var patient = _mapper.Map<AddEditPatientViewModel, Patient>(editPatient);
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