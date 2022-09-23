using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PMSolution.Web.Controllers;
using PMSolution.Web.Enums;
using PMSolution.Web.Services;
using PMSolution.Web.ViewModels;

namespace PMSolution.Test
{
    [TestClass]
    public class ClinicTests
    {
        [TestMethod]
        public void CheckStartEndTimeEdit()
        {
            // arrange
            var model = new EditClinicDayViewModel()
            {
                StartTimeHour = "10",
                StartTimeMin = "30",
                StartAMPM = "AM",

                EndTimeHour = "10",
                EndTimeMin = "30",
                EndAMPM = "AM",

                Hours = GetHoursList(),
                Minutes = GetMinutesList(),
                AMPM = GetAMPMList(),

                Day = WeekDays.Tuesday,
                customError = ""
                
            };
            //objects to hold clinic and appointment repositories
            var mock1 = new Mock<IClinicRepository>();
            var mock2 = new Mock<IAppointmentRepository>();

            // actual
            var controller = new ClinicController(mock1.Object, mock2.Object);

            var modelReply = controller.EditClinicDay(model);

            // assert
            Assert.IsNotNull(modelReply);
        }


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
