using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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


        // local methods
        public List<SelectListItem> GetHoursList()
        {
            return new List<SelectListItem>
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
                    };
        }

        public List<SelectListItem> GetMinutesList()
        {
            return new List<SelectListItem>
                    {
                        new SelectListItem { Value = "00", Text = "00" },
                        new SelectListItem { Value = "15", Text = "15" },
                        new SelectListItem { Value = "30", Text = "30" },
                        new SelectListItem { Value = "45", Text = "45" }
                    };
        }

        public List<SelectListItem> GetAMPMList()
        {
            return new List<SelectListItem>
                    {
                        new SelectListItem { Value = "AM", Text = "AM" },
                        new SelectListItem { Value = "PM", Text = "PM" }
                    };
        }
    }
}