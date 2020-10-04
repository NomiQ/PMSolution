using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PMSolution.Web.Domain;
using PMSolution.Web.Models;

namespace PMSolution.Web.Services
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public StaffRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Staff> AllStaff
        {
            get
            {
                return _appDbContext.Staffs;
            }
        }

        public Staff GetStaff(int id)
        {
            var staffMemeber = _appDbContext.Staffs
                                .FirstOrDefault(s => s.Id == id);
            return staffMemeber;
        }

        public bool Create(Staff staff)
        {
            _appDbContext.Staffs.Add(staff);
            var created = _appDbContext.SaveChanges();

            return created > 0;
        }

        public bool Update(Staff staff)
        {
            var editStaff = _appDbContext.Staffs.FirstOrDefault(s => s.Id == staff.Id);

            if (editStaff != null)
            {
                // set state to modify
                _appDbContext.Entry(editStaff).State = EntityState.Modified;

                editStaff.FirstName = staff.FirstName;
                editStaff.LastName = staff.LastName;
                editStaff.Gender = staff.Gender;
                editStaff.CNIC = staff.CNIC;
                editStaff.DateOfBirth = staff.DateOfBirth;
                editStaff.Address = staff.Address;
            }

            var updated = _appDbContext.SaveChanges();
            return updated > 0;
        }

        public bool Delete(Staff staff)
        {
            //remove Staff
            _appDbContext.Staffs.Remove(staff);
            var removed = _appDbContext.SaveChanges();

            return removed > 0;
        }
    }
}