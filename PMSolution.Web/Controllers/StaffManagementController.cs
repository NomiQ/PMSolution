using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PMSolution.Web.Domain;
using PMSolution.Web.Services;
using PMSolution.Web.ViewModels;

namespace PMSolution.Web.Controllers
{
    public class StaffManagementController : Controller
    {
        private readonly IStaffRepository _staffRepository;

        public StaffManagementController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public ActionResult Index()
        {
            var staffs = _staffRepository.AllStaff.ToList();
            var model = new StaffsViewModel()
            {
                AllStaff = staffs
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult ViewStaff(int id)
        {
            var staff = _staffRepository.GetStaff(id);
            if (staff != null)
            {
                var mapStaff = Mapper.Map<Staff, StaffViewModel>(staff);
                return View(mapStaff);
            }

            return HttpNotFound();

        }

        public ActionResult AddStaff()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStaff(AddEditStaffViewModel staffRequest)
        {

            if (ModelState.IsValid)
            {
                var staff = Mapper.Map<AddEditStaffViewModel, Staff>(staffRequest);
                var created = _staffRepository.Create(staff);

                if (created)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(staffRequest);
        }

        [HttpGet]
        public ActionResult EditStaff(int id)
        {
            // get staff details
            var staff = _staffRepository.GetStaff(id);

            if (staff != null)
            {
                // map to view model
                var editStaff = Mapper.Map<Staff, AddEditStaffViewModel>(staff);

                return View(editStaff);
            }

            return HttpNotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStaff(AddEditStaffViewModel editStaff)
        {
            if (ModelState.IsValid)
            {
                var staff = Mapper.Map<AddEditStaffViewModel, Staff>(editStaff);
                var updated = _staffRepository.Update(staff);

                if (updated)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(editStaff);
        }

        [HttpGet]
        public ActionResult DeleteStaff(int id)
        {
            var staff = _staffRepository.GetStaff(id);
            if (staff != null)
            {
                var deleted = _staffRepository.Delete(staff);

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