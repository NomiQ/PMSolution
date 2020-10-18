using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using PMSolution.Web.MappingProfiles;
using PMSolution.Web.Models;
using PMSolution.Web.Services;

namespace PMSolution.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            // register mvc controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // register appDbContext
            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();

            // register repositories
            builder.RegisterType<PatientRepository>()
                   .As<IPatientRepository>()
                   .InstancePerRequest();

            builder.RegisterType<StaffRepository>()
                  .As<IStaffRepository>()
                  .InstancePerRequest();

            builder.RegisterType<AppointmentRepository>()
                  .As<IAppointmentRepository>()
                  .InstancePerRequest();

            builder.RegisterType<ClinicRepository>()
                 .As<IClinicRepository>()
                 .InstancePerRequest();



            var container = builder.Build();

            // resolve dependency for mvc
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}