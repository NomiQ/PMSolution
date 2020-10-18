using System;
using System.Collections.Generic;
using PMSolution.Web.Domain;

namespace PMSolution.Web.Services
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> Patients { get; }
        Patient GetPatient(int id);
        List<Patient> SearchPatient(string surname, DateTime dob);
        bool Create(Patient patient);
        bool Update(Patient patient);
        bool Delete(Patient patient);
    }
}