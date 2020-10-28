using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PMSolution.Web.Domain;
using PMSolution.Web.Enums;
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
            var clinic = _appDbContext.Clinics
                            .Include(s => s.ClinicDays)
                            .FirstOrDefault();

                return clinic;
        }

        public Clinic GetClinic(int id)
        {
            var clinic = _appDbContext.Clinics
                            .Include(s => s.ClinicDays)                                                
                            .FirstOrDefault();
            
            return clinic;
        }

        public Clinic GetClinicDetailsOnly(int id)
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

        public bool DeleteClinic(Clinic clinic)
        {
            // remove clinic
            _appDbContext.Clinics.Remove(clinic);
            var removed = _appDbContext.SaveChanges();

            return removed > 0;
        }

        public ClinicDay GetClinicDay(int id)
        {
            var clinicDay = _appDbContext.ClinicDays
                                .FirstOrDefault(s => s.Id == id);

            return clinicDay;
        }

        public bool CheckDayExists(WeekDays day)
        {
            var exists = _appDbContext.ClinicDays
                            .Any(s => s.Day == day);
            return exists;
        }

        public IEnumerable<WeekDays> GetClinicDays(int id)
        {
            var days = _appDbContext.ClinicDays
                            .Where(s => s.ClinicId == id)
                            .Select(d => d.Day)
                            .ToList();
            return days;
        }
        public List<ClinicDay> GetClinicBusinessHours(int id)
        {
            var clinicBusinessHours = _appDbContext.ClinicDays
                                .Where(s => s.ClinicId == id)
                                .ToList();
            
            return clinicBusinessHours;
        }

        public bool AddClinicDay(ClinicDay clinicDay)
        {
            _appDbContext.ClinicDays.Add(clinicDay);
            var added = _appDbContext.SaveChanges();

            return added > 0;
        }

        public bool UpdateClinicDay(ClinicDay clinicDay)
        {
            var editClinicDay = _appDbContext.ClinicDays
                                .FirstOrDefault(s => s.Id == clinicDay.Id);

            if (editClinicDay != null)
            {
                // set state to modify
                _appDbContext.Entry(editClinicDay).State = EntityState.Modified;

                editClinicDay.Day = clinicDay.Day;
                editClinicDay.OpenTime = clinicDay.OpenTime;
                editClinicDay.CloseTime = clinicDay.CloseTime;
                editClinicDay.ClinicId = clinicDay.ClinicId;
            }

            var updated = _appDbContext.SaveChanges();
            
            return updated > 0;
        }

        public bool DeleteClinicDay(ClinicDay clinicDay)
        {
            // remove clinic day
            _appDbContext.ClinicDays.Remove(clinicDay);
            var removed = _appDbContext.SaveChanges();

            return removed > 0;
        }



    }
}