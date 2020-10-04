using System.Collections.Generic;
using PMSolution.Web.Domain;

namespace PMSolution.Web.Services
{
    public interface IStaffRepository
    {
        IEnumerable<Staff> AllStaff { get; }
        Staff GetStaff(int id);
        bool Create(Staff staff);
        bool Update(Staff staff);
        bool Delete(Staff staff);
    }
}