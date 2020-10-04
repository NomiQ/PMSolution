using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PMSolution.Web.Domain;
using PMSolution.Web.Models;

namespace PMSolution.Web.Services
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public PatientRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Patient> Patients
        {
            get
            {
                return _appDbContext.Patients;
            }
        }

        public Patient GetPatient(int id)
        {
            var patient = _appDbContext.Patients
                                .FirstOrDefault(s => s.Id == id);
            return patient;
        }

        public bool Create(Patient patient)
        {
            _appDbContext.Patients.Add(patient);
            var created = _appDbContext.SaveChanges();

            return created > 0;
        }

        public bool Update(Patient patient)
        {
            var editPatient = _appDbContext.Patients.FirstOrDefault(s => s.Id == patient.Id);

            if (editPatient != null)
            {
                // set state to modify
                _appDbContext.Entry(editPatient).State = EntityState.Modified;

                editPatient.MRN = patient.MRN;
                editPatient.FirstName = patient.FirstName;
                editPatient.LastName = patient.LastName;
                editPatient.Gender = patient.Gender;
                editPatient.CNIC = patient.CNIC;
                editPatient.DateOfBirth = patient.DateOfBirth;
                editPatient.Address = patient.Address;
            }

            var updated = _appDbContext.SaveChanges();
            return updated > 0;
        }

        public bool Delete(Patient patient)
        {
            //remove patient
            _appDbContext.Patients.Remove(patient);
            var removed = _appDbContext.SaveChanges();

            return removed > 0;
        }
    }
}