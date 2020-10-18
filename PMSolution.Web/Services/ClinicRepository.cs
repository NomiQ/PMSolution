using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PMSolution.Web.Domain;
using PMSolution.Web.Models;

namespace PMSolution.Web.Services
{
    public class ClinicRepository : IClinicRepository
    {
        public readonly ApplicationDbContext _appDbContext;

        public ClinicRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Clinic GetClinic()
        {
            var clinic = _appDbContext.Clinics.FirstOrDefault();
            return clinic;
        }

        

        public Clinic GetClinicDetails(int id)
        {
            var clinic = _appDbContext.Clinics
                            .FirstOrDefault(s => s.Id == id);
            return clinic;
        }

        public bool AddClinic(Clinic clinic)
        {
            _appDbContext.Clinics.Add(clinic);
            var created = _appDbContext.SaveChanges();

            return created > 0;
        }

        public bool EditClinic(Clinic clinic)
        {
            var editClinic = _appDbContext.Clinics
                                .FirstOrDefault(s => s.Id == clinic.Id);

            if (editClinic != null)
            {
                // set state to modify
                _appDbContext.Entry(editClinic).State = EntityState.Modified;

                editClinic.Name = clinic.Name;
                editClinic.PhoneNumber = clinic.PhoneNumber;
                editClinic.FaxNumber = clinic.FaxNumber;
                editClinic.Email = clinic.Email;
                editClinic.Address = clinic.Address;    
            }

            var updated = _appDbContext.SaveChanges();
            return updated > 0;
        }

        public bool Delete(Clinic clinic)
        {
            // remove clinic
            _appDbContext.Clinics.Remove(clinic);
            var removed = _appDbContext.SaveChanges();

            return removed > 0;
        }


        public bool IsClinicDay(string day)
        {
            var exists = _appDbContext.ClinicDays
                            .Any(s => s.Day == day);
            return exists;
        }

        public bool AddClinicDay(ClinicDay clinicDay)
        {
            _appDbContext.ClinicDays.Add(clinicDay);
            var added = _appDbContext.SaveChanges();

            return added > 0;
        }

    }
}