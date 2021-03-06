﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;
using PMSolution.Web.Enums;

namespace PMSolution.Web.Domain
{
    public class Staff
    {
        public int Id { get; set; }
        public StaffType Type { get; set; }
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string CNIC { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }


        public List<Appointment> Appointments { get; set; }
    }
}